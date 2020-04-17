using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGameManager : MonoBehaviour
{
    #region ATRIBUTES

    // Enemigos activos actualmente
    private List<BEnemy> activeEnemies = new List<BEnemy>();

    // Muros activos actualmente
    private List<BMuro> activeWalls = new List<BMuro>();

    // Terminales activas actualmente
    private List<BTerminal> activeTerminals = new List<BTerminal>();

    // Puertas activas actualmente
    private List<BPuerta> activeDoors = new List<BPuerta>();

    // Camara activa actualmente
    private BCameraController activeCamera;

    // JUgador activo actualmente
    private BPlayer activePlayer;

    // Board activos actualmente
    private BBoard activeBoard;

    private bool boardCompleted;

    // Debug
    public GameObject[] debugArray;

    // Tiempo de desolucion
    public float maxDisolveTimer;
    public float minDisolveTimer;

    private int iter = 0;

    #endregion

    #region METHODS

    #region START UPDATE SETUP
    // Start is called before the first frame update
    void Start()
    {
        // Obtenemos el primer board 
        activeBoard = debugArray[iter].GetComponent<BBoard>();

        // Hacemos el Setup del Board y de todos sus componentes
        activeBoard.SetupBoard(this);

        // Actualizamos nuestras variables
        UpdateActive();

        // Llamamos a la coorutina encargada de mostrar todos los objetos
        StartCoroutine(LoadObjects());

        boardCompleted = false;
    }

    // Update is called every time per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || boardCompleted)
        {
            boardCompleted = false;
            StartCoroutine(SwapActiveBoard());
        }
    }

    private void FixedUpdate()
    {
        if (boardCompleted)
        {
            boardCompleted = false;
            StartCoroutine(SwapActiveBoard());
        }
    }
    /// <summary>
    /// Actualiza todos los objetos que estan activos actualmente en escena
    /// </summary>
    private void UpdateActive()
    {
        // Guardamos los enemigos
        activeEnemies.Clear();
        activeEnemies.AddRange(activeBoard.GetBoardEnemies());

        // Guardamos los enemigos
        activeWalls.Clear();
        activeWalls.AddRange(activeBoard.GetBoardWalls());

        // Guardamos los enemigos
        activeTerminals.Clear();
        activeTerminals.AddRange(activeBoard.GetBoardTerminals());

        // Guardamos los enemigos
        activeDoors.Clear();
        activeDoors.AddRange(activeBoard.GetBoardDoors());

        // Guardamos el jugador
        activePlayer = activeBoard.GetPlayer();

        // Guardamos la camara
        activeCamera = activeBoard.GetCamera();

    }

    #endregion

    #region GETTERS
    /// <summary>
    /// Devuelve el board activo acutalmente
    /// </summary>
    /// <returns></returns>
    public BBoard GetActiveBoard()
    {
        return activeBoard;
    }

    /// <summary>
    /// Devuelve el jugador activo actualmente
    /// </summary>
    /// <returns></returns>
    public BPlayer GetActviePlayer()
    {
        return activePlayer;
    }

    public void BoardCompleted()
    {
        boardCompleted = true;
    }
    #endregion

    #region CORUTINES

    public IEnumerator SwapActiveBoard()
    {

        yield return StartCoroutine(DisolveObjects());

        // Terminamos el ultimo Board Activo
        activeBoard.EndBoard();

        iter += 1;
        iter = iter % debugArray.Length;

        activeBoard = debugArray[iter].GetComponent<BBoard>();

        activeBoard.SetupBoard(this);

        UpdateActive();

        StartCoroutine(LoadObjects());

        yield return null;
    }

    /// <summary>
    /// Llama a todos los enemigo sy le sindica que deben hacer su turno y espera hasta que todos acaben de hacerlo
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyTurn()
    {
        RunInfo info = new RunInfo();

        foreach (BEnemy enemy in activeEnemies)
        {
            info = enemy.NextMovement().ParallelCoroutine("enemies");
        }

        yield return new WaitUntil(() => info.count <= 0);
    }

    /// <summary>
    /// Coorutina encargada de hacer la animacion de disolucion de objetos como carga
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisolveObjects()
    {
        RunInfo info = new RunInfo();

        activePlayer.setPlay(false);

        activePlayer.DestroyPath();

        // Obtenemos la informacion del jugados
        Material playermaterial = activePlayer.GetComponent<Renderer>().material;
        info = DisolveObject(playermaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");

        // Seguidamente de todas las puertas y las temrinales
        foreach (BPuerta puerta in activeDoors)
        {
            if (!puerta.GetAbierta())
            {
                Material doorMaterial = puerta.GetComponentInChildren<Renderer>().material;
                info = DisolveObject(doorMaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");
            }
        }

        foreach (BTerminal terminal in activeTerminals)
        {
            Material terminalMaterial = terminal.GetComponentInChildren<Renderer>().material;
            info = DisolveObject(terminalMaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");
        }

        // Hacemos lo mismo con los enemigos y con los muros
        foreach (BMuro muro in activeWalls)
        {
            Material wallMaterial = muro.GetComponentInChildren<Renderer>().material;
            info = DisolveObject(wallMaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");
        }

        foreach (BEnemy enemy in activeEnemies)
        {
            enemy.EndEnemy();
            Material enemyMaterial = enemy.GetComponentInChildren<Renderer>().material;
            info = DisolveObject(enemyMaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");
        }

        yield return new WaitUntil(() => info.count <= 0);

        yield return null;
    }

    /// <summary>
    /// Coorutina encargada de hacer la animacion de disolucion de objetos al fin del nivel
    /// </summary>
    /// <param name="disolveMat">Material a disolver</param>
    /// <param name="alphaName">Nombre del parametro de disolucion de alpha</param>
    /// <returns></returns>
    private IEnumerator DisolveObject(Material disolveMat, String alphaName)
    {

        // Iniciamos el timer a 0
        float timer = 0.0f;

        // Tiempo que tardara la disolucion
        float disolveTimer = UnityEngine.Random.Range(minDisolveTimer, maxDisolveTimer);

        // Mientras el tiempo sea menor que la duracion de la transicion repetimos
        while (timer < disolveTimer)
        {
            // Aumentamos el tiempo en funcion de el Time.deltaTime
            timer += Time.deltaTime;
            // Calculamos el porcentaje del tiempo que llevamos
            float t = timer / disolveTimer;
            // Transformamos el porcentaje segun una funcion para que la transicion sea suave
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            // Hacemos un Lerp en funcion de la posicion inicial y finla y el porcentaje de tiempo
            disolveMat.SetFloat("_DisolutionValue", Mathf.Lerp(0f, 1f, t));

            yield return null;
        }

        yield return null;
    }

    /// <summary>
    /// Coorutina encargada de hacer la animacion de disolucion de objetos como carga
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadObjects()
    {
        RunInfo info = new RunInfo();

        // Obtenemos la informacion del jugados
        Material playermaterial = activePlayer.GetComponent<Renderer>().material;
        info = LoadObject(playermaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");

        // Seguidamente de todas las puertas y las temrinales
        foreach (BPuerta puerta in activeDoors)
        {
            if (!puerta.GetAbierta())
            {
                Material doorMaterial = puerta.GetComponentInChildren<Renderer>().material;
                info = LoadObject(doorMaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");
            }
        }

        foreach (BTerminal terminal in activeTerminals)
        {
            Material terminalMaterial = terminal.GetComponentInChildren<Renderer>().material;
            info = LoadObject(terminalMaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");
        }

        // Hacemos lo mismo con los enemigos y con los muros
        foreach (BMuro muro in activeWalls)
        {
            Material wallMaterial = muro.GetComponentInChildren<Renderer>().material;
            info = LoadObject(wallMaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");
        }

        foreach (BEnemy enemy in activeEnemies)
        {
            Material enemyMaterial = enemy.GetComponentInChildren<Renderer>().material;
            info = LoadObject(enemyMaterial, "_DisolutionValue").ParallelCoroutine("loadDisolve");
        }

        yield return new WaitUntil(() => info.count <= 0);

        activePlayer.setPlay(true);


        foreach (BEnemy enemy in activeEnemies)
        {
            enemy.SetupEnemyVision();
        }

        yield return null;
    }

    /// <summary>
    /// Coorutina encargada de disolver el material recibido por parametro 
    /// </summary>
    /// <param name="disolveMat">Material a disolver</param>
    /// <param name="alphaName">Nombre del parametro de disolucion de alpha</param>
    /// <returns></returns>
    private IEnumerator LoadObject(Material disolveMat, String alphaName)
    {

        // Iniciamos el timer a 0
        float timer = 0.0f;

        // Tiempo que tardara la disolucion
        float disolveTimer = UnityEngine.Random.Range(minDisolveTimer, maxDisolveTimer);

        // Mientras el tiempo sea menor que la duracion de la transicion repetimos
        while (timer < disolveTimer)
        {
            // Aumentamos el tiempo en funcion de el Time.deltaTime
            timer += Time.deltaTime;
            // Calculamos el porcentaje del tiempo que llevamos
            float t = timer / disolveTimer;
            // Transformamos el porcentaje segun una funcion para que la transicion sea suave
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            // Hacemos un Lerp en el alpha en funciotn del porcentaje de tiempo
            disolveMat.SetFloat(alphaName, Mathf.Lerp(1f, 0f, t));

            yield return null;
        }

        yield return null;
    }

    #endregion

    #endregion
}