using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPuerta : MonoBehaviour
{
    private bool abierta;
    private BBoard boardScript;
    private BGameManager gameManager;
    // Start is called before the first frame update

    public void SetupDoor(BGameManager manager)
    {
        abierta = false;
        gameManager = manager;
        boardScript = manager.GetActiveBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void abrir()
    {
        abierta = true;
        this.GetComponent<BMuro>().upEdge = 1;
        this.GetComponent<BMuro>().downEdge = 1;
        this.GetComponent<BMuro>().leftEdge = 1;
        this.GetComponent<BMuro>().rightEdge = 1;
        boardScript.UpdateWallsEdges(boardScript.PositionToIndex(this.transform.position), 1, 1, 1, 1);
    }

    public void cerrar()
    {
        abierta = false;
        this.GetComponent<BMuro>().upEdge = 0;
        this.GetComponent<BMuro>().downEdge = 0;
        this.GetComponent<BMuro>().leftEdge = 0;
        this.GetComponent<BMuro>().rightEdge = 0;
        boardScript.UpdateWallsEdges(boardScript.PositionToIndex(this.transform.position), 0, 0, 0, 0);
    }
}
