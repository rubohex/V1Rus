using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BTerminal : MonoBehaviour
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
    /// <summary>
    /// </summary>
    /// Guardamos la acción realizada al hackear
    public EAccionHack hackAction;
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

    /// Textos del mesaje de la pantalla
    private string mensaje1;
    private string mensaje2;
    /// Guarda si la terminal es hackeable o no, ya sea por distancia u otros motivos
    private bool hackeable;

    /// Pantalla de la terminal (todavía en pruebas)
    private GameObject testPantalla;

    private BGameManager gameManager;

    /// Guarda si la terminal está activa en el nivel global
    private bool active = false;
    private BBoard boardScript;
    #endregion


    public void SetupTerminal(BGameManager manager)
    {
        active = true;
        boardScript = manager.GetActiveBoard();

        gameManager = manager;

        pantalla1 = this.transform.Find("Pantalla").gameObject;

        mensaje1 = ("NIVEL TERMINAL " + Nivel);
        mensaje2 = ("Proceso " + numInteracciones / Nivel * 100 + "%");

        hackeable = false;

        testPantalla = this.transform.Find("CanvasPrueba2/Pantalla").gameObject;
        testPantalla.SetActive(false);

        objetoHackeado = this.GetComponent<BTerminal>().objetoHackeado;
        hackAction = EAccionHack.Door;
    }

    void Start()
    {
        pantalla1 = this.transform.Find("Pantalla").gameObject;

        testPantalla = this.transform.Find("CanvasPrueba2/Pantalla").gameObject;
        testPantalla.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        active = (boardScript != null) && (boardScript == gameManager.GetActiveBoard());

        if (active)
        {
            if (numInteracciones == Nivel && hackeable)
            {
                hackeable = false;
                AccionTrasHackear();
                transform.Find("CanvasPrueba2/Pantalla/Boton").GetComponent<Button>().interactable = hackeable;
                //System.Array.Reverse(this.GetComponent<MeshRenderer>().materials);
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
        else
        {
            pantalla1.SetActive(false);
            testPantalla.SetActive(false);

            hackeable = false;
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
            this.transform.Find("CanvasPrueba2/Pantalla/Textos/TextTest1").GetComponent<Text>().text = mensaje1;
            this.transform.Find("CanvasPrueba2/Pantalla/Textos/TextTest2").GetComponent<Text>().text = mensaje2;
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
    public void ActualizaInteracciones(int interacciones)
    {
        this.numInteracciones = interacciones;

        this.mensaje1 = ("NIVEL TERMINAL " + this.Nivel);
        this.mensaje2 = ("Proceso " + (((float)interacciones / (float)this.Nivel) * 100) + "%");
    }

    /// <summary>
    /// Devuelve el número de interacciones
    /// </summary>
    /// <returns>
    /// Int con el numero de interacciones
    /// </returns>
    public int GetInteracciones()
    {
        return this.numInteracciones;
    }

    /// <summary>
    /// Devuelve el estado de la terminal
    /// </summary>
    /// <returns>
    /// Bool con el estado de si es o no hackeable
    /// </returns>
    public bool GetHackeable()
    {
        return this.hackeable;
    }

    /// Acción que se realiza al ser hackeada
    private void AccionTrasHackear()
    {
        if (hackAction == EAccionHack.Door)
        {
            foreach (GameObject p in objetoHackeado)
            {
                p.GetComponent<BPuerta>().UpdateAbierta(true);
            }
        }
    }

    public GameObject GetPlayer()
    {
        return gameManager.GetActviePlayer().gameObject;
    }
}
