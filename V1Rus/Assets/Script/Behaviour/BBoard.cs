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
    DBoardInfo boardInfo;

    /// <summary>
    /// Prefab con la casilla basica
    /// </summary>
    public GameObject Tile;
    
    /// Diccionario para almacenar las localizaciones de las casillas en funcion de los indices de un array
    private readonly Dictionary<int, Vector3> locations = new Dictionary<int, Vector3>();

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

            locations.Add((xItemIndex + zItemIndex), position);

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

        Debug.Log("StartIndex " + startIndex + " EndIndex " + endIndex);
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
    /// Transforma una coordenada en el indice dentro del tablero
    /// </summary>
    /// <param name="position"> Vector 3 debe ser el centro de una casilla </param>
    /// <returns name="indice"> Indice correspondiente a la posicion </returns>
    public int positionToIndex(Vector3 position)
    {
        return locations.FirstOrDefault(x => x.Value == position).Key;
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
