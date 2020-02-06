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
    
    /// Diccionario para almacenar las localizaciones de las casillas en funcion de los indices de un array
    private Dictionary<int, Vector2> locations = new Dictionary<int, Vector2>();

    /// Diccionario para almacena los costes de los bordes con respecto a cada casilla
    private Dictionary<int, Dictionary<int, int>> edges = new Dictionary<int, Dictionary<int, int>>();

    /// Indice de la casilla incial y de la casilla final
    private int startIndex;
    private int endIndex;

    /// Tamaño en casillas del tablero en X 
    private int xSize;
    /// Tamaño en casillas del tablero en Z
    private int zSize;

    /// Limites del mapa en Eje X y Z
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;
    #endregion

    #region METHODS

    private void Awake()
    {
        //Obtenemos el tamaño de la casilla base y lo guardamos para usarlo mas tarde
        Vector3 tileSize = Tile.GetComponent<Renderer>().bounds.size;
        float tileSizeX = tileSize.x;
        float tileSizeZ = tileSize.z;

        //Obtenemos todas las casillas de la escena
        Object[] boardTiles = FindObjectsOfType(typeof(BTile));
        
        //A partir de todas estas casillas obtenemos sus posiciones en x y z
        List<float> xPositions = new List<float>();
        List<float> zPositions = new List<float>();
        foreach (BTile item in boardTiles)
        {
            xPositions.Add(item.gameObject.transform.position.x);
            zPositions.Add(item.gameObject.transform.position.z);
        }

        //Guardamos la posicion minima para usarla mas tarde
        minX = xPositions.Min();
        minZ = zPositions.Min();
        maxX = xPositions.Max();
        maxZ = zPositions.Max();

        //Calculamos el tamaño del tablero a partir de las posiciones y de los tamaños
        xSize = (int)((maxX - minX) / tileSizeX) + 1;
        zSize = (int)((maxZ - minZ) / tileSizeZ) + 1;

        //Para cada casilla almacenamos su posicion y como referente guardamos su indice en el array
        foreach (BTile item in boardTiles)
        {
            //Calculamos el nuevo indice para guardar en el diccionario de localizaciones
            Vector3 position = item.gameObject.transform.position;
            int xItemIndex = (int) (position.x - minX / tileSizeX);
            int zItemIndex = (int) (position.z - minZ / tileSizeZ) * xSize;

            //Debug.Log("x " +xItemIndex + " z" + zItemIndex+ " " + position);

            locations.Add((xItemIndex + zItemIndex), new Vector2(position.x,position.z));

            //Guardamos el indice de la casilla inicial y final
            if(item.currentState == BTile.ETileState.Start)
            {
                startIndex = xItemIndex + zItemIndex;

            }else if (item.currentState == BTile.ETileState.End)
            {
                endIndex = xItemIndex + zItemIndex;
            }

            Debug.Log("Indice " + (xItemIndex + zItemIndex) + " localizacion " + locations[(xItemIndex + zItemIndex)]);

        }

        // Para cada casilla almacenamos sus bordes usando como referente su indice en el array
        foreach (int index in locations.Keys)
        {
            // Llamamos a la funcion getLocaledges que nos devuelve los bordes del indice
            edges.Add(index, getLocalEdges(index));
        }

        addWallEdges();

    }

    /// <summary>
    /// Devolvemos los limites del Tablero en un array de 4 componentes
    /// </summary>
    /// <returns> Vector4 que contiene la minima coordenada de x y la maxima 
    /// seguida de la minima coordenada de z y la maxima </returns>
    public Vector4 getBoardLimits()
    {
        return new Vector4(minX,maxX,minZ,maxZ);
    }
    
    /// <summary>
    /// Devolvemos el tamaño del tablero en casillas
    /// </summary>
    /// <returns> Vector2 que contiene xSize seguido de zSize </returns>
    public Vector2 getBoardShape()
    {
        return new Vector2(xSize, zSize);
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
    /// <param name="position"> Vector 3 debe ser el centro de una casilla </param>
    /// <returns> Indice correspondiente a la posicion </returns>
    public int positionToIndex(Vector3 position)
    {

        return locations.FirstOrDefault(x => x.Value == new Vector2(position.x,position.z)).Key;
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
    /// Recorre todos los muros y añade sus bordes a los del tablero
    /// </summary>
    private void addWallEdges()
    {
        // Buscamos todos los muros y los guardamos en un array
        BMuro[] walls = FindObjectsOfType<BMuro>();

        // Realizamos el mismo proceso para cada mur
        foreach (BMuro wall in walls)
        {
            // Calculamos su indice
            int index = positionToIndex(wall.transform.position);
            int auxIndex;

            // Para cada uno de las casillas que lo rodea actualizamos el eje en ambos sentidos
            // Al sumarle xSize obtenemos la casilla norte
            auxIndex = index + xSize;
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

            // Al restar xSize obtenemos la casilla al sur
            auxIndex = index - xSize;
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
    }

    /// <summary>
    /// Obtenemos un diccionario con los 4 indices que lo rodean y sus respectivos costes
    /// </summary>
    /// <param name="index"> Int indica el indice del que queremos obtener su diccionario</param>
    /// <returns> Diccionario con los respectivos costes </returns>
    private Dictionary<int,int> getLocalEdges(int index)
    {
        // Diccionario que llenaremos con la informacion pertinente
        Dictionary<int, int> localEdges = new Dictionary<int, int>();
        // Variable auxiliar para guardar el indice que estudiamos en cada momento
        int auxIndex;

        // Calculamos todos los ejes a partir de los ejes basicos guardados en boardInfo
        // Al sumarle xSize obtenemos la casilla norte
        auxIndex = index + xSize;
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

        // Al restar xSize obtenemos la casilla al sur
        auxIndex = index - xSize;
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
    #endregion

    #region Temporal hasta que decidamos como aparecen los niveles
    // Start is called before the first frame update
    void Start()
    {
        ShowBoard();
    }

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
