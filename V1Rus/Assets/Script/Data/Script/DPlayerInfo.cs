using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ScriptableObjects/PlayerInfo", order = 1)]

public class DPlayerInfo : ScriptableObject { 

    [Header("Opciones De Jugador")]
    
    /// <summary>
    /// Vida maxima del jugador
    /// </summary>
    public byte maxHealth;

    /// <summary>
    /// Vida del jugador
    /// </summary>
    public byte currentHealth;

    /// <summary>
    /// Velocidad de giro del jugador
    /// </summary>
    public float rotationTime;

    /// <summary>
    /// Velocidad de giro del jugador
    /// </summary>
    public float moveTime;


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

    [Header("Habilidades")]

    ///<summary>
    /// Indica si el jugador posee la habilidad de Recoger Cable
    /// </summary>
    public bool recogerCable;
}
