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
    /// <summary>
    /// Bordes basicos 1 para poder pasar 0 para no poder
    /// </summary>
    public int[] BaseEdges = new int[4];
}
