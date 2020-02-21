using System.Collections;
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

    #endregion

    #region METHODS

    #region AWAKE START UPDATE
    private void Awake()
    {

        // Primero observamos el sistema de coordenadas que vamos a utilizar
        // El sistema elegido dependera de las coordenadas que provengan del boardInfo
        if (boardInfo.x && boardInfo.y)
        {
            coordSys = ECord.XY;
        }
        else if(boardInfo.x && boardInfo.z)
        {
            coordSys = ECord.XZ;
        }
        else if(boardInfo.y && boardInfo.z)
        {
            coordSys = ECord.YZ;
        }
        else
        {
            Debug.LogError("Por favor indique en la informacion del tablero dos coordenadas para el systema de coordenadas");
        }

        // Obtenemos el tamaño de la casilla base y lo guardamos para usarlo mas tarde
        Vector3 tileSize = Tile.GetComponent<Renderer>().bounds.size;
        tileSize1 = tileSize.x;
        tileSize2 = tileSize.z;
        tileSize3 = tileSize.y;

        // Obtenemos todas las casillas de la escena activas
        Object[] boardTiles = FindObjectsOfType(typeof(BTile));

        // Obtenemos la coordenada de la superficie para poder spawnear particulas mas tarde en esta
        BTile aux = (BTile) boardTiles[0];
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

        //Guardamos la posicion minima para usarla mas tarde
        min1 = firstPositions.Min();
        min2 = secondPositions.Min();
        max1 = firstPositions.Max();
        max2 = secondPositions.Max();

        //Calculamos el tamaño del tablero a partir de las posiciones y de los tamaños
        size1 = (int)((max1 - min1) / tileSize1) + 1;
        size2 = (int)((max2 - min2) / tileSize2) + 1;

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
                    firstIndex = (int)(position.x - min1 / tileSize1);
                    secondIndex = (int)(position.y - min2 / tileSize2) * size1;
                    // Guardamos en el diccionario el index con su respectiva posicion
                    locations.Add((firstIndex + secondIndex), new Vector2(position.x, position.y));
                    tiles.Add((firstIndex + secondIndex), item);
                    break;
                case ECord.XZ:

                    // Calculamos los indices respecto a las posiciones
                    firstIndex = (int)(position.x - min1 / tileSize1);
                    secondIndex = (int)(position.z - min2 / tileSize2) * size1;
                    // Guardamos en el diccionario el index con su respectiva posicion
                    locations.Add((firstIndex + secondIndex), new Vector2(position.x, position.z));
                    break;
                case ECord.YZ:

                    // Calculamos los indices respecto a las posiciones
                    firstIndex = (int)(position.y - min1 / tileSize1);
                    secondIndex = (int)(position.z - min2 / tileSize2) * size1;
                    // Guardamos en el diccionario el index con su respectiva posicion
                    locations.Add((firstIndex + secondIndex), new Vector2(position.y, position.z));
                    break;
                default:
                    break;
            }

            //Guardamos el indice de la casilla inicial y final
            if(item.currentState == BTile.ETileState.Start)
            {
                startIndex = firstIndex + secondIndex;

            }else if (item.currentState == BTile.ETileState.End)
            {
                endIndex = firstIndex + secondIndex;
            }

            Debug.Log("Indice " + (firstIndex + secondIndex) + " localizacion " + locations[(firstIndex + secondIndex)]);

        }

        // Para cada casilla almacenamos sus bordes usando como referente su indice en el array
        foreach (int index in locations.Keys)
        {
            // Llamamos a la funcion getLocaledges que nos devuelve los bordes del indice
            edges.Add(index, getLocalEdges(index));
        }

        // Añadimos los bordes de las paredes
        addWallsEdges();

        // Ponemos a 0 los bordes del tablero
        setBorderEdges();

    }

    private void Start()
    {
        ShowBoard();

        // Obtenemos la roatacion de los elementos del tablero para cuando spawneemos particulas
        spawnRotation = FindObjectOfType<BPlayer>().transform.rotation;
    }
    #endregion
    
    #region GETTERS

    /// <summary>
    /// Devolvemos los limites del Tablero en un array de 4 componentes
    /// </summary>
    /// <returns> 
    /// Vector4 que contiene la minima coordenada de la primera coordenada y la maxima 
    /// seguida de la minima coordenada de la segunda coordenada y la maxima de la misma 
    /// </returns>
    public Vector4 getBoardLimits()
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
    public int getBoardAp()
    {
        return boardInfo.APNivel;
    }

    /// <summary>
    /// Devuelve el tamaño de la casilla
    /// </summary>
    /// <returns></returns>
    public float getTileSize()
    {
        return tileSize1;
    }

    /// <summary>
    /// Transforma una coordenada en el indice dentro del tablero
    /// </summary>
    /// <param name="position"> Vector 3 Posicion que queremos pasar a index </param>
    /// <returns> Indice correspondiente a la posicion </returns>
    public int positionToIndex(Vector3 position)
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
    /// Devuelve la posicion de spawn para el jugador
    /// </summary>
    /// <param name="playerHeight">Float Altura del jugador</param>
    /// <returns></returns>
    public Vector3 getPlayerSpawnPos(float playerHeight)
    {
        // Obtenemos el vector para decidir hacia donte apunta parte superior
        Vector3 vectorUp = FindObjectOfType<BTile>().transform.up;

        switch (coordSys)
        {
            case ECord.XY:
                return indexToVector(startIndex, null, surfaceCoord + vectorUp.z * playerHeight / 2);
            case ECord.XZ:
                return indexToVector(startIndex, null, surfaceCoord + vectorUp.y * playerHeight / 2);
            case ECord.YZ:
                return indexToVector(startIndex, null, surfaceCoord + vectorUp.x * playerHeight / 2);
            default:
                return vectorUp;
        } 
    }
    /// <summary>
    /// Devuelve la posicion de spawn para el enemigo
    /// </summary>
    /// <param name="pos"> Posicion del waypoint en el que queremos spawnear</param>
    /// <returns></returns>
    public Vector3 getEnemySpawnPos(Vector3 pos,float enemyHeight)
    {
        // Obtenemos el vector para decidir hacia donte apunta parte superior
        Vector3 vectorUp = FindObjectOfType<BTile>().transform.up;

        switch (coordSys)
        {
            case ECord.XY:
                return new Vector3(pos.x, pos.y, surfaceCoord + vectorUp.z * enemyHeight / 2);
            case ECord.XZ:
                return new Vector3(pos.x, surfaceCoord + vectorUp.y * enemyHeight / 2, pos.z);
            case ECord.YZ:
                return new Vector3(surfaceCoord + vectorUp.z * enemyHeight / 2, pos.y, pos.z);
            default:
                return vectorUp;
        }
    }
    /// <summary>
    /// Devuelve el vector que apunta hacia arriba del tablero
    /// </summary>
    /// <returns></returns>
    public Vector3 getBoardUp()
    {
        return FindObjectOfType<BTile>().transform.up;
    }

    /// <summary>
    /// Devuelve la posicion de spawn para el jugador
    /// </summary>
    /// <returns></returns>
    public Quaternion getPlayerSpawnRot()
    {
        GameObject aux = FindObjectOfType<BTile>().gameObject;
        return Quaternion.LookRotation(aux.transform.forward, aux.transform.up);
    }
    /// <summary>
    /// Devuelve el sistema de coordenadas usado
    /// </summary>
    /// <returns></returns>
    public ECord getCoordSys()
    {
        return coordSys;
    }

    /// <summary>
    /// Transforma un indice en una posicion
    /// </summary>
    /// <param name="index">Int Indice de la casilla </param>
    /// <param name="targetObject"> GameObject objeto del que tomaremos la coordenada restante</param>
    /// <param name="extraCoord"> Offset de la coordenada extra en caso de añadir GameObject y en caso coontrario offset que le sumamos</param>
    /// <returns> Vector 3 con las cordenadas bases del tablero y la restante del objeto que recibimos </returns>
    public Vector3 indexToVector(int index, GameObject targetObject = null, float extraCoord = 0f)
    {
        Vector2 aux = locations[index];
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
    /// Obtenemos un diccionario con los 4 indices que lo rodean y sus respectivos costes
    /// </summary>
    /// <param name="index"> Int indica el indice del que queremos obtener su diccionario</param>
    /// <returns> Diccionario con los respectivos costes </returns>
    private Dictionary<int, int> getLocalEdges(int index)
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

    #region BOARD LOGIC
    /// <summary>
    /// Recorre todos los muros y añade sus bordes a los del tablero
    /// </summary>
    private void addWallsEdges()
    {
        // Buscamos todos los muros y los guardamos en un array
        BMuro[] walls = FindObjectsOfType<BMuro>();

        // Realizamos el mismo proceso para cada mur
        foreach (BMuro wall in walls)
        {
            addWallEdges(wall);
        }
    }

    /// <summary>
    /// Añade los bordes de un muro a la casilla
    /// </summary>
    /// <param name="wall"> Muro del que obtenemos los bordes</param>
    private void addWallEdges(BMuro wall)
    {
        // Calculamos su indice
        int index = positionToIndex(wall.transform.position);
        int auxIndex;

        // Para cada uno de las casillas que lo rodea actualizamos el eje en ambos sentidos
        // Al sumarle size1 obtenemos la casilla norte
        auxIndex = index + indexDirections["Up"];
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] *= wall.upEdge;
            edges[auxIndex][index] *= wall.upEdge;
        }

        // Al sumarle uno obtenemos la casilla al oeste
        auxIndex = index + indexDirections["Left"];
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] *= wall.leftEdge;
            edges[auxIndex][index] *= wall.leftEdge;
        }

        // Al restar size1 obtenemos la casilla al sur
        auxIndex = index + indexDirections["Down"];
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] *= wall.downEdge;
            edges[auxIndex][index] *= wall.downEdge;
        }

        // Al restar 1 obtenemos la casilla al este
        auxIndex = index + indexDirections["Right"];
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] *= wall.rightEdge;
            edges[auxIndex][index] *= wall.rightEdge;
        }
    }

    /// <summary>
    /// Devuelve el coste de entrar a objectiveTile desde acutalTile
    /// </summary>
    /// <param name="actualTile"> Int indica el indice de la casilla actual</param>
    /// <param name="objectiveTile"> Int indica el indice de la casilla objetivo </param>
    /// <returns> Int que representa el coste del movimiento</returns>
    public int costToEnter(int actualTile, int objectiveTile, bool activeAbility)
    {
        if (edges.ContainsKey(actualTile) && edges[actualTile].ContainsKey(objectiveTile))
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
            return 0;
        }

    }

    /// <summary>
    /// Cambia los valores de los bordes de las casillas a los bordes a 0
    /// </summary>
    private void setBorderEdges()
    {
        foreach (int index in edges.Keys)
        {
            if((index / size1) == 0)
            {
                int auxIndex = index + indexDirections["Down"];
                edges[index][auxIndex] = 0;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = 0;
                }
            }

            if ((index % size1) == 0)
            {
                int auxIndex = index + indexDirections["Left"];
                edges[index][auxIndex] = 0;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = 0;
                }
            }

            if ((index / size1) == size2)
            {
                int auxIndex = index + indexDirections["Up"];
                edges[index][auxIndex] = 0;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = 0;
                }
            }

            if ((index % size1) == size1-1)
            {
                int auxIndex = index + indexDirections["Right"];
                edges[index][auxIndex] = 0;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = 0;
                }
            }
        }
    }
    #endregion

    #region PARTICLES
    /// <summary>
    /// Spawnea particulas de datos en la casilla de index
    /// </summary>
    /// <param name="index"> Int index indica la casilla en la que spawneamos las particulas</param>
    /// <param name="activeAbility"> Bool indica si tenemos la habilidad de recoger cable activa
    /// ya que hasta que no la tengamos las particulas no deben cambiar la logica de los bordes 
    /// </param>
    public void spawnParticle(int index, bool activeAbility)
    {

        // Comprobamos que no haya una particula ya en esa posicion
        if (!dataParticles.ContainsKey(index))
        {
            // Obtenemos la posicion en la que queremos spawnear
            Vector3 position = indexToVector(index,null,surfaceCoord);

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
    public void despawnParticle(int index)
    {

        // Obtenemos las particulas y la eliminamos del diccionario
        GameObject particle = dataParticles[index];
        dataParticles.Remove(index);

        // Eliminamos la particula de datos
        Destroy(particle);
    }
    
    #endregion
    
    #endregion

    #region Temporal hasta que decidamos como aparecen los niveles

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            HideBoard();
        }
    }

    private void ShowBoard()
    {
        //Obtenemos todas las casillas de la escena
        Object[] boardTiles = FindObjectsOfType(typeof(BTile));

        foreach (BTile item in boardTiles)
        {
            item.ShowTile();
        }
    }

    private void HideBoard()
    {
        //Obtenemos todas las casillas de la escena
        Object[] boardTiles = FindObjectsOfType(typeof(BTile));

        foreach (BTile item in boardTiles)
        {
            item.HideTile();
        }
    }
    #endregion
}
