using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "MapCameraInfo", menuName = "ScriptableObjects/MapCameraInfo", order = 1)]
public class DMapCameraInfo : ScriptableObject
{
    [Header("Opciones Camara")]
    /// <summary>
    /// Velocidad de movimiento transversal de la camara
    /// </summary>
    public float moveSpeed = 20f;
    /// <summary>
    /// Velocidad de rotacion de la camara
    /// </summary>
    public float rotationSpeed = 2f;

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
    public KeyCode rotateUp = KeyCode.UpArrow;
    /// <sumary>
    /// Codigo de tecla para mover hacia abajo
    /// </sumary>
    public KeyCode rotateDown = KeyCode.DownArrow;
    /// <sumary>
    /// Codigo de tecla para mover a la izquierda
    /// </sumary>
    public KeyCode rotateForward = KeyCode.LeftArrow;
    /// <sumary>
    /// Codigo de tecla para rotar -90º
    /// </sumary>
    public KeyCode rotateBack = KeyCode.RightArrow;
}
