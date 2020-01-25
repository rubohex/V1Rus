using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BBoard : MonoBehaviour
{
    #region ATTRIBUTES

    // Informacion pertinenete al nivel
    [SerializeField]
    DBoardInfo boardInfo;

    // Obtenemos la casilla basica a partir del prefab para poder tener en cuenta su tamaño
    public GameObject Tile;
    
    // Diccionario para almacenar las localizaciones de las casillas en funcion de los indices de un array
    private readonly Dictionary<int, Vector3> locations = new Dictionary<int, Vector3>();

    //Indice de la casilla incial y de la casilla final
    private int startIndex;
    private int endIndex;

    //Tamaño en casillas del tablero en X 
    private int xSize;
    //Tamaño en casillas del tablero en Z
    private int zSize;
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
        float xMin = xPositions.Min();
        float zMIn = zPositions.Min();

        //Calculamos el tamaño del tablero a partir de las posiciones y de los tamaños
        xSize = (int)((xPositions.Max() - xPositions.Min()) / tileSizeX) + 1;
        zSize = (int)((zPositions.Max() - zPositions.Min()) / tileSizeZ) + 1;

        //Para cada casilla almacenamos su posicion y como referente guardamos su indice en el array
        foreach (BTile item in boardTiles)
        {
            //Calculamos el nuevo indice para guardar en el diccionario de localizaciones
            Vector3 position = item.gameObject.transform.position;
            int xItemIndex = (int) (position.x - xMin / tileSizeX);
            int zItemIndex = (int) (position.z - zMIn / tileSizeZ) * xSize;

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

    public int PositionToIndex(Vector3 position)
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
        if (Input.GetKeyDown(KeyCode.Q))
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
