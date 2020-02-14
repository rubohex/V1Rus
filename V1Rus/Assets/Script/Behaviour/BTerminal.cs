using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTerminal : MonoBehaviour
{
    //Atributos

    private GameObject pantalla;
    private GameObject camara;
    private Vector3 direccionPantalla;
    private Quaternion cuaternion;
    private float giroY;
    private float giroX;
    private Vector2 orientacion_camera_y;
    private Vector2 orientacion_camera_x;
    // Start is called before the first frame update
    void Start()
    {
        pantalla = GameObject.Find("Pantalla");
        camara = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
        direccionPantalla = (camara.transform.position - pantalla.transform.position);
        orientacion_camera_y = new Vector2(direccionPantalla[0], direccionPantalla[2]);
        orientacion_camera_x = new Vector2(direccionPantalla[1], direccionPantalla[2]);
        giroY = Vector2.Angle(orientacion_camera_y,new Vector2(0, -1));

        if(camara.transform.position.x<=this.transform.position.x){
            giroY = giroY;
        }
        else{
            giroY = -giroY;
        }

        giroX = Vector3.Angle(direccionPantalla, new Vector3(0,1,0));
        pantalla.transform.eulerAngles = new Vector3(-giroX, giroY, 0);

    }
}
