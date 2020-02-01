using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPlayer : MonoBehaviour
{

    #region ATRIBUTES
    [Header("Data")]

    /// Datos de la Camara
    public DPlayerInfo playerInfo;

    /// Codigo de tecla para rotar -90º
    private KeyCode rotateLeft;
    /// Codigo de tecla para rotar -90º
    private KeyCode rotateRight;
    /// Codigo de tecla para mover hacia arriba
    private KeyCode move;

    /// Ap maximo del jugador
    private int maxAP;
    /// Ap actual del jugador
    private int Ap;

    /// Velocidad de rotacion del jugador
    private float rotationSpeed;
    /// Velocidad de movimiento del jugador
    private float moveSpeed;

    /// Indice de la casilla actual
    private int tileIndex;

    /// Indica si el jugador esta rotando o moviendose
    private bool isMoving = false;

    /// Diccionario para guardar la relacion entre la rotacion y la casilla a la que apuntamos
    private Dictionary<int, int> direction = new Dictionary<int, int>();
    
    /// Tablero del nivel
    private BBoard board;
    #endregion

    #region METHODS

    private void Awake()
    {
        rotateLeft = playerInfo.rotateLeft;
        rotateRight = playerInfo.rotateRight;
        move = playerInfo.move;
        rotationSpeed = playerInfo.rotationSpeed;
        moveSpeed = playerInfo.moveSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<BBoard>();
        Ap = maxAP = board.getBoardAp();
        tileIndex = board.positionToIndex(transform.position);
        int xSize = (int) board.getBoardShape()[0];

        direction.Add(0, xSize);
        direction.Add(90, 1);
        direction.Add(180, -xSize);
        direction.Add(270, -1);

    }

    // Update is called once per frame
    void Update()
    {
        // Control del giro a la izquierda
        if (Input.GetKeyDown(rotateLeft) && !isMoving)
        {
            // Añadimos una velocidad de giro
            GetComponentInParent<Rigidbody>().angularVelocity = new Vector3(0f, -rotationSpeed, 0f);
            isMoving = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(-1));
        }

        // Control del giro a la derecha
        if (Input.GetKeyDown(rotateRight) && !isMoving)
        {
            // Añadimos una velocidad de giro
            GetComponentInParent<Rigidbody>().angularVelocity = new Vector3(0f, rotationSpeed, 0f);
            isMoving = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(1));
        }

        // Control movimiento
        if (Input.GetKeyDown(move) && !isMoving)
        {
            int objectiveIndex = tileIndex + direction[(int) transform.eulerAngles.y];
            int cost = board.costToEnter(tileIndex,objectiveIndex);
            if (cost > 0)
            {
                isMoving = true;
                GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
                StartCoroutine(stopMovement(objectiveIndex));
                tileIndex = objectiveIndex;
                changeAP(-1);
                Debug.Log("Ap restante: "+ Ap );
            }

        }
    }

    /// <summary>
    /// Sumamos al Ap actual los puntos que introducimos por parametros
    /// </summary>
    /// <param name="actionPoints"> Int puntos a sumar </param>
    public void changeAP(int actionPoints)
    {
        Ap = Mathf.Clamp(Ap + actionPoints, 0, maxAP);
    }

    /// <summary>
    /// Corrutina encargada de controlar que la rotacion sea de 90 grados
    /// </summary>
    /// <param name="direction"> -1 o 1 dependiendo del sentido de la rotacion </param>
    IEnumerator stopRotation(int direction)
    {
        // Guardamos la rotacion anterior
        Vector3 prevRotation = transform.eulerAngles;

        // Calculamos el tiempo que tenemos que esperar en funcion a la velocidad de rotacion
        float timeToWait = (Mathf.PI / 2) / rotationSpeed;
        // Esperamos el tiempo necesario
        yield return new WaitForSeconds(timeToWait);

        // Paramos el giro
        GetComponentInParent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        // Redondeamos la rotacion para que sea exacta
        transform.eulerAngles = prevRotation + new Vector3(0, direction * 90, 0);
        // Informamos que ha parado la rotacion
        isMoving = false;
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
        float timeToWait = Vector3.Magnitude(objectivePos-transform.position) / moveSpeed;

        // Esperamos hasta obtener el angulo deseado
        yield return new WaitForSeconds(timeToWait);
        
        // Paramos el giro
        GetComponentInParent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        // Redondeamos la rotacion para que sea exacta
        transform.position = objectivePos;
        // Informamos que ha parado la rotacion
        isMoving = false;
    }
    #endregion

}
