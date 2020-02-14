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
    private ECord cordSys;

    /// Diccionario para almacenar las localizaciones de las casillas en funcion de los indices de un array
    private Dictionary<int, Vector2> locations = new Dictionary<int, Vector2>();

    /// Diccionario para almacena los costes de los bordes con respecto a cada casilla
    private Dictionary<int, Dictionary<int, int>> edges = new Dictionary<int, Dictionary<int, int>>();

    /// Diccionario para almacenar las particulas de datos
    private Dictionary<int, GameObject> dataParticles = new Dictionary<int, GameObject>();

    /// Indice de la casilla incial y de la casilla final
    private int startIndex;
    private int endIndex;

    /// Tamaño en casillas del tablero en X 
    private int size1;
    /// Tamaño en casillas del tablero en Z
    private int size2;

    /// Limites del mapa en Eje X y Z
    private float min1;
    private float max1;
    private float min2;
    private float max2;

    /// Tamaño de las casillas 
    private float tileSize1;
    private float tileSize2;

    #endregion

    #region METHODS

    #region AWAKE START UPDATE
    private void Awake()
    {
        // Primero observamos el sistema de coordenadas que vamos a utilizar
        // El sistema elegido dependera de las coordenadas que provengan del boardInfo
        if(boardInfo.x && boardInfo.y)
        {
            cordSys = ECord.XY;
        }
        else if(boardInfo.x && boardInfo.z)
        {
            cordSys = ECord.XZ;
        }
        else if(boardInfo.y && boardInfo.z)
        {
            cordSys = ECord.YZ;
        }
        else
        {
            Debug.LogError("Por favor indique en la informacion del tablero dos coordenadas para el systema de coordenadas");
        }

        //Obtenemos el tamaño de la casilla base y lo guardamos para usarlo mas tarde
        Vector3 tileSize = Tile.GetComponent<Renderer>().bounds.size;
        tileSize1 = tileSize.x;
        tileSize2 = tileSize.z;

        //Obtenemos todas las casillas de la escena activas
        Object[] boardTiles = FindObjectsOfType(typeof(BTile));
        
        //A partir de todas estas casillas obtenemos sus posiciones en el sistema
        List<float> firstPositions = new List<float>();
        List<float> secondPositions = new List<float>();
        foreach (BTile item in boardTiles)
        {
            // Dependiendo del sistema de coordenadas guardamos unos valores diferentes
            switch (cordSys)
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

        //Para cada casilla almacenamos su posicion y como referente guardamos su indice en el array
        foreach (BTile item in boardTiles)
        {
            //Calculamos el nuevo indice para guardar en el diccionario de localizaciones
            Vector3 position = item.gameObject.transform.position;
            int firstIndex = 0;
            int secondIndex = 0; 

            switch (cordSys)
            {
                case ECord.XY:

                    // Calculamos los indices respecto a las posiciones
                    firstIndex = (int)(position.x - min1 / tileSize1);
                    secondIndex = (int)(position.y - min2 / tileSize2) * size1;
                    // Guardamos en el diccionario el index con su respectiva posicion
                    locations.Add((firstIndex + secondIndex), new Vector2(position.x, position.y));
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

        // Premaramos el diccionario con las diferentes direcciones
        indexDirections.Add("Up", size1);
        indexDirections.Add("Down", -size1);
        indexDirections.Add("Left", -1);
        indexDirections.Add("Right", 1);
    }

    private void Start()
    {
        ShowBoard();
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
    public Vector2 getBoardShape()
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
    /// Transforma una coordenada en el indice dentro del tablero
    /// </summary>
    /// <param name="position"> Vector 3 Posicion que queremos pasar a index </param>
    /// <returns> Indice correspondiente a la posicion </returns>
    public int positionToIndex(Vector3 position)
    {
        switch (cordSys)
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
    /// <param name="index"> Indice de la casilla </param>
    /// <returns></returns>
    public Vector2 indexToVector(int index)
    {
        return locations[index];
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
        auxIndex = index + size1;
        if (locations.ContainsKey(auxIndex))
        {
            localEdges.Add(auxIndex, boardInfo.northEdge);
        }
        else
        {
            localEdges.Add(auxIndex, 0);
        }

        // Al sumarle uno obtenemos la casilla al este
        auxIndex = index + 1;
        if (locations.ContainsKey(auxIndex))
        {
            localEdges.Add(auxIndex, boardInfo.eastEdge);
        }
        else
        {
            localEdges.Add(auxIndex, 0);
        }

        // Al restar size1 obtenemos la casilla al sur
        auxIndex = index - size1;
        if (locations.ContainsKey(auxIndex))
        {
            localEdges.Add(auxIndex, boardInfo.northEdge);
        }
        else
        {
            localEdges.Add(auxIndex, 0);
        }

        // Al restar 1 obtenemos la casilla al oeste
        auxIndex = index - 1;
        if (locations.ContainsKey(auxIndex))
        {
            localEdges.Add(auxIndex, boardInfo.northEdge);
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
        auxIndex = index + size1;
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] *= wall.northEdge;
            edges[auxIndex][index] *= wall.northEdge;
        }

        // Al sumarle uno obtenemos la casilla al este
        auxIndex = index + 1;
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] *= wall.eastEdge;
            edges[auxIndex][index] *= wall.eastEdge;
        }

        // Al restar size1 obtenemos la casilla al sur
        auxIndex = index - size1;
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] *= wall.southEdge;
            edges[auxIndex][index] *= wall.southEdge;
        }

        // Al restar 1 obtenemos la casilla al oeste
        auxIndex = index - 1;
        if (locations.ContainsKey(auxIndex))
        {
            edges[index][auxIndex] *= wall.westEdge;
            edges[auxIndex][index] *= wall.westEdge;
        }
    }

    /// <summary>
    /// Devuelve el coste de entrar a objectiveTile desde acutalTile
    /// </summary>
    /// <param name="actualTile"> Int indica el indice de la casilla actual</param>
    /// <param name="objectiveTile"> Int indica el indice de la casilla objetivo </param>
    /// <returns> Int que representa el coste del movimiento</returns>
    public int costToEnter(int actualTile, int objectiveTile)
    {
        if (edges.ContainsKey(actualTile) && edges[actualTile].ContainsKey(objectiveTile))
        {
            return edges[actualTile][objectiveTile];
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
                int auxIndex = index - size1;
                edges[index][auxIndex] = 0;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = 0;
                }
            }

            if ((index % size1) == 0)
            {
                int auxIndex = index - 1;
                edges[index][auxIndex] = 0;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = 0;
                }
            }

            if ((index / size1) == size2)
            {
                int auxIndex = index + size1;
                edges[index][auxIndex] = 0;
                if (edges.ContainsKey(auxIndex))
                {
                    edges[auxIndex][index] = 0;
                }
            }

            if ((index % size1) == size1-1)
            {
                int auxIndex = index + 1;
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
            Vector2 position = indexToVector(index);

            // Spawneamos las particulas de datos
            GameObject particle = Instantiate(dataParticle, new Vector3(position.x, 0f, position.y), Quaternion.identity);

            // Añadimos los bordes de la particula al tablero
            if (activeAbility)
            {
                addWallEdges(particle.GetComponent<BMuro>());
            }

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
        // Obtenemos la posicion en la que queremos spawnear
        Vector2 position = indexToVector(index);

        // Obtenemos las particulas y la eliminamos del diccionario
        GameObject particle = dataParticles[index];
        dataParticles.Remove(index);

        // Devolvemos los bordes a su estado anterior
        addWallEdges(particle.GetComponent<BMuro>());

        // Eliminamos la particula de datos
        Destroy(particle);
    }

    /// <summary>
    /// Activa las particulas de datos inactivas hasta el momento activando los bordes
    /// </summary>
    public void activateDataParticles()
    {
        foreach (GameObject dataPart in dataParticles.Values)
        {
            addWallEdges(dataPart.GetComponent<BMuro>());
        }
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
