﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BMapPlayer : MonoBehaviour
{
    #region ATRIBUTES
    [Header("Data")]

    /// Datos de la Camara
    public DMapPlayerInfo mapPlayerInfo;

    /// Codigo de tecla para mover a la izquierda
    private KeyCode moveLeft;
    /// Codigo de tecla para mover a la derecha
    private KeyCode moveRight;
    /// Codigo de tecla para mover hacia adelante
    private KeyCode moveFront;
    /// Codigo de tecla para mover hacia atrás
    private KeyCode moveBack;

    /// Velocidad de rotacion del jugador
    private float rotationTime;
    /// Velocidad de movimiento del jugador
    private float moveTime;

    /// Tester para ver si se puede mover hacia adelante
    public bool CanMove = true;

    /// El EventPoint actual en el que se situa
    public GameObject CurrentEventpoint;
    public GameObject NewEventPoint;

    private Vector3 desplazamiento = Vector3.zero;

    /// Hacia donde mira el jugador respecto a la cara del cubo (Front, Back, Left, Right)
    private string Direction = "null";

    /// Tiempo total del movimiento del muelle
    float lerpTime = 5f;
    /// Tiempo que lleva desplazandose en el movimiento del muelle
    float currentLerpTime;

    // Diccionario con los objetivos (Event Points) por defecto en cada dirección
    public Dictionary<string, GameObject> Targets = new Dictionary<string, GameObject>();

    public enum ECord
    {
        XY,
        XZ,
        YZ
    }

    #endregion

    #region METHODS

    private void Awake()
    {
        moveLeft = mapPlayerInfo.moveLeft;
        moveRight = mapPlayerInfo.moveRight;
        moveFront = mapPlayerInfo.moveFront;
        moveBack = mapPlayerInfo.moveBack;
        lerpTime = mapPlayerInfo.moveTime;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        Targets = CurrentEventpoint.gameObject.GetComponent<BEventPoint>().Targets;
    }
    

    // Update is called once per frame
    void Update()
    {

        //Control de movimiento hacia adelante
        if (Input.GetKey(moveFront) && (CanMove || Direction == "Front"))
        {
            if (Direction != "Front")
            {
                transform.position = CurrentEventpoint.transform.position;
                currentLerpTime = 0f;
                Direction = "Front";
                NewEventPoint = Targets[Direction];
            }
            CanMove = false;
        }
        //Control de movimiento hacia atrás
        else if (Input.GetKey(moveBack) && (CanMove || Direction == "Back"))
        {
            if (Direction != "Back")
            {
                transform.position = CurrentEventpoint.transform.position;
                currentLerpTime = 0f;
                Direction = "Back";
                NewEventPoint = Targets[Direction];
            }
            CanMove = false;
        }
        //Control de movimiento hacia la izquierda
        else if (Input.GetKey(moveLeft) && (CanMove || Direction == "Left"))
        {
            if (Direction != "Left")
            {
                transform.position = CurrentEventpoint.transform.position;
                Direction = "Left";
                NewEventPoint = Targets[Direction];
                currentLerpTime = 0f;
            }
            CanMove = false;
        }
        //Control de movimiento hacia la derecha
        else if (Input.GetKey(moveRight) && (CanMove || Direction == "Right"))
        {
            if (Direction != "Right")
            {
                transform.position = CurrentEventpoint.transform.position;
                Direction = "Right";
                NewEventPoint = Targets[Direction];
            }
            CanMove = false;
        }
        else
        {
            transform.position = CurrentEventpoint.transform.position;
            currentLerpTime = 0f;
            CanMove = true;
            desplazamiento = Vector3.zero;
        }

        //Desplazamiento
        if (!CanMove && NewEventPoint != null)
        {
            Move(CurrentEventpoint.transform.position + desplazamiento, NewEventPoint.transform.position);
        }

        

    }

    #endregion

    void Move(Vector3 startPos, Vector3 endPos)
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
            CurrentEventpoint = NewEventPoint;
            Targets = CurrentEventpoint.gameObject.GetComponent<BEventPoint>().Targets;
        }

        float t = currentLerpTime / lerpTime;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        transform.position = Vector3.Lerp(startPos, endPos, t);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.GetComponent<BJunction>() != null)
        {
            if (Direction != "Front" && Input.GetKey(moveFront) && col.GetComponent<BJunction>().Targets["Front"])
            {
                Direction = "Front";
                desplazamiento = col.transform.position - CurrentEventpoint.transform.position;
                NewEventPoint = col.GetComponent<BJunction>().Targets[Direction];
                currentLerpTime = 0f;
            }
            else if (Direction != "Back" && Input.GetKey(moveBack) && col.GetComponent<BJunction>().Targets["Back"])
            {
                Direction = "Back";
                desplazamiento = col.transform.position - CurrentEventpoint.transform.position;
                NewEventPoint = col.GetComponent<BJunction>().Targets[Direction];
                currentLerpTime = 0f;
            }
            else if (Direction != "Left" && Input.GetKey(moveLeft) && col.GetComponent<BJunction>().Targets["Left"])
            {
                Direction = "Left";
                desplazamiento = col.transform.position - CurrentEventpoint.transform.position;
                NewEventPoint = col.GetComponent<BJunction>().Targets[Direction];
                currentLerpTime = 0f;
            }
            else if (Direction != "Right" && Input.GetKey(moveRight) && col.GetComponent<BJunction>().Targets["Right"])
            {
                Direction = "Right";
                desplazamiento = col.transform.position - CurrentEventpoint.transform.position;
                NewEventPoint = col.GetComponent<BJunction>().Targets[Direction];
                currentLerpTime = 0f;
            }
        }
    }

}
