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
    public void Update()
    {
        active = (boardScript != null) && (boardScript == gameManager.GetActiveBoard());
    }

    public void SetupDoor(BGameManager manager)
    {
        active = true;

        gameManager = manager;
        boardScript = manager.GetActiveBoard();

        setEdges(abierta);
    }

    /// <summary>
    /// Actualiza el estado de la puerta y de la casilla en la que se encuentra
    /// </summary>
    /// <param name="abierta">Nuevo estado</param>
    public void UpdateAbierta(bool abierta)
    {
        this.abierta = abierta;

        setEdges(abierta);

        transform.Find("Cube").gameObject.SetActive(!abierta);
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
    /// <param name="abierta">Estado de abierta</param>
    private void setEdges(bool abierta)
    {
        Vector3 posCentro = transform.position;
        Vector3 posPuerta = transform.Find("Cube/Suelo").transform.position;
        Vector3 dirPuerta = (posPuerta - posCentro).normalized;


        dirPuerta = setClean(dirPuerta);

        int indexSuelo = boardScript.PositionToIndex(this.transform.position);
        int indexSigPuerta = boardScript.PositionToIndex(this.transform.position + dirPuerta);

        int edge = Convert.ToInt32(abierta);

        int dir = indexSigPuerta - indexSuelo;
        if (active)
        {
            if (dir == 1)
            {
                boardScript.UpdateWallsEdges(indexSuelo, rightEdge: edge);
            }
            else if (dir == -1)
            {
                boardScript.UpdateWallsEdges(indexSuelo, leftEdge: edge);

            }
            else if (dir > 1)
            {
                boardScript.UpdateWallsEdges(indexSuelo, upEdge: edge);
            }
            else if (dir < -1)
            {
                boardScript.UpdateWallsEdges(indexSuelo, downEdge: edge);
            }
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
