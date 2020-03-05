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
    private string mensaje2;
    private bool hackeable;

    private GameObject testPantalla;
    #endregion

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        pantalla1 = GameObject.Find("Pantalla1");

        pantalla1.SetActive(false);
        mensaje1 = ("\nNIVEL=" + Nivel + "\n\n" + "INFECTADO\n " + numInteracciones / Nivel * 100 + "%");
        mensaje2 = ("Proceso Hackeo\n" + "Interacciones necesarias: \n " + Nivel + "\nInteracciones actuales: " + numInteracciones);
        hackeable = false;

        testPantalla = GameObject.Find("Image");
        testPantalla.SetActive(false);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (numInteracciones == Nivel)
        {
            accionTrasHackear();
        }
        testPantalla.transform.position = cam.WorldToScreenPoint(pantalla1.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //pantalla1.SetActive(true);
            testPantalla.SetActive(true);
            if (this.numInteracciones < this.Nivel)
            {
                hackeable = true;
            }
            else
            {
                hackeable = false;
            }
            GameObject.Find("TextTest").GetComponent<Text>().text = mensaje1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
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
        this.mensaje2 = ("Proceso Hackeo\n" + "Interacciones necesarias: \n " + Nivel + "\nInteracciones actuales: " + this.numInteracciones);
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
