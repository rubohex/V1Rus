using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "PathPlatformInfo", menuName = "ScriptableObjects/PathPlatformInfo", order = 1)]
public class DPathPlatformInfo : ScriptableObject
{
    [Header("Coordenadas activas")]
    public bool x = false;
    public bool y = false;
    public bool z = false;
}
