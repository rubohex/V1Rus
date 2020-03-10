using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPuerta : MonoBehaviour
{
    /// Estado de la puerta
    private bool abierta;
    private BBoard boardScript;
    private BGameManager gameManager;

    public void Start()
    {
        this.transform.Find("Sphere").gameObject.SetActive(false);
    }
    public void SetupDoor(BGameManager manager)
    {
        abierta = false;
        gameManager = manager;
        boardScript = manager.GetActiveBoard();
    }

    /// <summary>
    /// Actualiza el estado de la puerta y de la casilla en la que se encuentra
    /// </summary>
    /// <param name="abierta">Nuevo estado</param>
    public void UpdateAbierta(bool abierta)
    {
        this.abierta = abierta;
        int edge = System.Convert.ToInt32(abierta);
        this.GetComponent<BMuro>().upEdge = edge;
        this.GetComponent<BMuro>().downEdge = edge;
        this.GetComponent<BMuro>().leftEdge = edge;
        this.GetComponent<BMuro>().rightEdge = edge;
        boardScript.UpdateWallsEdges(boardScript.PositionToIndex(this.transform.position), edge, edge, edge, edge);
        this.transform.Find("Cube").gameObject.SetActive(!abierta);
    }

    /// <summary>
    /// Devuelve el estado actual de abierta
    /// </summary>
    public bool GetAbierta()
    {
        return this.abierta;
    }
}
