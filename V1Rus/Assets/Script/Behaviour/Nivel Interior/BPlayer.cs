using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BPlayer : MonoBehaviour
{

    #region ATRIBUTES
    [Header("Data")]

    /// Material de cursor en casilla
    public Material cursorMaterial;

    /// Material de casilla seleccionada
    public Material selectedMaterial;

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

    /// Game manager del nivel
    private BGameManager gameManager;

    /// Texto del canvas de prueba
    private Text textAp;

    /// Rayo lanzado desde la camara para detectar objetos
    private Ray cameraRay;

    /// Hit del Raycast
    private RaycastHit cameraHit;

    /// Indice de la casilla en la que estaba el cursor anteriormente
    private int previousHit = -1;

    /// Indice de la casilla actual
    private int actualHit = -1;

    /// Camino que seguira el jugador
    private List<int> path = new List<int>();

    #endregion

    #region METHODS

    #region AWAKE START UPDATE

    public void SetupPlayer(BGameManager manager, DPlayerInfo playerInfo)
    {
        // Cargamos los datos basicos del jugador
        rotateLeft = playerInfo.rotateLeft;
        rotateRight = playerInfo.rotateRight;
        move = playerInfo.move;
        rotationTime = playerInfo.rotationTime;
        moveTime = playerInfo.moveTime;
        recogerCable = playerInfo.recogerCable;

        // Cargamos el manager del nivel
        gameManager = manager;

        // Obtenemos el tablero
        board = gameManager.GetActiveBoard();

        // Actualizamos los Ap que tenemos para el nivel
        Ap = maxAP = board.GetBoardAp();

        // Colocamos al jugador en la casilla de salida y guardamos su indice
        tileIndex = board.PositionToIndex(transform.position);

        // Inicializa el camino
        RestartPath();

        // Temporal
        textAp = GameObject.Find("TextApPrueba").GetComponent<Text>();
        textAp.text = "AP: " + Ap;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateUI();
        // Control del giro a la izquierda
        if (Input.GetKeyDown(rotateLeft) && !isMoving)
        {
            StartCoroutine(RotateOverTimeCoroutine(this.gameObject, rotationTime, transform.rotation, Quaternion.LookRotation(-transform.right, transform.up)));
        }

        // Control del giro a la derecha
        if (Input.GetKeyDown(rotateRight) && !isMoving)
        {
            StartCoroutine(RotateOverTimeCoroutine(this.gameObject, rotationTime, transform.rotation, Quaternion.LookRotation(transform.right, transform.up)));
        }

        if (Input.GetMouseButtonDown(0) && !isMoving && !EventSystem.current.IsPointerOverGameObject())
        {
            if(path.Count > 0)
            {
                previousHit = -1;

                StartCoroutine(MakePath());
            }
        }

        // Control movimiento
        if (Input.GetKeyDown(move) && !isMoving)
        {
            // Vector de posicion de la casilla objetivo
            Vector3 objectivePos = board.IndexToPosition(tileIndex) + transform.forward;
            // Obtenemos el indice de la casilla a la que queremos ir a partir de la casilla acutal y la casilla a la que miramos
            int objectiveIndex = board.PositionToIndex(objectivePos);

            // Obtenemos tambien el coste de dicha casilla
            float cost = board.CostToEnter(tileIndex, objectiveIndex, recogerCable);

            // Obsevamos que el coste es distinto de cero
            //maxAP == 0 sería considerado AP infinito(sala del boss)
            if (cost != Mathf.Infinity && (cost <= Ap || maxAP == 0))
            {
                // Informamos de que nos estamos moviendo
                isMoving = true;

                // Llamamos a la corutina para que se encargue del movimiento
                StartCoroutine(MoveOverTimeCoroutine(this.gameObject, moveTime, transform.position, board.IndexToPosition(objectiveIndex, gameObject)));

                // Miramos si la casilla en la que entramos tiene particuals de datos o no esto lo podemos ver con el coste
                if (cost > 0)
                {
                    // Spawneamos las particulas
                    board.SpawnParticle(tileIndex, recogerCable);
                }
                else if (cost < 0 && recogerCable)
                {
                    // Eliminamos la particula
                    board.DespawnParticle(objectiveIndex);
                }

                // Cambiamos a casilla en la que estamos y el coste
                tileIndex = objectiveIndex;
                if (maxAP != 0)
                {
                    ChangeAP((int)-cost);
                }

                gameManager.EnemyTurn();
            }

            // Temporal
            RestartPath();
        }
    }

    void FixedUpdate(){
        
        // Elegimos el rayo que vamos a usar para el raycast
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Hacemos el raycast y vemos si hemos golpeado alguna casilla
        if (Physics.Raycast(cameraRay, out cameraHit) && !EventSystem.current.IsPointerOverGameObject() && !isMoving)
        {
            if (cameraHit.collider.name.Contains("Plataforma"))
            {
                actualHit = board.PositionToIndex(cameraHit.point);

                if (board.isTile(actualHit))
                {
                    if (previousHit != actualHit)
                    {
                        // Vaciamos el camino anterior
                        RestartPath();

                        // Calculamos el camino
                        path.AddRange(board.AStarAlgorithm(path[path.Count - 1], actualHit, recogerCable, false));

                        // Dibujamos el camino
                        DrawPath();

                        // Guardamos el indice de la casilla cambiada
                        previousHit = actualHit;

                    }
                }
                else if (previousHit != -1)
                {
                    // Cambiamos el material de la anterior casillas
                    path.ForEach(tile => board.ResetMaterial(tile));
                    previousHit = -1;
                    path.Clear();
                }
            }
        }
        else if (previousHit != -1 && path.Count > 1)
        {
            path.ForEach(tile => board.ResetMaterial(tile));
            previousHit = -1;
            path.Clear();
        }

        //Temporal activacion de recogerDatos hasta que tengamos la habilidad util para pruebas
        if (Input.GetKeyDown(KeyCode.Space))
        {
            recogerCable = !recogerCable;
        }
    }

    /// <summary>
    /// Funcion usada antes de destruir al jugador
    /// </summary>
    public void DestroyPath()
    {
        path.ForEach(tile => board.ResetMaterial(tile));

        path.Clear();
    }
    #endregion

    #region GETTERS
    /// <summary>
    /// Devuelve el indice del jugador
    /// </summary>
    /// <returns></returns>
    public int GetIndex()
    {
        return tileIndex;
    }
    #endregion

    #region CHANGE STATS
    /// <summary>
    /// Sumamos al Ap actual los puntos que introducimos por parametros
    /// </summary>
    /// <param name="actionPoints"> Int puntos a sumar </param>
    public void ChangeAP(int actionPoints)
    {
        Ap = Mathf.Clamp(Ap + actionPoints, 0, maxAP);
    }

    /// Temporal para debugear ap hasta tener un IU
    public void UpdateUI()
    {
        textAp.text = "AP: " + Ap;

        if (maxAP == 0)
        {
            textAp.text = "AP: " + "inf";
        }
        else if (Ap == 0)
        {
            textAp.color = Color.red;
        }
    }


    #endregion

    #region PATH

    /// <summary>
    /// La funcion vacia el camino y añade el indice actual
    /// </summary>
    private void RestartPath()
    {
        path.ForEach(tile => board.ResetMaterial(tile));

        path.Clear();

        path.Add(tileIndex);
    }

    /// <summary>
    /// La funcion dibuja el camino que lleva hasta el cursor en el tablero
    /// </summary>
    private void DrawPath()
    {
        for (int i = 0; i < path.Count-1; i++)
        {
            board.ChangeTileMaterial(path[i], cursorMaterial);
        }

        board.ChangeTileMaterial(path[path.Count - 1], selectedMaterial);
    }

    #endregion

    #region COROUTINES 

    /// <summary>
    /// Coorutina encargada de realizar todo el movimiento del jugador avisando al game manager cuando se deben mover los enemigos
    /// </summary>
    /// <returns></returns>
    private IEnumerator MakePath()
    {
        // Informamos de que el jugador se esta moviendo
        isMoving = true;

        bool enemyFinish = true;

        board.ResetMaterial(path[0]);

        for (int i = 0; i < path.Count - 1; i++)
        {
            // Obtenemos el indice de la casilla a la que queremos ir a partir de la casilla acutal y la casilla a la que miramos
            int objectiveIndex = path[i + 1];

            // Rotamos hacia dicha casilla si es necesario
            Vector3 lookAt = board.IndexToPosition(objectiveIndex, gameObject);

            // Comprobamos si hace falta girar o no
            if (Vector3.Distance(transform.position+transform.forward,lookAt) > 0.001f)
            {
                yield return StartCoroutine(RotateOverTimeCoroutine(this.gameObject, rotationTime, transform.rotation, Quaternion.LookRotation(lookAt-transform.position, transform.up)));
            }

            // Obtenemos tambien el coste de dicha casilla
            float cost = board.CostToEnter(tileIndex, objectiveIndex, recogerCable);

            if (cost > Ap)
            {
                break;
            }
            else
            {
                // Llamamos a la corutina para que se encargue del movimiento
                yield return StartCoroutine(MoveOverTimeCoroutine(this.gameObject, moveTime, transform.position, board.IndexToPosition(objectiveIndex, gameObject)));

                board.ResetMaterial(objectiveIndex);

                // Miramos si la casilla en la que entramos tiene particuals de datos o no esto lo podemos ver con el coste
                if (cost > 0)
                {
                    // Spawneamos las particulas
                    board.SpawnParticle(tileIndex, recogerCable);
                }
                else if (cost < 0 && recogerCable)
                {
                    // Eliminamos la particula
                    board.DespawnParticle(objectiveIndex);
                }

                // Cambiamos a casilla en la que estamos y el coste
                tileIndex = objectiveIndex;

                if (maxAP != 0)
                {
                    ChangeAP((int)-cost);
                }
            }

            yield return StartCoroutine(gameManager.EnemyTurn());
            yield return new WaitForSeconds(0.05f);
        }

        RestartPath();

        isMoving = false;

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

        yield return null;
    }

    #endregion

    #endregion

}
