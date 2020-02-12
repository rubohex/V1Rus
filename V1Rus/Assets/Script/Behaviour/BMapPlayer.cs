using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BMapPlayer : MonoBehaviour
{
    #region ATRIBUTES
    [Header("Data")]

    /// Datos de la Camara
    public DMapPlayerInfo mapPlayerInfo;

    /// Codigo de tecla para rotar -90º
    private KeyCode rotateLeft;
    /// Codigo de tecla para rotar -90º
    private KeyCode rotateRight;
    /// Codigo de tecla para mover hacia arriba
    private KeyCode move;

    /// Velocidad de rotacion del jugador
    private float rotationSpeed;
    /// Velocidad de movimiento del jugador
    private float moveSpeed;

    /// Indica si el jugador esta rotando o moviendose
    private bool isMoving = false;

    /// Diccionario para guardar la relacion entre la rotacion y la casilla a la que apuntamos
    private Dictionary<int, int> direction = new Dictionary<int, int>();

    public bool CanMove = true;



    #endregion

    #region METHODS

    private void Awake()
    {
        rotateLeft = mapPlayerInfo.rotateLeft;
        rotateRight = mapPlayerInfo.rotateRight;
        move = mapPlayerInfo.move;
        rotationSpeed = mapPlayerInfo.rotationSpeed;
        moveSpeed = mapPlayerInfo.moveSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
        //Si el trigger del Scanner detecta que esta en una plataforma
        if (CanMove)
        {
            //Control de movimiento hacia adelante
            if (Input.GetKey(move))
            {
                this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            
        }
        //Control de giro a la izquierda
        if (Input.GetKey(rotateLeft))
        {
            this.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
        //Control de giro a la derecha.
        if (Input.GetKey(rotateRight))
        {
            this.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

    }

    #endregion
}
