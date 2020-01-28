using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "BoardInfo", menuName = "ScriptableObjects/BoardInfo", order = 1)]
public class DBoardInfo : ScriptableObject
{
    [Header("Opciones De Tablero")]
    /// <summary>
    /// Numero de AP asignados en el nivel
    /// </summary>
    public byte APNivel;
}
