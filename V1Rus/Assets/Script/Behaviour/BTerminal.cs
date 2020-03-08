using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BTerminal : MonoBehaviour
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
    private GameObject pantalla2;
    private GameObject camara;
    private Vector3 direccionPantalla;
    private Quaternion cuaternion;
    private float giroY;
    private float giroX;
    private Vector2 orientacion_camera_y;
    private Vector2 orientacion_camera_x;
    public int Nivel;
    private int numInteracciones;
    private TextMeshPro texto1;
    private TextMeshPro texto2;
    private string mensaje1;
    private string mensaje2;
    private bool hackeable;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        pantalla1 = GameObject.Find("Pantalla1");
        camara = GameObject.Find("Main Camera");
        texto1 = GameObject.Find("Text1").GetComponent<TextMeshPro>();
        texto2 = GameObject.Find("Text2").GetComponent<TextMeshPro>();
        pantalla1.SetActive(false);
        mensaje1 = ("\nNIVEL=" + Nivel + "\n\n" + "INFECTADO\n " + numInteracciones/Nivel*100 + "%");
        mensaje2= ("Proceso Hackeo\n" + "Interacciones necesarias: \n " + Nivel + "\nInteracciones actuales: " + numInteracciones);
        hackeable = false;

    }

    // Update is called once per frame
    void Update()
    {
        direccionPantalla = (camara.transform.position - pantalla1.transform.position);
        orientacion_camera_y = new Vector2(direccionPantalla[0], direccionPantalla[2]);
        orientacion_camera_x = new Vector2(direccionPantalla[1], direccionPantalla[2]);
        giroY = Vector2.Angle(orientacion_camera_y,new Vector2(0, -1));

        if(camara.transform.position.x<=this.transform.position.x){
            //giroY se queda igual
        }
        else{
            giroY = -giroY;
        }

        giroX = Vector3.Angle(direccionPantalla, new Vector3(0,1,0));
        pantalla1.transform.eulerAngles = new Vector3(-giroX, giroY, 0);

        if(numInteracciones==Nivel){
            accionTrasHackear();
        }

    }

    private void OnTriggerStay  (Collider other)
    {
        if(other.gameObject.name=="Player"){
            texto1.text = mensaje1;
            texto2.text = mensaje2;
            pantalla1.SetActive(true);
            if (this.numInteracciones<this.Nivel){
                hackeable = true;
            }
            else{
                hackeable = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            pantalla1.SetActive(false);
            hackeable = false;

        }
    }

    public void actualizaInteracciones(int interacciones){
        this.numInteracciones = interacciones;
        this.mensaje1=("\nNIVEL=" + this.Nivel + "\n\n" + "INFECTADO\n " + (((float)interacciones / (float)this.Nivel) * 100) + "%");
        this.mensaje2=("Proceso Hackeo\n" + "Interacciones necesarias: \n " + Nivel + "\nInteracciones actuales: " + this.numInteracciones);
    }

    public int getInteracciones(){
        return this.numInteracciones;
    }

    public bool getHackeable()
    {
        return this.hackeable;
    }

    private void accionTrasHackear(){
        if(hackAction == EAccionHack.Door)
        {
            foreach(GameObject p in objetoHackeado)
            {
                p.GetComponent<BPuerta>().updateAbierta(true);
            }
        }
    }

}
