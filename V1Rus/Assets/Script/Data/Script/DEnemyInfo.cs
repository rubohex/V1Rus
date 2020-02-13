using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "EnemyInfo", menuName = "ScriptableObjects/EnemyInfo", order = 1)]

public class DEnemyInfo : ScriptableObject
{
    [Header("Opciones De Enemigo")]

    /// <summary>
    /// Velocidad de giro del enemigo
    /// </summary>
    public float rotationSpeed;

    /// <summary>
    /// Velocidad de giro del enemigo
    /// </summary>
    public float moveSpeed;
}
