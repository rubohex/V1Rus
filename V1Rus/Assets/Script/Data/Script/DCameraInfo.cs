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
    /// <summary>
    /// Tamaño minimo de la vista ortografica
    /// </summary>
    public float closeZoomClamp = 3f;
    /// <summary>
    /// Tamaño maximo de la vista ortografica
    /// </summary>
    public float farZoomClamp = 8f;

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

    [Header("Starting Position")]
    /// <summary>
    /// Posicion de la camara con respecto al target
    /// </summary>
    public Vector3 position = new Vector3(-9.31f, 7.6f, -9.31f);

    /// <summary>
    /// Rotacion de la camara con respecto al target
    /// </summary>
    public Vector3 rotation = new Vector3(30f, 45f, 0f);

}
