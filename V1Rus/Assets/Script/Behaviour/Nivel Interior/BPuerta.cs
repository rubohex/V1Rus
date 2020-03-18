using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPuerta : MonoBehaviour
{
    /// Estado de la puerta
    private bool abierta = false;
    private bool active = false;
    private BBoard boardScript;
    private BGameManager gameManager;

    public void Start()
    {
        this.transform.Find("Sphere").gameObject.SetActive(false);
    }

    public void SetupDoor(BGameManager manager)
    {
        active = true;

        gameManager = manager;
        boardScript = manager.GetActiveBoard();

        setEdges(boardScript, abierta);
        
    }

    /// <summary>
    /// Actualiza el estado de la puerta y de la casilla en la que se encuentra
    /// </summary>
    /// <param name="abierta">Nuevo estado</param>
    public void UpdateAbierta(bool abierta)
    {
        this.abierta = abierta;

        setEdges(boardScript, abierta);
        this.transform.Find("Cube").gameObject.SetActive(!abierta);
    }

    /// <summary>
    /// Devuelve el estado actual de abierta
    /// </summary>
    public bool GetAbierta()
    {
        return this.abierta;
    }

    /// <summary>
    /// Establece los bordes de la puerta de forma automática
    /// </summary>
    /// <param name="activeBoard">Tablero activo</param>
    /// <param name="abierta">Estado de abierta</param>
    private void setEdges(BBoard activeBoard, bool abierta)
    {
        Vector3 posCentro = this.transform.position;
        Vector3 posPuerta = this.transform.Find("Cube/Suelo").transform.position;
        Vector3 dirPuerta = (posPuerta - posCentro).normalized;


        dirPuerta = setClean(dirPuerta);

        int indexSuelo = boardScript.PositionToIndex(this.transform.position);
        int indexSigPuerta = boardScript.PositionToIndex(this.transform.position + dirPuerta);

        int edge = System.Convert.ToInt32(abierta);

        int up = 1;
        int down = 1;
        int left = 1;
        int rigth = 1;

        this.GetComponent<BMuro>().upEdge = 1;
        this.GetComponent<BMuro>().downEdge = 1;
        this.GetComponent<BMuro>().leftEdge = 1;
        this.GetComponent<BMuro>().rightEdge = 1;

        int dir = indexSigPuerta - indexSuelo;
        if(dir == 1)
        {
            this.GetComponent<BMuro>().rightEdge = edge;
            rigth = edge;
        }
        else if(dir == -1)
        {
            this.GetComponent<BMuro>().leftEdge = edge;
            left = edge;

        }
        else if(dir > 1)
        {
            this.GetComponent<BMuro>().upEdge = edge;
            up = edge;
        }
        else if(dir < -1)
        {
            this.GetComponent<BMuro>().downEdge = edge;
            down = edge;
        }

        if (active /*TODO desactivar o comprobar board activo*/)
        {
            boardScript.UpdateWallsEdges(boardScript.PositionToIndex(this.transform.position), up, left, down, rigth);
            string bordes = this.GetComponent<BMuro>().upEdge.ToString() + this.GetComponent<BMuro>().downEdge.ToString() + this.GetComponent<BMuro>().leftEdge.ToString() + this.GetComponent<BMuro>().rightEdge.ToString();
            print(bordes);
        }
    }

    /// <summary>
    /// Busca el mayor componente en valor absoluto y lo pone el resto a 0
    /// </summary>
    /// <param name="dirPuerta">Vector a limpiar</param>
    /// <returns></returns>
    private Vector3 setClean(Vector3 dirPuerta)
    {
        int maxInd = 0;
        float maxEl = 0;
        Vector3 retVector = Vector3.zero;
        Vector3 absDir = new Vector3(Math.Abs(dirPuerta.x), Math.Abs(dirPuerta.y), Math.Abs(dirPuerta.z));

        for (int i = 0; i < 3; i++)
        {
            if(absDir[i] > maxEl)
            {
                maxInd = i;
                maxEl = absDir[i];
            }
        }
        retVector[maxInd] = 1f * Math.Sign(dirPuerta[maxInd]);
        return retVector;
    }
}
