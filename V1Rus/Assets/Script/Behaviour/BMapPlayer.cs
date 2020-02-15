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

    /// Codigo de tecla para rotar a la izquierda
    private KeyCode rotateLeft;
    /// Codigo de tecla para rotar a la derecha
    private KeyCode rotateRight;
    /// Codigo de tecla para mover hacia adelante
    private KeyCode move;

    /// Velocidad de rotacion del jugador
    private float rotationSpeed;
    /// Velocidad de movimiento del jugador
    private float moveSpeed;

    /// Tester para ver si se puede mover hacia adelante
    public bool CanMove = true;

    public Vector3 CurrentEventpoint;
    public Vector3 NewPoint;

    float lerpTime = 5f;
    float currentLerpTime;

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
                Move(CurrentEventpoint, NewPoint);
            }
            else
            {
                transform.position = CurrentEventpoint;
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

    void Move(Vector3 startPos, Vector3 endPos)
    {
        if (Input.GetKeyDown(move))
        {
            currentLerpTime = 0f;
        }

        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float t = currentLerpTime / lerpTime;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        transform.position = Vector3.Lerp(startPos, endPos, t);

        
    }
}
