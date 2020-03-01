using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CameraInfo", menuName = "ScriptableObjects/CameraInfo", order = 1)]
public class DCameraInfo : ScriptableObject
{
    [Header("Opciones Camara")]
    /// <summary>
    /// Velocidad de movimiento transversal de la camara
    /// </summary>
    public float cameraSpeed = 20f;
    /// <summary>
    /// Velocidad de rotacion de la camara
    /// </summary>
    public float rotationTime = 2f;

    [Header("Teclas control")]

    /// <sumary>
    /// Codigo de tecla para rotar -90º
    /// </sumary>
    public KeyCode rotateLeft = KeyCode.E;
    /// <sumary>
    /// Codigo de tecla para rotar -90º
    /// </sumary>
    public KeyCode rotateRight = KeyCode.Q;
    /// <sumary>
    /// Codigo de tecla para mover hacia arriba
    /// </sumary>
    public KeyCode moveUp = KeyCode.UpArrow;
    /// <sumary>
    /// Codigo de tecla para mover hacia abajo
    /// </sumary>
    public KeyCode moveDown = KeyCode.DownArrow;
    /// <sumary>
    /// Codigo de tecla para mover a la izquierda
    /// </sumary>
    public KeyCode moveLeft = KeyCode.LeftArrow;
    /// <sumary>
    /// Codigo de tecla para rotar -90º
    /// </sumary>
    public KeyCode moveRight = KeyCode.RightArrow;
}
