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
    public float moveTime;


    [Header("Teclas control")]

    public KeyCode movement = KeyCode.Space;
    /// <sumary>
    /// Codigo de tecla para mover hacia la Izquierda
    /// </sumary>
    public KeyCode moveLeft = KeyCode.A;
    /// <sumary>
    /// Codigo de tecla para mover hacia la Derecha
    /// </sumary>
    public KeyCode moveRight = KeyCode.D;
    /// <sumary>
    /// Codigo de tecla para mover hacia delante
    /// </sumary>
    public KeyCode moveFront = KeyCode.W;
    /// <sumary>
    /// Codigo de tecla para mover hacia atrás
    /// </sumary>
    public KeyCode moveBack = KeyCode.S;
}
