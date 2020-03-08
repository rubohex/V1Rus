using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BTerminalTesting : MonoBehaviour
{
    #region Atributos

    public enum EAccionHack
    {
        Ability,
        Door,
    }

    public EAccionHack hackAction;
    public GameObject[] objetoHackeado;

    private GameObject pantalla1;

    public int Nivel;
    private int numInteracciones;

    private string mensaje1;
    private bool hackeable;

    private GameObject testPantalla;
    private BGameManager gameManager;

    private bool active = false;
    #endregion


    // Start is called before the first frame update
    public void SetupTerminal(BGameManager manager)
    {
        active = true;

        gameManager = manager;

        pantalla1 = this.transform.Find("Pantalla1").gameObject;
        pantalla1.SetActive(false);

        mensaje1 = ("\nNIVEL=" + Nivel + "\n\n" + "INFECTADO\n " + numInteracciones / Nivel * 100 + "%");

        hackeable = false;

        testPantalla = this.transform.Find("CanvasPrueba2/Image").gameObject;
        testPantalla.SetActive(false);

    }
    
    void Start()
    {
                pantalla1 = this.transform.Find("Pantalla1").gameObject;
        pantalla1.SetActive(false);

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
            BBoard board = gameManager.GetActiveBoard();
            Camera cam = board.GetCamera().GetComponent<Camera>();
            Vector3 newPos = cam.WorldToScreenPoint(pantalla1.transform.position);
            testPantalla.transform.position = newPos;
        }
        
    }

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

    public void actualizaInteracciones(int interacciones)
    {
        this.numInteracciones = interacciones;
        this.mensaje1 = ("\nNIVEL=" + this.Nivel + "\n\n" + "INFECTADO\n " + (((float)interacciones / (float)this.Nivel) * 100) + "%");
    }

    public int getInteracciones()
    {
        return this.numInteracciones;
    }

    public bool getHackeable()
    {
        return this.hackeable;
    }

    private void accionTrasHackear()
    {
        if (hackAction == EAccionHack.Door)
        {
            foreach (GameObject p in objetoHackeado)
            {
                p.GetComponent<BPuerta>().abrir();
            }
        }
    }

}
