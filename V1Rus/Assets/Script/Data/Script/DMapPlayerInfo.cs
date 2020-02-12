using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "MapPlayerInfo", menuName = "ScriptableObjects/MapPlayerInfo", order = 1)]

public class DMapPlayerInfo : ScriptableObject
{
    [Header("Opciones De Jugador")]


    /// <summary>
    /// Velocidad de giro del jugador
    /// </summary>
    public float rotationSpeed;

    /// <summary>
    /// Velocidad de giro del jugador
    /// </summary>
    public float moveSpeed;


    [Header("Teclas control")]

    /// <sumary>
    /// Codigo de tecla para rotar -90º
    /// </sumary>
    public KeyCode rotateLeft = KeyCode.A;
    /// <sumary>
    /// Codigo de tecla para rotar -90º
    /// </sumary>
    public KeyCode rotateRight = KeyCode.D;
    /// <sumary>
    /// Codigo de tecla para mover hacia delante
    /// </sumary>
    public KeyCode move = KeyCode.W;
}
