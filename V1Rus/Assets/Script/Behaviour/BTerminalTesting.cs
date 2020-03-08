﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BTerminalTesting : MonoBehaviour
{
    #region Atributos

    /// <summary>
    /// Enumerado para definir la accion que se realizará al hackear
    /// </summary>
    public enum EAccionHack
    {
        Ability,
        Door,
    }

    /// Guardamos la acción realizada al hackear
    private EAccionHack hackAction;
    /// <summary>
    /// Guardamos el array de objetos sobre los que se realizará la acción del hackeo
    /// </summary>
    public GameObject[] objetoHackeado;

    /// Lugar que ocupará la pantalla de la terminal
    private GameObject pantalla1;

    /// <summary>
    /// Número de interacciones necesarias para hackear
    /// </summary>
    public int Nivel;
    /// Contador de las interacciones realizadas
    private int numInteracciones;

    /// Texto del mesaje de la pantalla
    private string mensaje1;
    /// Guarda si la terminal es hackeable o no, ya sea por distancia u otros motivos
    private bool hackeable;

    /// Pantalla de la terminal (todavía en pruebas)
    private GameObject testPantalla;

    private BGameManager gameManager;

    /// Guarda si la terminal está activa en el nivel global
    private bool active = false;
    #endregion


    public void SetupTerminal(BGameManager manager)
    {
        active = true;

        gameManager = manager;

        pantalla1 = this.transform.Find("Pantalla").gameObject;

        mensaje1 = ("\nNIVEL=" + Nivel + "\n\n" + "INFECTADO\n " + numInteracciones / Nivel * 100 + "%");

        hackeable = false;

        testPantalla = this.transform.Find("CanvasPrueba2/Image").gameObject;
        testPantalla.SetActive(false);

    }
    
    void Start()
    {
        pantalla1 = this.transform.Find("Pantalla").gameObject;

        testPantalla = this.transform.Find("CanvasPrueba2/Image").gameObject;
        testPantalla.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (numInteracciones == Nivel)
            {
                accionTrasHackear();
            }

            // Actualiza la posición de la pantalla en el canvas
            BBoard board = gameManager.GetActiveBoard();
            Camera cam = board.GetCamera().GetComponent<Camera>();
            Vector3 newPos = cam.WorldToScreenPoint(pantalla1.transform.position);
            testPantalla.transform.position = newPos;

            // Actualiza la escala de la pantalla para simular la perspectiva
            float dist = Vector3.Distance(cam.transform.position, this.transform.position);
            testPantalla.transform.localScale = Vector3.one / dist * 10f;
        }
        
    }

    /// Mientras el jugador esté al lado de la terminal se muestra la pantalla y se actualiza el texto
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            testPantalla.SetActive(true);
            if (this.numInteracciones < this.Nivel)
            {
                hackeable = true;
            }
            else
            {
                hackeable = false;
            }
            this.transform.Find("CanvasPrueba2/Image/TextTest").GetComponent<Text>().text = mensaje1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            pantalla1.SetActive(false);
            testPantalla.SetActive(false);

            hackeable = false;

        }
    }

    /// <summary>
    /// Actualiza el numero de interacciones realizadas
    /// </summary>
    /// <param name="interacciones">Nuevo número de actualizaciones</param>
    public void actualizaInteracciones(int interacciones)
    {
        this.numInteracciones = interacciones;
        this.mensaje1 = ("\nNIVEL=" + this.Nivel + "\n\n" + "INFECTADO\n " + (((float)interacciones / (float)this.Nivel) * 100) + "%");
    }

    /// <summary>
    /// Devuelve el número de interacciones
    /// </summary>
    /// <returns>
    /// Int con el numero de interacciones
    /// </returns>
    public int getInteracciones()
    {
        return this.numInteracciones;
    }

    /// <summary>
    /// Devuelve el estado de la terminal
    /// </summary>
    /// <returns>
    /// Bool con el estado de si es o no hackeable
    /// </returns>
    public bool getHackeable()
    {
        return this.hackeable;
    }

    /// Acción que se realiza al ser hackeada
    private void accionTrasHackear()
    {
        if (hackAction == EAccionHack.Door)
        {
            foreach (GameObject p in objetoHackeado)
            {
                p.GetComponent<BPuerta>().updateAbierta(true);
            }
        }
    }

}
