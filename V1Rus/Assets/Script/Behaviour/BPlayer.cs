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
    private float rotationTime;
    /// Velocidad de movimiento del jugador
    private float moveTime;

    /// Indice de la casilla actual
    private int tileIndex;

    /// Indica si el jugador esta rotando o moviendose
    private bool isMoving = false;
    
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
        rotationTime = playerInfo.rotationTime;
        moveTime = playerInfo.moveTime;
        recogerCable = playerInfo.recogerCable;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Obtenemos el tablero
        board = FindObjectOfType<BBoard>();
        // Actualizamos los Ap que tenemos para el nivel
        Ap = maxAP = board.getBoardAp();

        // Colocamos al jugador en la casilla de salida y guardamos su indice
        transform.rotation = board.getPlayerSpawnRot();
        transform.position = board.getPlayerSpawnPos(GetComponent<Renderer>().bounds.size.y);
        tileIndex = board.positionToIndex(transform.position);

        // Temporal
        textAp.text = "AP: " + Ap;

    }

    // Update is called once per frame
    void Update()
    {
        // Control del giro a la izquierda
        if (Input.GetKeyDown(rotateLeft) && !isMoving)
        {
            StartCoroutine(RotateOverTimeCoroutine(this.gameObject, rotationTime, transform.rotation, Quaternion.LookRotation(-transform.right,transform.up)));
        }

        // Control del giro a la derecha
        if (Input.GetKeyDown(rotateRight) && !isMoving)
        {
            StartCoroutine(RotateOverTimeCoroutine(this.gameObject, rotationTime, transform.rotation, Quaternion.LookRotation(transform.right, transform.up)));
        }

        // Control movimiento
        if (Input.GetKeyDown(move) && !isMoving)
        {
            // Vector de posicion de la casilla objetivo
            Vector3 objectivePos = board.indexToVector(tileIndex) + transform.forward;
            // Obtenemos el indice de la casilla a la que queremos ir a partir de la casilla acutal y la casilla a la que miramos
            int objectiveIndex = board.positionToIndex(objectivePos);

            // Obtenemos tambien el coste de dicha casilla
            int cost = board.costToEnter(tileIndex, objectiveIndex, recogerCable);

            // Debug.Log("Coste: " + cost);
            
            // Obsevamos que el coste es distinto de cero
            //maxAP == 0 sería considerado AP infinito(sala del boss)
            if (cost != 0 && (cost <= Ap || maxAP == 0))
            {
                // Informamos de que nos estamos moviendo
                isMoving = true;

                // Llamamos a la corutina para que se encargue del movimiento
                StartCoroutine(MoveOverTimeCoroutine(this.gameObject, moveTime, transform.position, board.indexToVector(objectiveIndex, gameObject)));

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
        }
    }
    #endregion

    #region CHANGE STATS
    /// <summary>
    /// Sumamos al Ap actual los puntos que introducimos por parametros
    /// </summary>
    /// <param name="actionPoints"> Int puntos a sumar </param>
    public void changeAP(int actionPoints)
    {
        Ap = Mathf.Clamp(Ap + actionPoints, 0, maxAP);
    }
    #endregion

    #region COROUTINES 

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

            // Hacemos un Lerp en funcion de la rotacion objetivo la original y el procentaje de tiempo que ha pasado
            targetObject.transform.rotation = Quaternion.Slerp(start, target, t);

            yield return null;
        }

        isMoving = false;

        yield return null;
    }

    #endregion

    #endregion

}
