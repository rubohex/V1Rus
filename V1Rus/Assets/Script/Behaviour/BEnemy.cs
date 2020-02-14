using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEnemy : MonoBehaviour
{
    #region ATRIBUTES

    public Transform[] wayPoints;
    public bool closedPath;

    public DEnemyInfo enemyInfo;

    /// Velocidad de rotacion del enemigo
    private float rotationSpeed;
    /// Velocidad de movimiento del enemigo
    private float moveSpeed;

    /// Indica si el enemigo moviendose
    private bool isMoving = false;
    /// Indica si el enemigo esta rotando
    private bool isRotating = false;

    /// Indice en el que se encuentra el enemigo
    private int actualIndex;
    /// Indice al que debe ir el enemigo
    private int objectiveIndex;

    /// Indice del path en el que se encuentra el enemigo
    private int pathActualIndex;
    /// Indica el sentido del camino en el que vamos para caminos no cerrados
    private int pathDirection = 1;
    /// Quaternion que guarda la rotacion que el enemigo tendra que hacer despues de moverse
    private Quaternion enemyRotation;

    /// Tablero del nivel
    private BBoard board;

    #endregion
    private void Awake()
    {
        rotationSpeed = enemyInfo.rotationSpeed;
        moveSpeed = enemyInfo.moveSpeed;
        board = FindObjectOfType<BBoard>();
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(wayPoints[0].position.x, transform.position.y, wayPoints[0].position.z);
        transform.rotation = Quaternion.LookRotation(wayPoints[1].position-wayPoints[0].position);
        actualIndex = board.positionToIndex(transform.position);
        pathActualIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // Obtenemos el regidbody y le damos una velocidad
            GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
            // Lanzamos una Corrutina para que pare al enemigo al llegar al punto
            StartCoroutine(stopMovement(objectiveIndex));
        }

        if (isRotating)
        {
            Debug.Log("PUM DAMAGES");
            StartCoroutine(RotateOverTimeCoroutine(this.gameObject, .5f, transform.rotation, enemyRotation));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            nextMovement();
        }
    }

    public void nextMovement()
    {
        // Obtenemos el Indice al que queremos movernos
        pathActualIndex = computeNextWayPointsIndex(pathActualIndex);
        objectiveIndex = board.positionToIndex(wayPoints[pathActualIndex].position);
        // Actualizamos la variable para indicar que el enemigo se esta moviendo
        isMoving = true;

        int lookWayPointIndex = computeNextWayPointsIndex(pathActualIndex);
        enemyRotation = Quaternion.LookRotation(wayPoints[lookWayPointIndex].position - wayPoints[pathActualIndex].position);
        
    }

    /// <summary>
    /// Calcula el indice del siguiente wayPoin al que debe ir el enemigo
    /// </summary>
    /// <param name="actualIndex">Int Indice del wayPoint actual </param>
    /// <returns> Int Indice del siguiente wayPoint </returns>
    private int computeNextWayPointsIndex(int actualIndex)
    {
        // En caso de que el path no sea observamos los casos de los extremos
        if (!closedPath && actualIndex == wayPoints.Length - 1)
        {
            // En caso de llegar al ultimo waypoint damos la vuelta al sentido
            Debug.Log("HAllo");
            pathDirection = -1;
            
        }else if (!closedPath && actualIndex == 0)
        {
            // En caso de ser el primer waypoint ponemos el sentido recto
            pathDirection = 1;
        }

        // Aumentamos el index en funcion de la direccion
        int nextIndex = actualIndex + pathDirection;

        // En caso de ser un path cerrado al llegar al final indicamos que el proximo indice es el primero
        if (closedPath && actualIndex == wayPoints.Length - 1)
        {
            nextIndex = 0;
        }

        return nextIndex;
    }

    #region COROUTINES STOP 
    private IEnumerator RotateOverTimeCoroutine(GameObject targetObject, float transitionDuration, Quaternion start, Quaternion target)
    {
        float timer = 0.0f;

        Debug.Log("He entrado puto");

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

    /// <summary>
    /// Corrutina encargada de controlar que el movimiento sea de una sola casilla
    /// </summary>
    /// <param name="direction"> -1 o 1 dependiendo del sentido de la rotacion </param>
    IEnumerator stopMovement(int objectiveInd)
    {
        Vector2 aux = board.indexToVector(objectiveInd);
        Vector3 objectivePos = new Vector3(aux.x, transform.position.y, aux.y);

        // Calculamos el tiempo que tenemos que esperar
        float timeToWait = Vector3.Magnitude(objectivePos - transform.position) / moveSpeed;

        // Esperamos hasta obtener el angulo deseado
        yield return new WaitForSeconds(timeToWait);

        // Paramos el giro
        GetComponentInParent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        // Redondeamos la rotacion para que sea exacta
        transform.position = objectivePos;
        // Informamos que ha parado la rotacion
        isMoving = false;

        // Comenzamos la rotacion
        isRotating = true;
    }

    #endregion
}
