using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BMapPlayer : MonoBehaviour
{
    #region ATRIBUTES
    [Header("Data")]

    /// Datos de la Camara
    public DPlayerInfo playerInfo;

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


    #endregion

    #region METHODS

    private void Awake()
    {
        rotateLeft = playerInfo.rotateLeft;
        rotateRight = playerInfo.rotateRight;
        move = playerInfo.move;
        rotationSpeed = playerInfo.rotationSpeed;
        moveSpeed = playerInfo.moveSpeed;
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}
