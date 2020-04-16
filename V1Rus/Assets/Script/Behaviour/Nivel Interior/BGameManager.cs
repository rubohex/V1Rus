using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGameManager : MonoBehaviour
{
    #region ATRIBUTES

    // Enemigos activos actualmente
    private List<BEnemy> activeEnemies = new List<BEnemy>();

    // Camara activa actualmente
    private BCameraController activeCamera;

    // JUgador activo actualmente
    private BPlayer activePlayer;

    // Board activos actualmente
    private BBoard activeBoard;

    // Debug
    public GameObject[] debugArray;

    private int iter = 0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        activeBoard = debugArray[iter].GetComponent<BBoard>();

        activeBoard.SetupBoard(this);

        UpdateActive();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwapActiveBoard(null);
        }
    }

    public void SwapActiveBoard(BBoard newBoard)
    {
        activeBoard.EndBoard();

        iter += 1;
        iter = iter % debugArray.Length;

        activeBoard = debugArray[iter].GetComponent<BBoard>();

        activeBoard.SetupBoard(this);

        UpdateActive();
    }

    /// <summary>
    /// Actualiza todos los objetos que estan activos actualmente en escena
    /// </summary>
    private void UpdateActive()
    {
        // Guardamos los enemigos
        activeEnemies.AddRange(activeBoard.GetBoardEnemies());

        // Guardamos el jugador
        activePlayer = activeBoard.GetPlayer();

        // Guardamos la camara
        activeCamera = activeBoard.GetCamera();
    }


    public IEnumerator EnemyTurn()
    {
        RunInfo info = new RunInfo();

        foreach (BEnemy enemy in activeEnemies)
        {
           info = enemy.NextMovement().ParallelCoroutine("enemies");
        }

        yield return new WaitUntil(() => info.count <= 0);
    }

    public BBoard GetActiveBoard()
    {
        return activeBoard;
    }

    public BPlayer GetActviePlayer()
    {
        return activePlayer;
    }
}
