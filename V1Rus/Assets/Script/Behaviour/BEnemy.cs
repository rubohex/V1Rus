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
    /// Informacion del enemigo
    /// </summary>
    public DEnemyInfo enemyInfo;

    /// Velocidad de rotacion del enemigo
    private float rotationTime;
    /// Velocidad de movimiento del enemigo
    private float moveTime;

    /// Indica si el enemigo moviendose
    private bool isMoving = false;
    /// Indica si el enemigo esta rotando
    private bool isRotating = false;

    /// Indice al que debe ir el enemigo
    private int boardIndex;

    /// Indice del path en el que se encuentra el enemigo
    private int pathIndex;
    /// Indica el sentido del camino en el que vamos para caminos no cerrados
    private int pathDirection = 1;
    /// Quaternion que guarda la rotacion que el enemigo tendra que hacer despues de moverse
    private Quaternion enemyRotation;

    public int verticalVisionSize = 2;
    public int horizontalVisionSize = 3;
    private HashSet<int> visionTiles = new HashSet<int>();

    /// Tablero del nivel
    private BBoard board;

    /// Vector que apunta hacia arriba en el tablero
    private Vector3 boardUP;

    #endregion
    private void Awake()
    {
        rotationTime = enemyInfo.rotationTime;
        moveTime = enemyInfo.moveTime;
        board = FindObjectOfType<BBoard>();
        boardUP = board.getBoardUp();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Altura del enemigo
        float enemyHeight = GetComponent<Renderer>().bounds.size.y;

        // Posicion inicial del jugador en el waypoint 0
        transform.position = board.getEnemySpawnPos(wayPoints[0].position, enemyHeight);
        // Rotacion inicial mirando hacia el waypoint 1
        transform.rotation = Quaternion.LookRotation(wayPoints[1].position-wayPoints[0].position ,boardUP);
        
        // Obtenemos el indice del tablero en el que estamos
        boardIndex = board.positionToIndex(transform.position);
        // Obtenemos el indice del array de waypoints en el que estamos
        pathIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            StartCoroutine(MoveOverTimeCoroutine(gameObject, moveTime, transform.position, board.indexToVector(boardIndex, gameObject)));
        }

        if (isRotating)
        {
            StartCoroutine(RotateOverTimeCoroutine(this.gameObject, rotationTime, transform.rotation, enemyRotation));
        }

        if (Input.GetKeyDown(KeyCode.L) && !isRotating && !isMoving)
        {
            nextMovement();
        }
    }

    /// <summary>
    /// Cuando se llama a este metodo el enemigo hace su siguiente movimiento mirando al siguiente wayPoint
    /// </summary>
    public void nextMovement()
    {
        // Obtenemos el Indice al que queremos movernos
        pathIndex = computeNextWayPointsIndex(pathIndex);
        boardIndex = board.positionToIndex(wayPoints[pathIndex].position);

        // Actualizamos la variable para indicar que el enemigo se esta moviendo
        isMoving = true;

        // Calculamos el indice al que va a mirar el enemigo al finalizar su movimiento y calculamos la rotacion
        int lookWayPointIndex = computeNextWayPointsIndex(pathIndex);
        enemyRotation = Quaternion.LookRotation(wayPoints[lookWayPointIndex].position - wayPoints[pathIndex].position, boardUP);

    }

    private void ComputeVisionSet()
    {
        // Vaciamos el Set anterior

        // Obtenemos el tamaño basico de la casilla
        float tileSize = board.getTileSize();

        // Obtenemos el indice de la casilla a la que miramos
        int centralIndex = board.positionToIndex(transform.position + transform.forward* tileSize);

        // Obtenemos la diferencia entre el indice al que apuntamos y el actual
        int rightDiff = Mathf.Abs(centralIndex - boardIndex);

        // Calculamos los limites para los bucles
        int minI;
        int maxI;
        int minJ;
        int maxJ;
        int boardSize1 = (int) board.GetBoardShape()[0];

        if (Mathf.Abs(rightDiff) < boardSize1)
        {
            maxI = boardSize1 * (horizontalVisionSize % 2);
            minI = -maxI;
            maxJ = rightDiff;
            minJ = 0;
            
        }
        else
        {
            maxI = (verticalVisionSize - 1) * rightDiff;
            minI = 0;
            maxJ = horizontalVisionSize % 2;
            minJ = -maxJ;
        }

        for (int i = minI; i < maxI; i++)
        {
            for (int j = minJ; j < maxJ; j++)
            {
                visionTiles.Add(centralIndex + i + j);
            }
        }
    }

    /// <summary>
    /// Calcula el indice del siguiente wayPoin al que debe ir el enemigo
    /// </summary>
    /// <param name="boardIndex">Int Indice del wayPoint actual </param>
    /// <returns> Int Indice del siguiente wayPoint </returns>
    private int computeNextWayPointsIndex(int boardIndex)
    {
        // En caso de que el path no sea observamos los casos de los extremos
        if (!closedPath && boardIndex == wayPoints.Length - 1)
        {
            // En caso de llegar al ultimo waypoint damos la vuelta al sentido
            pathDirection = -1;
            
        }else if (!closedPath && boardIndex == 0)
        {
            // En caso de ser el primer waypoint ponemos el sentido recto
            pathDirection = 1;
        }

        // Aumentamos el index en funcion de la direccion
        int nextIndex = boardIndex + pathDirection;

        // En caso de ser un path cerrado al llegar al final indicamos que el proximo indice es el primero
        if (closedPath && boardIndex == wayPoints.Length - 1)
        {
            nextIndex = 0;
        }

        return nextIndex;
    }

    #region COROUTINES STOP 

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

        isMoving = false;
        isRotating = true;

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
            timer += Time.deltaTime;
            float t = timer / transitionDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            targetObject.transform.rotation = Quaternion.Slerp(start, target, t);

            yield return null;
        }

        isRotating = false;

        yield return null;
    }
    #endregion
}
