using PowerUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEnemy : MonoBehaviour
{
    #region ATRIBUTES
    /// <summary>
    /// Array con los waypoints por los que ha de pasar el enemigo
    /// </summary>
    public Transform[] wayPoints;

    /// <summary>
    /// Booleano que marca si el camino es cerrado o no
    /// </summary>
    public bool closedPath;

    /// <summary>
    /// Tamaño vertical de la zona de vision
    /// </summary>
    public int verticalVisionSize = 2;

    /// <summary>
    /// Tamaño horizontal de la zona de vision
    /// </summary>
    public int horizontalVisionSize = 3;

    /// <summary>
    /// Informacion del enemigo
    /// </summary>
    public DEnemyInfo enemyInfo;

    /// <summary>
    /// Material de la zona de vision
    /// </summary>
    public Material visionMaterial;

    /// <summary>
    /// Material normal de una casilla
    /// </summary>
    public Material normalMaterial;


    /// Velocidad de rotacion del enemigo
    private float rotationTime;

    /// Velocidad de movimiento del enemigo
    private float moveTime;

    /// Indica si el enemigo moviendose
    private bool isMoving;

    /// Indice al que debe ir el enemigo
    private int boardIndex;

    /// Indice del path en el que se encuentra el enemigo
    private int pathIndex;

    /// Indica el sentido del camino en el que vamos para caminos no cerrados
    private int pathDirection;

    /// Vector que apunta hacia arriba en el tablero
    private Vector3 boardUP;

    /// Conjunto con el indice de las casillas dentro de la vision
    private HashSet<int> visionTiles = new HashSet<int>();

    /// Camino que va a seguir el enemigo
    private List<Vector3> path = new List<Vector3>();

    /// GameManager del nivel
    private BGameManager gameManager;

    /// Tablero del nivel
    private BBoard board;

    /// Variable para indicar si la partida ha acabado o no
    private bool gameOver;

    #endregion

    #region AWAKE START UPDATE
    private void Awake()
    {
        rotationTime = enemyInfo.rotationTime;
        moveTime = enemyInfo.moveTime;
    }

    public void SetupEnemy(BGameManager gameManager)
    {
        // Guardamos el gameManager
        this.gameManager = gameManager;

        // Obtenemos el tablero
        board = gameManager.GetActiveBoard();

        // Obtenemos el vector hacia arriba del tablero
        boardUP = board.GetBoardUp();

        // Posicion inicial del jugador en el waypoint 0
        transform.position = board.GetEnemySpawnPos(wayPoints[0].position, GetComponent<Renderer>().bounds.size);

        // Obtenemos el camino que tendra que recorrer el enemigo
        CreatePath();

        // Rotacion inicial mirando hacia el waypoint 1
        transform.rotation = Quaternion.LookRotation(path[1] - path[0], boardUP);

        // Obtenemos el indice del tablero en el que estamos
        boardIndex = board.PositionToIndex(transform.position);

        // Obtenemos el indice del array de waypoints en el que estamos
        pathIndex = 0;

        // Iniciamos la variable del pathDirection
        pathDirection = 1;

        // Definimos que el enemigo no se esta moviendo
        isMoving = false;

        // Definimos que la partida no se ha acabado todavia
        gameOver = false;

    }

    public void SetupEnemyVision()
    {
        // Calculamos la zona de vision
        ComputeVisionSet();

        // Cambiamos los materiales de las casillas de vision del enemigo
        ChangeVisionRangeMaterial(visionMaterial);
    }

    /// <summary>
    /// Funcion encargada de eliminar la vision del enemigo y sus estructuras de informacion para cuando se complete el tablero
    /// </summary>
    public void EndEnemy()
    {
        // Eliminamos el campo de vision y lo vaciamos
        ResetVisionRange();

        // Vaciamos el path
        path.Clear();

        //Marcamos el gameOVer
        setGameOver(true);
    }


    #endregion

    #region CONTROL

    /// <summary>
    /// Funcion que indica si la partida se ha acabado
    /// </summary>
    /// <param name="gOver"> Booleano que se asignara a gameOver </param>
    public void setGameOver(bool gOver)
    {
        gameOver = gOver;
    }

    /// <summary>
    /// Funcion encargada de activar al enemigo. Sera llamada desde la centralita
    /// </summary>
    public void EnableEnemy()
    {
        // Cambiamos los materiales de las casillas de vision del enemigo
        ChangeVisionRangeMaterial(visionMaterial);

        // Informamos al manager de que este enemigo se ha activado
        gameManager.EnableEnemy(this);
    }

    /// <summary>
    /// Funcion encargada de desactivar al enemigo.
    /// </summary>
    public void DisableEnemy()
    {
        // Eliminamos el campo de vision y lo vaciamos
        ResetVisionRange();
        visionTiles.Clear();

        // Informamos al manager de que este enemigo se ha desactivado
        gameManager.DisableEnemy(this);
    }

    #endregion

    #region GETTERS
    /// <summary>
    /// Devuelve el indice de la casilla en la que se encentra el enemigo
    /// </summary>
    /// <returns>Indice de la casilla sobre la que esta el enemigo</returns>
    public int GetEnemyIndex()
    {
        return boardIndex;
    }
    #endregion

    #region PATH

    /// <summary>
    /// La funcion se encarga de crear el camino que seguira el enemigo a partir de los waypoints y si el camino es cerrado o no
    /// </summary>
    private void CreatePath()
    {
        // Añadimos la posicion actual como inicial en el camino
        path.Add(transform.position);

        // Para cada waypoint calculamos su camino al siguiente
        for (int i = 0; i < wayPoints.Length - 1; i++)
        {
            // Aplicando el A estrella obtenemos un camino
            List<int> waypointPath = board.AStarAlgorithm(board.PositionToIndex(wayPoints[i].position), board.PositionToIndex(wayPoints[i + 1].position));

            // Añadimos todos estos indices al final de nuestro path
            foreach (int index in waypointPath)
            {
                path.Add(board.IndexToPosition(index, gameObject));
            }
        }

        if (closedPath)
        {
            // Aplicando el A estrella obtenemos un camino
            List<int> waypointPath = board.AStarAlgorithm(board.PositionToIndex(wayPoints[wayPoints.Length - 1].position), board.PositionToIndex(wayPoints[0].position));

            // Añadimos todos estos indices al final de nuestro path
            foreach (int index in waypointPath)
            {
                path.Add(board.IndexToPosition(index, gameObject));
            }

            // Si el camino es cerrado eliminamos el ultimo punto ya que es el inicial
            path.RemoveAt(path.Count - 1);
        }
    }

    /// <summary>
    /// Calcula el indice del siguiente wayPoin al que debe ir el enemigo
    /// </summary>
    /// <param name="boardIndex">Int Indice del wayPoint actual </param>
    /// <returns> Int Indice del siguiente wayPoint </returns>
    private int ComputeNextPathIndex(int boardIndex)
    {
        // En caso de que el path no sea observamos los casos de los extremos
        if (!closedPath && boardIndex == path.Count - 1)
        {
            // En caso de llegar al ultimo waypoint damos la vuelta al sentido
            pathDirection = -1;

        }
        else if (!closedPath && boardIndex == 0)
        {
            // En caso de ser el primer waypoint ponemos el sentido recto
            pathDirection = 1;
        }

        // Aumentamos el index en funcion de la direccion
        int nextIndex = boardIndex + pathDirection;

        // En caso de ser un path cerrado al llegar al final indicamos que el proximo indice es el primero
        if (closedPath && boardIndex == path.Count - 1)
        {
            nextIndex = 0;
        }

        return nextIndex;
    }
    #endregion

    #region VISION
    /// <summary>
    /// Cambia el material de todas las casillas en el rango de vision
    /// </summary>
    /// <param name="newMaterial">Nuevo material que le asignaremos a la casillas</param>
    private void ChangeVisionRangeMaterial(Material newMaterial)
    {
        foreach (int tile in visionTiles)
        {
            board.ChangeTileMaterial(tile, newMaterial);
            board.ChangeTileState(tile, BTile.ETileState.EVision);
        }
    }

    /// <summary>
    /// Devuelve el material de todas las casillas del rango de vision a su estado anterior
    /// </summary>
    private void ResetVisionRange()
    {
        foreach (int tile in visionTiles)
        {
            board.RemoveMaterial(tile,visionMaterial);
            board.RemoveState(tile,BTile.ETileState.EVision);
        }
    }

    /// <summary>
    /// La funcion crea el conjunto de casillas que forman la vision
    /// </summary>
    private void ComputeVisionSet()
    {
        // Vaciamos el Set anterior
        visionTiles.Clear();

        // Obtenemos el tamaño basico de la casilla
        float tileSize = board.GetTileSize();

        // Obtenemos el indice de la casilla a la que miramos
        int centralIndex = board.PositionToIndex(transform.position + transform.forward* tileSize);

        // Obtenemos la diferencia entre el indice al que apuntamos y el actual
        int centerDiff = centralIndex - boardIndex;

        // Calculamos los limites para los bucles
        int minI;
        int maxI;
        int minJ;
        int maxJ;
        int boardSize1 = (int) board.GetBoardShape()[0];

        if (Mathf.Abs(centerDiff) < boardSize1)
        {
            // Definimos los bordes del tablero
            int clampMaxJ = boardSize1 - 1 - (centralIndex % boardSize1);
            int clampMinJ = -(centralIndex % boardSize1);

            maxI = horizontalVisionSize / 2;
            minI = -horizontalVisionSize / 2;
            maxJ = Mathf.Clamp((verticalVisionSize - 1) * centerDiff, clampMinJ, clampMaxJ);
            minJ = 0;

            if (minJ > maxJ)
            {
                minJ = maxJ;
                maxJ = 0;
            }

        }
        else
        {
            // Definimos los bordes del tablero
            int clampMaxJ = boardSize1 - 1 - (centralIndex % boardSize1);
            int clampMinJ = -(centralIndex % boardSize1);

            maxJ = Mathf.Clamp(horizontalVisionSize / 2, clampMinJ, clampMaxJ);
            minJ = Mathf.Clamp(-horizontalVisionSize / 2, clampMinJ, clampMaxJ);
            maxI = (verticalVisionSize - 1) * (int)Mathf.Sign(centerDiff);
            minI = 0;

            if (minI > maxI)
            {
                minI = maxI;
                maxI = 0;
            }
        }

        int borderCheck = Mathf.Abs(centralIndex % boardSize1 - boardIndex % boardSize1);

        if (centralIndex >= 0 && borderCheck != boardSize1-1)
        {
            for (int i = minI; i <= maxI; i++)
            {
                for (int j = minJ; j <= maxJ; j++)
                {
                    int aux = centralIndex + j + i * boardSize1;
                    visionTiles.Add(aux);
                }
            }
        }

        List<int> removeList = new List<int>();

        // Una vez añadidas todas las casillas de vision basicas detectaremos los muros que hay dentro de ellas y eliminaremos los valores de vision que tapan deichos muros
        foreach (int tile in visionTiles)
        {
            if (board.isWall(tile))
            {

                if(Mathf.Abs(centerDiff) < boardSize1)
                {
                    for (int i = 0; i < verticalVisionSize; i++)
                    {
                        removeList.Add(tile + i * (int)Mathf.Sign(centerDiff));
                    }
                    
                }
                else
                {
                    for (int i = 0; i < verticalVisionSize; i++)
                    {
                        removeList.Add(tile + i * boardSize1 * (int)Mathf.Sign(centerDiff));
                    }
                }

            }
        }

        removeList.ForEach(x => visionTiles.Remove(x));
    }

    #endregion

    #region COROUTINES 

    /// <summary>
    /// Cuando se llama a este metodo el enemigo hace su siguiente movimiento mirando al siguiente wayPoint
    /// </summary>
    public IEnumerator NextMovement()
    {
        if (!gameOver)
        {
            // Cambiamos el material de las celdas de vision
            ResetVisionRange();

            // Obtenemos el vector al que miraremos
            Vector3 lookAt = path[ComputeNextPathIndex(pathIndex)];

            // Actualizamos la variable para indicar que el enemigo esta rotando
            isMoving = true;

            // Iniciamos una corutina que se encargara de hacer rotar al enemigo
            if (Vector3.Distance(transform.position + transform.forward, lookAt) > 0.001f)
            {
                yield return StartCoroutine(RotateOverTimeCoroutine(this.gameObject, rotationTime, transform.rotation, Quaternion.LookRotation(lookAt - path[pathIndex], boardUP)));
            }

            // Obtenemos el Indice al que queremos movernos
            pathIndex = ComputeNextPathIndex(pathIndex);

            // Obtenemos el indice del tablero al que nos moveremos
            boardIndex = board.PositionToIndex(path[pathIndex]);

            // Actualizamos la posicion del enemigo
            board.UpdateEnemy(board.PositionToIndex(transform.position), boardIndex);

            // Una vez terminada la rotacion iniciamos una corutina para que el jugador se mueva
            yield return StartCoroutine(MoveOverTimeCoroutine(gameObject, moveTime, transform.position, board.IndexToPosition(boardIndex, gameObject)));

            // Calculamos el nuevo campo de vision
            ComputeVisionSet();

            // Pintamos las casillas del campo de vision
            ChangeVisionRangeMaterial(visionMaterial);
        }
     
        yield return null;
    }

    /// <summary>
    /// Corutina encargada de mover un objeto al lugar indicado en el tiempo indicado
    /// </summary>
    /// <param name="targetObject"> GameObject Objeto que vamos a mover </param>
    /// <param name="transitionDuration"> Float Tiempo que dura la transicion </param>
    /// <param name="start"> Vector3 marca la posicion inicial </param>
    /// <param name="target"> Vector3 marca la posicion final </param>
    private IEnumerator MoveOverTimeCoroutine(GameObject targetObject, float transitionDuration, Vector3 start, Vector3 target)
    {
        // Marcamos que el jugador se esta moviendo
        isMoving = true;

        // Iniciamos el timer a 0
        float timer = 0.0f;

        // Mientras el tiempo sea menor que la duracion de la transicion repetimos
        while (timer < transitionDuration)
        {
            // Aumentamos el tiempo en funcion de el Time.deltaTime
            timer += Time.deltaTime;
            // Calculamos el porcentaje del tiempo que llevamos
            float t = timer / transitionDuration;
            // Transformamos el porcentaje segun una funcion para que la transicion sea suave
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            // Hacemos un Lerp en funcion de la posicion inicial y finla y el porcentaje de tiempo
            targetObject.transform.position = Vector3.Lerp(start, target, t);

            yield return null;
        }

        // Informamos que el enemigo ya no se mueve
        isMoving = false;

        yield return null;

    }

    /// <summary>
    /// Corutina encargada de rotar un objeto a una rotacion indicada en el tiempo indicado
    /// </summary>
    /// <param name="targetObject"> GameObject Objeto que vamos a mover </param>
    /// <param name="transitionDuration"> Float Tiempo que dura la transicion </param>
    /// <param name="start"> Quaternion Marca la rotacion de inicio </param>
    /// <param name="target"> Quaternion Marca la rotacion final </param>
    private IEnumerator RotateOverTimeCoroutine(GameObject targetObject, float transitionDuration, Quaternion start, Quaternion target)
    {
        float timer = 0.0f;

        while (timer < transitionDuration)
        {
            // Aumentamos el tiempo en funcion de el Time.deltaTime
            timer += Time.deltaTime;
            // Calculamos el porcentaje del tiempo que llevamos
            float t = timer / transitionDuration;
            // Transformamos el porcentaje segun una funcion para que la transicion sea suave
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            // Hacemos un Slerp en funcion de la rotacion inicial y final y el porcentaje de tiempo
            targetObject.transform.rotation = Quaternion.Slerp(start, target, t);

            yield return null;
        }

        yield return null;
    }
    #endregion
}
