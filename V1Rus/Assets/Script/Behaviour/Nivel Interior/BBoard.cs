using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BBoard : MonoBehaviour
{
    #region ATTRIBUTES
    
    [Header("Data")]
    [SerializeField]

    /// <summary>
    /// Datos del nivel
    /// </summary>
    public DBoardInfo boardInfo;

    /// <summary>
    /// Datos del jugador en el nivel
    /// </summary>
    public DPlayerInfo playerInfo;

    /// <summary>
    /// Datos de la camara del nivel
    /// </summary>
    public DCameraInfo cameraInfo;

    /// <summary>
    /// Prefab con la casilla basica
    /// </summary>
    public GameObject Tile;

    /// <summary>
    /// Prefab de la particula de datos
    /// </summary>
    public GameObject dataParticle;

    /// <summary>
    /// Enumerado para definir el tipo de coordenada que vamos a usar
    /// </summary>
    public enum ECord
    {
        XY,
        XZ,
        YZ
    }

    /// Guardamos los valores que tenemos que sumarle al indice actual para conseguir el indice asociado al string
    public Dictionary<string, int> indexDirections = new Dictionary<string, int>();

    /// Guardamos el valor del systema de coordenadas
    private ECord coordSys;

    /// Diccionario para almacenar las localizaciones de las casillas en funcion de los indices de un array
    private Dictionary<int, Vector2> locations = new Dictionary<int, Vector2>();

    /// Diccionario para almacena los costes de los bordes con respecto a cada casilla
    private Dictionary<int, Dictionary<int, int>> edges = new Dictionary<int, Dictionary<int, int>>();

    /// Diccionario para almacenar los tiles y poder cambiar sus valores
    private Dictionary<int, BTile> tiles = new Dictionary<int, BTile>();

    /// Diccionario para almacenar las particulas de datos
    private Dictionary<int, GameObject> dataParticles = new Dictionary<int, GameObject>();

    /// Diccionario para almacenar los muros de almacenamiento
    private Dictionary<int, BMuro> walls = new Dictionary<int, BMuro>();

    /// Rotacion del jugador que nos servira para spawnear las particulas
    private Quaternion spawnRotation;

    /// Indice de la casilla incial y de la casilla final
    private int startIndex;
    private int endIndex;

    /// Tamaño en casillas del tablero en la primera Coordenada
    private int size1;
    /// Tamaño en casillas del tablero en la segunda Coordenada
    private int size2;

    /// Limites del mapa en Eje X y Z
    private float min1;
    private float max1;
    private float min2;
    private float max2;

    /// Tamaño de las casillas 
    private float tileSize1;
    private float tileSize2;
    private float tileSize3;

    /// Coordenada de la superficie
    private float surfaceCoord;

    /// Manager del nivel
    private BGameManager gameManager;

    /// Camara asociada al nivel
    private BCameraController camera;

    /// Script del jugador
    private BPlayer player;

    /// Set que contiene las posiciones de los enemigos
    private HashSet<int> enemiesPos = new HashSet<int>();

    #endregion

    #region METHODS

    #region UPDATE SETUP DESTROY

    /// <summary>
    /// Funcion encargada de hacer el Setup del Board iniciando todas sus estructuras logicas
    /// </summary>
    /// <param name="manager">Manager del cubo</param>
    public void SetupBoard(BGameManager manager)
    {
        // GUardamos el manager
        gameManager = manager;

        // Primero observamos el sistema de coordenadas que vamos a utilizar
        // El sistema elegido dependera de las coordenadas que provengan del boardInfo
        if (boardInfo.x && boardInfo.y)
        {
            coordSys = ECord.XY;
        }
        else if (boardInfo.x && boardInfo.z)
        {
            coordSys = ECord.XZ;
        }
        else if (boardInfo.y && boardInfo.z)
        {
            coordSys = ECord.YZ;
        }
        else
        {
            Debug.LogError("Por favor indique en la informacion del tablero dos coordenadas para el systema de coordenadas");
        }

        // Obtenemos el tamaño de la casilla base y lo guardamos para usarlo mas tarde
        Vector3 tileSize = Tile.GetComponent<Renderer>().bounds.size;
        tileSize1 = (int)tileSize.x;
        tileSize2 = (int)tileSize.z;
        tileSize3 = tileSize.y;

        // Obtenemos todas las casillas de la escena activas
        Object[] boardTiles = GetComponentsInChildren<BTile>();

        // Obtenemos la coordenada de la superficie para poder spawnear particulas mas tarde en esta
        BTile aux = (BTile)boardTiles[0];
        switch (coordSys)
        {
            case ECord.XY:
                surfaceCoord = aux.gameObject.transform.position.z + aux.transform.up.z * tileSize3 / 2;
                break;
            case ECord.XZ:
                surfaceCoord = aux.gameObject.transform.position.y + aux.transform.up.y * tileSize3 / 2;
                break;
            case ECord.YZ:
                surfaceCoord = aux.gameObject.transform.position.x + aux.transform.up.x * tileSize3 / 2;
                break;
            default:
                break;
        }

        // A partir de todas estas casillas obtenemos sus posiciones en el sistema
        List<float> firstPositions = new List<float>();
        List<float> secondPositions = new List<float>();

        foreach (BTile item in boardTiles)
        {
            // Dependiendo del sistema de coordenadas guardamos unos valores diferentes
            switch (coordSys)
            {
                case ECord.XY:
                    firstPositions.Add(item.gameObject.transform.position.x);
                    secondPositions.Add(item.gameObject.transform.position.y);
                    break;
                case ECord.XZ:
                    firstPositions.Add(item.gameObject.transform.position.x);
                    secondPositions.Add(item.gameObject.transform.position.z);
                    break;
                case ECord.YZ:
                    firstPositions.Add(item.gameObject.transform.position.y);
                    secondPositions.Add(item.gameObject.transform.position.z);
                    break;
                default:
                    break;
            }
        }

        // Guardamos la posicion minima para usarla mas tarde
        min1 = firstPositions.Min();
        min2 = secondPositions.Min();
        max1 = firstPositions.Max();
        max2 = secondPositions.Max();

        // Calculamos el tamaño del tablero a partir de las posiciones y de los tamaños
        size1 = Mathf.RoundToInt((max1 - min1) / tileSize1) + 1;
        size2 = Mathf.RoundToInt((max2 - min2) / tileSize2) + 1;

        // Spawneamos el plano de collision para detectar los clicks
        SpawnCollisionPlane();

        // Premaramos el diccionario con las diferentes direcciones
        indexDirections.Add("Up", size1);
        indexDirections.Add("Down", -size1);
        indexDirections.Add("Left", -1);
        indexDirections.Add("Right", 1);

        //Para cada casilla almacenamos su posicion y como referente guardamos su indice en el array
        foreach (BTile item in boardTiles)
        {
            //Calculamos el nuevo indice para guardar en el diccionario de localizaciones
            Vector3 position = item.gameObject.transform.position;
            int firstIndex = 0;
            int secondIndex = 0;

            switch (coordSys)
            {
                case ECord.XY:

                    // Calculamos los indices respecto a las posiciones
                    firstIndex = Mathf.RoundToInt((position.x - min1) / tileSize1) ;
                    secondIndex = Mathf.RoundToInt((position.y - min2) / tileSize2) * size1;
                    // Guardamos en el diccionario el index con su respectiva posicion
                    locations.Add((firstIndex + secondIndex), new Vector2(position.x, position.y));
                    tiles.Add((firstIndex + secondIndex), item);
                    break;
                case ECord.XZ:

                    // Calculamos los indices respecto a las posiciones
                    firstIndex = Mathf.RoundToInt((position.x - min1) / tileSize1);
                    secondIndex = Mathf.RoundToInt((position.z - min2) / tileSize2) * size1;
                    // Guardamos en el diccionario el index con su respectiva posicion
                    locations.Add((firstIndex + secondIndex), new Vector2(position.x, position.z));
                    tiles.Add((firstIndex + secondIndex), item);
                    break;
                case ECord.YZ:

                    // Calculamos los indices respecto a las posiciones
                    firstIndex = Mathf.RoundToInt((position.y - min1) / tileSize1);
                    secondIndex = Mathf.RoundToInt((position.z - min2) / tileSize2) * size1;
                    // Guardamos en el diccionario el index con su respectiva posicion
                    locations.Add((firstIndex + secondIndex), new Vector2(position.y, position.z));
                    tiles.Add((firstIndex + secondIndex), item);
                    break;
                default:
                    break;
            }

            //Guardamos el indice de la casilla inicial y final
            if (item.currentState == BTile.ETileState.Start)
            {
                startIndex = firstIndex + secondIndex;

            }
            else if (item.currentState == BTile.ETileState.End)
            {
                endIndex = firstIndex + secondIndex;
            }
        }

        // Para cada casilla almacenamos sus bordes usando como referente su indice en el array
        foreach (int index in locations.Keys)
        {
            // Llamamos a la funcion CreateLocalEdges que nos devuelve los bordes del indice
            edges.Add(index, CreateLocalEdges(index));
        }

        // Añadimos los bordes de las paredes
        AddWallsEdges();

        // Ponemos a 0 los bordes del tablero
        SetBorderEdges();

        // Obtenemos la posicion y rotacion para instanciar al jugador
        Vector3 playerPos = GetPlayerSpawnPos(boardInfo.player.GetComponent<Renderer>().bounds.size.y);
        Quaternion playerRot = GetPlayerSpawnRot();

        // Instanciamos al jugador
        GameObject playerGO = Instantiate(boardInfo.player,playerPos,playerRot);

        // Obtenemos el script del jugador y lo guardamos
        player = playerGO.GetComponent<BPlayer>();

        player.transform.parent = this.transform;

        // Hacemos el setup del jugador
        player.SetupPlayer(manager, playerInfo);

        

        // Obtenemos la roatacion de los elementos del tablero para cuando spawneemos particulas
        spawnRotation = player.transform.rotation;

        // Obtenemos todos los enemigos y los guardamos en la lista
        foreach (BEnemy enemy in GetComponentsInChildren<BEnemy>())
        {
            enemiesPos.Add(enemy.GetEnemyIndex());
            enemy.SetupEnemy(manager);
        }

        // Spawneamos el target de la camara y lo colocamos en el centro
        GameObject target = new GameObject("Camera Target");

        target.transform.position = Vector3.zero;
        target.transform.rotation = Quaternion.identity;

        target.transform.parent = this.transform;

        // Instanciamos la camara
        GameObject cameraGO = Instantiate(boardInfo.camera, cameraInfo.position, Quaternion.Euler(cameraInfo.rotation));

        cameraGO.transform.parent = target.transform;

        // Guardamos el script de la camara
        camera = cameraGO.GetComponent<BCameraController>();

        // Ejecutamos el Setup
        camera.SetupCamera(manager, cameraInfo);

        // Iniciamos las puertas del nivel
        foreach (BPuerta puerta in GetComponentsInChildren<BPuerta>())
        {
            puerta.SetupDoor(manager);
        }
        foreach (BTerminal terminal in GetComponentsInChildren<BTerminal>())
        {
            terminal.SetupTerminal(manager);
        }
    }

    /// <summary>
    /// Elimina al jugador y la camara del nivel
    /// </summary>
    public void EndBoard()
    {
        player.DestroyPath();

        Destroy(player.gameObject);
        
        Destroy(camera.transform.parent.gameObject);

        Destroy(camera.gameObject);

        locations.Clear();

        tiles.Clear();

        edges.Clear();

        walls.Clear();

        indexDirections.Clear();
    }
    #endregion

    #region GETTERS

    /// <summary>
    /// Devuelve el tamaño de la casilla
    /// </summary>
    /// <returns></returns>
    public float GetTileSize()
    {
        return tileSize1;
    }

    /// <summary>
    /// Devolvemos los limites del Tablero en un array de 4 componentes
    /// </summary>
    /// <returns> 
    /// Vector4 que contiene la minima coordenada de la primera coordenada y la maxima 
    /// seguida de la minima coordenada de la segunda coordenada y la maxima de la misma 
    /// </returns>
    public Vector4 GetBoardLimits()
    {
        return new Vector4(min1, max1, min2, max2);
    }

    /// <summary>
    /// Devolvemos el tamaño del tablero en casillas
    /// </summary>
    /// <returns> Vector2 que contiene size1 seguido de size2 </returns>
    public Vector2 GetBoardShape()
    {
        return new Vector2(size1, size2);
    }

    /// <summary>
    /// Devuelve el Ap asignado en este nivel
    /// </summary>
    /// <returns></returns>
    public int GetBoardAp()
    {
        return boardInfo.APNivel;
    }

    /// <summary>
    /// Devuelve el vector que apunta hacia arriba del tablero
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBoardUp()
    {
        return tiles[startIndex].transform.up;
    }

    /// <summary>
    /// Devuelve la posicion de spawn para el enemigo
    /// </summary>
    /// <param name="pos"> Posicion del waypoint en el que queremos spawnear</param>
    /// <returns></returns>
    public Vector3 GetEnemySpawnPos(Vector3 pos, Vector3 enemySize)
    {
        switch (coordSys)
        {
            case ECord.XY:
                return new Vector3(pos.x, pos.y, surfaceCoord + GetBoardUp().z * enemySize.z / 2);
            case ECord.XZ:
                return new Vector3(pos.x, surfaceCoord + GetBoardUp().y * enemySize.y / 2, pos.z);
            case ECord.YZ:
                return new Vector3(surfaceCoord + GetBoardUp().x * enemySize.x / 2, pos.y, pos.z);
            default:
                return GetBoardUp();
        }

    }

    /// <summary>
    /// Devuelve la posicion de spawn para el jugador
    /// </summary>
    /// <param name="playerHeight">Float Altura del jugador</param>
    /// <returns></returns>
    public Vector3 GetPlayerSpawnPos(float playerHeight)
    {

        switch (coordSys)
        {
            case ECord.XY:
                return IndexToPosition(startIndex, null, surfaceCoord + GetBoardUp().z * playerHeight / 2);
            case ECord.XZ:
                return IndexToPosition(startIndex, null, surfaceCoord + GetBoardUp().y * playerHeight / 2);
            case ECord.YZ:
                return IndexToPosition(startIndex, null, surfaceCoord + GetBoardUp().x * playerHeight / 2);
            default:
                return GetBoardUp();
        }
    }

    /// <summary>
    /// Devuelve la posicion de spawn para el jugador
    /// </summary>
    /// <returns></returns>
    public Quaternion GetPlayerSpawnRot()
    {
        return Quaternion.LookRotation(tiles[startIndex].transform.forward, GetBoardUp());
    }

    /// <summary>
    /// Devuelve el sistema de coordenadas usado
    /// </summary>
    /// <returns></returns>
    public ECord GetCoordSys()
    {
        return coordSys;
    }

    /// <summary>
    /// La funcion devuelve true si hay un muro en el indice
    /// </summary>
    /// <param name="index">Indice en el que queremos saber si hay un muro</param>
    /// <returns> True en caso de haber muro False en caso contrario</returns>
    public bool isWall(int index)
    {
        return walls.ContainsKey(index);
    }

    /// <summary>
    /// La funcion devuelve true si hay una casilla en el indice
    /// </summary>
    /// <param name="index">Indice en el que queremos saber si hay tile</param>
    /// <returns>True en caso de haber una casilla false en caso contrario</returns>
    public bool isTile(int index)
    {
        return tiles.ContainsKey(index);
    }

    /// <summary>
    /// Devolvemos un array de los enemigos
    /// </summary>
    /// <returns> Array de enemigos </returns>
    public BEnemy[] GetBoardEnemies()
    {
        return GetComponentsInChildren<BEnemy>();
    }

    /// <summary>
    /// Devolvemos el jugador del tablero
    /// </summary>
    /// <returns> Jugador del tablero </returns>
    public BPlayer GetPlayer()
    {
        return player;
    }

    /// <summary>
    /// Devolvemos la camara del tablero
    /// </summary>
    /// <returns> Camara del tablero</returns>
    public BCameraController GetCamera()
    {
        return camera;
    }

    #endregion

    #region CHANGEINDEX
    /// <summary>
    /// Transforma una coordenada en el indice dentro del tablero
    /// </summary>
    /// <param name="position"> Vector 3 Posicion que queremos pasar a index </param>
    /// <returns> Indice correspondiente a la posicion </returns>
    public int PositionToIndex(Vector3 position)
    {
        switch (coordSys)
        {
            case ECord.XY:
                return Mathf.RoundToInt((position.x - min1) / tileSize1) + Mathf.RoundToInt((position.y - min2) / tileSize2) * size1;
            case ECord.XZ:
                return Mathf.RoundToInt((position.x - min1) / tileSize1) + Mathf.RoundToInt((position.z - min2) / tileSize2) * size1;
            case ECord.YZ:
                return Mathf.RoundToInt((position.y - min1) / tileSize1) + Mathf.RoundToInt((position.z - min2) / tileSize2) * size1;
            default:
                return -1;
        }
    }

    /// <summary>
    /// Transforma un indice en una posicion
    /// </summary>
    /// <param name="index">Int Indice de la casilla </param>
    /// <param name="targetObject"> GameObject objeto del que tomaremos la coordenada restante</param>
    /// <param name="extraCoord"> Offset de la coordenada extra en caso de añadir GameObject y en caso coontrario offset que le sumamos</param>
    /// <returns> Vector 3 con las cordenadas bases del tablero y la restante del objeto que recibimos </returns>
    public Vector3 IndexToPosition(int index, GameObject targetObject = null, float extraCoord = 0f)
    {
        Vector2 aux = new Vector2(min1 + (index % size1) * tileSize1, min2 + (index / size1) * tileSize2);
        if(targetObject == null)
        {
            switch (coordSys)
            {
                case ECord.XY:
                    return new Vector3(aux.x, aux.y, extraCoord);
                case ECord.XZ:
                    return new Vector3(aux.x, extraCoord, aux.y);
                case ECord.YZ:
                    return new Vector3(extraCoord, aux.x, aux.y);
                default:
                    return new Vector3(0, 0, 0);
            }
        }
        else
        {
            switch (coordSys)
            {
                case ECord.XY:
                    return new Vector3(aux.x, aux.y, targetObject.transform.position.z);
                case ECord.XZ:
                    return new Vector3(aux.x, targetObject.transform.position.y, aux.y);
                case ECord.YZ:
                    return new Vector3(targetObject.transform.position.x, aux.x, aux.y);
                default:
                    return new Vector3(0, 0, 0);
            }
        }
    }

    /// <summary>
    /// Actualizamos la posicion del enemigo en el mapa
    /// </summary>
    /// <param name="enemy">Benemy enemigo del que actualizamos la posicion</param>
    public void UpdateEnemy(int oldPos, int newPos)
    {
        enemiesPos.Remove(oldPos);
        enemiesPos.Add(newPos);
    }
    #endregion

    #region BOARD LOGIC

    #region BORDER LOGIC
    /// <summary>
    /// Recorre todos los muros y añade sus bordes a los del tablero
    /// </summary>
    private void AddWallsEdges()
    {
        // Buscamos todos los muros y los guardamos en un array
        BMuro[] walls = GetComponentsInChildren<BMuro>();

        // Realizamos el mismo proceso para cada mur
        foreach (BMuro wall in walls)
        {
            AddWallEdges(wall);

            // Tambien añadimos al muro al diccionario de muros
            int index = PositionToIndex(wall.transform.position);

            this.walls.Add(index, wall);
        }
    }

    /// <summary>
    /// Acutaliza los ejes de las casillas 
    /// </summary>
    /// <param name="index">Indice de la casilla</param>
    /// <param name="upEdge"> Eje superior de la casilla</param>
    /// <param name="leftEdge"> Eje izquierdo de la casilla</param>
    /// <param name="downEdge"> Eje inferior de la casilla</param>
    /// <param name="rightEdge"> Eje derecho de la casilla</param>
    public void UpdateWallsEdges(int index, int upEdge, int leftEdge, int downEdge, int rightEdge)
    {

        int auxIndex = index + indexDirections["Up"];
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] = upEdge;
            edges[auxIndex][index] = upEdge;
        }

        // Al sumarle uno obtenemos la casilla al oeste
        auxIndex = index + indexDirections["Left"];
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] = leftEdge;
            edges[auxIndex][index] = leftEdge;
        }

        // Al restar size1 obtenemos la casilla al sur
        auxIndex = index + indexDirections["Down"];
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] = downEdge;
            edges[auxIndex][index] = downEdge;
        }

        // Al restar 1 obtenemos la casilla al este
        auxIndex = index + indexDirections["Right"];
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] = rightEdge;
            edges[auxIndex][index] = rightEdge;
        }
        SetBorderEdges();
    }

    /// <summary>
    /// Añade los bordes de un muro a la casilla
    /// </summary>
    /// <param name="wall"> Muro del que obtenemos los bordes</param>
    private void AddWallEdges(BMuro wall)
    {
        // Calculamos su indice
        int index = PositionToIndex(wall.transform.position);
        int auxIndex;

        // Para cada uno de las casillas que lo rodea actualizamos el eje en ambos sentidos
        // Al sumarle size1 obtenemos la casilla norte
        auxIndex = index + indexDirections["Up"];
        if (locations.ContainsKey(auxIndex) && locations.ContainsKey(index))
        {
            edges[index][auxIndex] *= wall.upEdge;
            edges[auxIndex][index] *= wall.upEdge;
        }

        // Al sumarle uno obtenemos la casilla al oeste
        auxIndex = index + indexDirections["Left"];
        if (locations.ContainsKey(auxIndex) && locations.ContainsKey(index))
        {
            edges[index][auxIndex] *= wall.leftEdge;
            edges[auxIndex][index] *= wall.leftEdge;
        }

        // Al restar size1 obtenemos la casilla al sur
        auxIndex = index + indexDirections["Down"];
        if (locations.ContainsKey(auxIndex) && locations.ContainsKey(index))
        {
            edges[index][auxIndex] *= wall.downEdge;
            edges[auxIndex][index] *= wall.downEdge;
        }

        // Al restar 1 obtenemos la casilla al este
        auxIndex = index + indexDirections["Right"];
        if (locations.ContainsKey(auxIndex) && locations.ContainsKey(index))
        {
            edges[index][auxIndex] *= wall.rightEdge;
            edges[auxIndex][index] *= wall.rightEdge;
        }
    }

    /// <summary>
    /// Cambia los valores de los bordes de las casillas a los bordes a 0
    /// </summary>
    private void SetBorderEdges()
    {
        // Recorremos todos los casillas y en caso de ser uno de los bordes del tablero ponemos su borde a cero
        foreach (int index in edges.Keys)
        {
            // Borde Inferior
            if ((index / size1) == 0)
            {
                int auxIndex = index + indexDirections["Down"];
                edges[index][auxIndex] = -1;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = -1;
                }
            }

            // Borde Izquierdo
            if ((index % size1) == 0)
            {
                int auxIndex = index + indexDirections["Left"];
                edges[index][auxIndex] = -1;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = -1;
                }
            }

            // Borde Superior
            if ((index / size1) == size2)
            {
                int auxIndex = index + indexDirections["Up"];
                edges[index][auxIndex] = -1;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = -1;
                }
            }

            // Borde Derecho
            if ((index % size1) == size1 - 1)
            {
                int auxIndex = index + indexDirections["Right"];
                edges[index][auxIndex] = -1;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = -1;
                }
            }
        }
    }

    /// <summary>
    /// Obtenemos un diccionario con los 4 indices que lo rodean y sus respectivos costes
    /// </summary>
    /// <param name="index"> Int indica el indice del que queremos obtener su diccionario</param>
    /// <returns> Diccionario con los respectivos costes </returns>
    private Dictionary<int, int> CreateLocalEdges(int index)
    {
        // Diccionario que llenaremos con la informacion pertinente
        Dictionary<int, int> localEdges = new Dictionary<int, int>();
        // Variable auxiliar para guardar el indice que estudiamos en cada momento
        int auxIndex;

        // Calculamos todos los ejes a partir de los ejes basicos guardados en boardInfo
        // Al sumarle size1 obtenemos la casilla norte
        auxIndex = index + indexDirections["Up"];
        if (locations.ContainsKey(auxIndex))
        {
            localEdges.Add(auxIndex, boardInfo.upEdge);
        }
        else
        {
            localEdges.Add(auxIndex, 0);
        }

        // Al sumarle uno obtenemos la casilla al oeste
        auxIndex = index + indexDirections["Left"];
        if (locations.ContainsKey(auxIndex))
        {
            localEdges.Add(auxIndex, boardInfo.leftEdge);
        }
        else
        {
            localEdges.Add(auxIndex, 0);
        }

        // Al restar size1 obtenemos la casilla al sur
        auxIndex = index + indexDirections["Down"];
        if (locations.ContainsKey(auxIndex))
        {
            localEdges.Add(auxIndex, boardInfo.downEdge);
        }
        else
        {
            localEdges.Add(auxIndex, 0);
        }

        // Al restar 1 obtenemos la casilla al este
        auxIndex = index + indexDirections["Right"];
        if (locations.ContainsKey(auxIndex))
        {
            localEdges.Add(auxIndex, boardInfo.rightEdge);
        }
        else
        {
            localEdges.Add(auxIndex, 0);
        }

        return localEdges;
    }

    #endregion

    #region PATHFINDING

    /// <summary>
    /// Devuelve el coste de entrar a objectiveTile desde acutalTile
    /// </summary>
    /// <param name="actualTile"> Int indica el indice de la casilla actual</param>
    /// <param name="objectiveTile"> Int indica el indice de la casilla objetivo </param>
    /// <returns> Int que representa el coste del movimiento</returns>
    public float CostToEnter(int actualTile, int objectiveTile, bool activeAbility)
    {
        if (edges.ContainsKey(actualTile) && edges[actualTile].ContainsKey(objectiveTile) && !enemiesPos.Contains(objectiveTile))
        {

            if (edges[actualTile][objectiveTile] == -1)
            {
                return Mathf.Infinity;
            }
            else if (edges[actualTile][objectiveTile] != 0)
            {
                if (activeAbility && dataParticles.ContainsKey(objectiveTile))
                {
                    return -edges[actualTile][objectiveTile];

                }
                else
                {
                    return edges[actualTile][objectiveTile];
                }
            }
            else
            {
                return 2000f;
            }
        }
        else
        {
            return Mathf.Infinity;
        }
    }

    /// <summary>
    /// Devuelve una lista que representa un camino desde el indice de inicio al objetivo
    /// </summary>
    /// <param name="start"> Indice en el que estamos</param>
    /// <param name="objective"> Indice al que queremos ir</param>
    /// <param name="activeAbility"> Booleano que nos indica si la abilidad de recoger codigo esta activa</param>
    /// <param name="AP"> Float indica la cantidad de ap que tiene el jugador</param>
    /// <returns> Una Lista con los indices desde objective hasta start</returns>
    public List<int> AStarAlgorithm(int start, int objective, bool activeAbility = false, bool id = true)
    {
        Dictionary<int, float> openSet = new Dictionary<int, float>();

        Dictionary<int, int> cameFrom = new Dictionary<int, int>();

        Dictionary<int, float> gScore = new Dictionary<int, float>();
        Dictionary<int, float> fScore = new Dictionary<int, float>();

        SortedSet<int> neighbours = new SortedSet<int>();

        int current = start;

        gScore.Add(start, 0f);
        fScore.Add(start, Heuristic(start, objective));
        openSet.Add(start, fScore[start]);

        while (openSet.Count != 0)
        {
            current = openSet.OrderBy(x => x.Value).First().Key;
            
            if (current == objective)
            {
                return ReconstructPath(cameFrom, current,id);
            }

            neighbours.Add(current + size1);
            neighbours.Add(current - size1);
            neighbours.Add(current + 1);
            neighbours.Add(current - 1);

            openSet.Remove(current);

            foreach (int neighbor in neighbours)
            {

                float neighborGScore = gScore[current] + CostToEnter(current, neighbor, activeAbility);

                if (neighborGScore != Mathf.Infinity && (!gScore.ContainsKey(neighbor) || neighborGScore < gScore[neighbor]))
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = neighborGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, objective);
                    if (!openSet.ContainsKey(neighbor))
                    {
                        openSet.Add(neighbor, neighborGScore);
                    }
                }
            }

            neighbours.Clear();
        }

        return ReconstructPath(cameFrom, current,id);
    }

    /// <summary>
    /// La funcion recibe un diccionario y devuelve una lista con el camino desde el objetivo hasta el final
    /// </summary>
    /// <param name="cameFrom"> Diccionario en el que cada indice contiene al indice del que viene</param>
    /// <param name="current"> Indice de la ultima casilla</param>
    /// <returns></returns>
    private List<int> ReconstructPath(Dictionary<int,int> cameFrom, int current,bool id)
    {
        List<int> initialPath = new List<int>();
        List<int> finalPath = new List<int>();

        initialPath.Add(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            initialPath.Insert(0,current);
        }

        if (id)
        {
            initialPath.RemoveAt(0);
            return initialPath;
        }
        else
        {
            
            finalPath.Add(initialPath[0]);

            for (int i = 0; i < initialPath.Count-1; i++)
            {
                float cost = CostToEnter(initialPath[i], initialPath[i + 1],false);
                if(cost < 2000f)
                {
                    finalPath.Add(initialPath[i + 1]);
                }
                else
                {
                    finalPath.RemoveAt(0);
                    return finalPath;
                }
                
            }

            initialPath.RemoveAt(0);
            return initialPath;
        }
        
    }

    /// <summary>
    /// Devuelve la Heuristica de una casilla para el algoritmo A estrella
    /// </summary>
    /// <param name="element">Indice del que queremos saber la euristica</param>
    /// <param name="objective">Indice del valor al que queremos llegar</param>
    /// <returns>Float Heuristica basada en la distancia entre los dos puntos</returns>
    private float Heuristic(int element, int objective)
    {
        return Vector2.Distance(IndexToPosition(element), IndexToPosition(objective));
    }

    #endregion

    #region COLLISION PLANE

    private void SpawnCollisionPlane()
    {
        // Generamos la colision
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();

        // Dependiendo de el sistema de coordenadas generamos el plano de collision en un punto distinto
        switch (coordSys)
        {
            case ECord.XY:
                // Calculamos el tamaño del plano
                boxCollider.size = new Vector3(size1, size2, 0);
                // Calculamos el punto central del plano 
                boxCollider.center = transform.InverseTransformPoint(new Vector3((max1 + min1) / 2, (max2 + min2) / 2, surfaceCoord));
                break;
            case ECord.XZ:
                // Calculamos el tamaño del plano
                boxCollider.size = new Vector3(size1, 0, size2);
                // Calculamos el punto central del plano 
                boxCollider.center = transform.InverseTransformPoint(new Vector3((max1 + min1) / 2, surfaceCoord, (max2 + min2) / 2));
                break;
            case ECord.YZ:
                // Calculamos el tamaño del plano
                boxCollider.size = new Vector3(0,size1, size2);
                // Calculamos el punto central del plano 
                boxCollider.center = transform.InverseTransformPoint(new Vector3(surfaceCoord, (max1 + min1) / 2, (max2 + min2) / 2));
                break;
            default:
                break;
        }
    }

    #endregion

    #endregion

    #region PARTICLES & MATERIALS
    /// <summary>
    /// Spawnea particulas de datos en la casilla de index
    /// </summary>
    /// <param name="index"> Int index indica la casilla en la que spawneamos las particulas</param>
    /// <param name="activeAbility"> Bool indica si tenemos la habilidad de recoger cable activa
    /// ya que hasta que no la tengamos las particulas no deben cambiar la logica de los bordes 
    /// </param>
    public void SpawnParticle(int index, bool activeAbility)
    {

        // Comprobamos que no haya una particula ya en esa posicion
        if (!dataParticles.ContainsKey(index))
        {
            // Obtenemos la posicion en la que queremos spawnear
            Vector3 position = IndexToPosition(index,null,surfaceCoord);

            // Spawneamos las particulas de datos
            GameObject particle = Instantiate(dataParticle, position, spawnRotation);

            // Guardamos las particulas
            dataParticles.Add(index, particle);
        }
    }

    /// <summary>
    /// Destruye la particula en la casilla de index
    /// </summary>
    /// <param name="index">Int indica el indice de la casilla </param>
    public void DespawnParticle(int index)
    {

        // Obtenemos las particulas y la eliminamos del diccionario
        GameObject particle = dataParticles[index];
        dataParticles.Remove(index);

        // Eliminamos la particula de datos
        Destroy(particle);
    }

    /// <summary>
    /// Cambia el material de la casilla en la posicion index
    /// </summary>
    /// <param name="index"> Indice de la casilla</param>
    /// <param name="mat"> Nuevo material de la casilla</param>
    public void ChangeTileMaterial(int index, Material newMaterial)
    {
        if (tiles.ContainsKey(index))
        {
            tiles[index].ChangeMaterial(newMaterial);
        }
    }

    /// <summary>
    /// Devuelve el material de la casilla en la posicion index
    /// </summary>
    /// <param name="index">Indice de la casilla</param>
    public void ResetMaterial(int index)
    {
        if (tiles.ContainsKey(index))
        {
            tiles[index].ResetMaterial();
        }
    }

    /// <summary>
    /// Elimina el material que recibe por parametro de la lista de materiales de la casilla
    /// </summary>
    /// <param name="index">Indice de la casilla</param>
    public void RemoveMaterial(int index, Material material)
    {
        if (tiles.ContainsKey(index))
        {
            tiles[index].RemoveMaterial(material);
        }
    }

    /// <summary>
    /// Conseguimos el material de la casilla seleccionada
    /// </summary>
    /// <param name="index">Indice de la casilla deseada</param>
    /// <returns> Material de la casilla</returns>
    public Material GetTileMaterial(int index)
    {
        if (tiles.ContainsKey(index))
        {
            return tiles[index].GetComponent<Renderer>().material;
        }
        else
        {
            return null;
        }
    }
    #endregion

    #endregion
}
