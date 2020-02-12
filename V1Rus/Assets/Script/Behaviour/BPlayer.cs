using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    /// Indica si tenemos activa la habilidad para recoger Cable
    private bool recogerCable;

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

    /// Texto del canvas de prueba
    public Text textAp;
    #endregion

    #region METHODS

    #region AWAKE START UPDATE
    private void Awake()
    {
        rotateLeft = playerInfo.rotateLeft;
        rotateRight = playerInfo.rotateRight;
        move = playerInfo.move;
        rotationSpeed = playerInfo.rotationSpeed;
        moveSpeed = playerInfo.moveSpeed;
        recogerCable = playerInfo.recogerCable;
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

        textAp.text = "AP: " + Ap;

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
            // Obtenemos el indice de la casilla a la que queremos ir a partir de la casilla acutal y la casilla a la que miramos
            int objectiveIndex = tileIndex + direction[Mathf.RoundToInt(transform.eulerAngles.y)];

            // Obtenemos tambien el coste de dicha casilla
            int cost = board.costToEnter(tileIndex, objectiveIndex);
            Debug.Log("Coste: " + cost);
            // Obsevamos que el coste es distinto de cero
            //maxAP == 0 sería considerado AP infinito(sala del boss)
            if (cost != 0 && (cost <= Ap || maxAP == 0))
            {
                // Informamos de que nos estamos moviendo
                isMoving = true;

                // Obtenemos el regidbody y le damos una velocidad
                GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
                // Lanzamos una Corrutina para que pare al jugador al llegar al punto
                StartCoroutine(stopMovement(objectiveIndex));

                // Miramos si la casilla en la que entramos tiene particuals de datos o no esto lo podemos ver con el coste
                if (cost > 0)
                {
                    // Spawneamos las particulas
                    board.spawnParticle(tileIndex, recogerCable);
                }
                else if (cost < 0 && recogerCable)
                {
                    // Eliminamos la particula
                    board.despawnParticle(objectiveIndex);
                }

                // Cambiamos a casilla en la que estamos y el coste
                tileIndex = objectiveIndex;
                if(maxAP != 0)
                {
                    changeAP(-cost);
                }

                // Temporal para debugear ap hasta tener un IU
                textAp.text = "AP: " + Ap;

                if(maxAP == 0)
                {
                    textAp.text = "AP: " + "inf";
                }
                else if (Ap == 0)
                {
                    textAp.color = Color.red;
                }
            }
        }

        //Temporal activacion de recogerDatos hasta que tengamos la habilidad util para pruebas
        if (Input.GetKeyDown(KeyCode.Space))
        {
            recogerCable = !recogerCable;
            board.activateDataParticles();
        }
    }
    #endregion
    
    /// <summary>
    /// Sumamos al Ap actual los puntos que introducimos por parametros
    /// </summary>
    /// <param name="actionPoints"> Int puntos a sumar </param>
    public void changeAP(int actionPoints)
    {
        Ap = Mathf.Clamp(Ap + actionPoints, 0, maxAP);
    }

    #region COROUTINES STOP 
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
    #endregion

}
