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
    public int upEdge = 1;
    public int leftEdge = 1;
    public int rightEdge = 1;
    public int downEdge = 1;

    [Header("Coordenadas activas")]
    public bool x = false;
    public bool y = false;
    public bool z = false;

    [Header("Prefabs")]
    public GameObject player;
    public GameObject camera;

}
