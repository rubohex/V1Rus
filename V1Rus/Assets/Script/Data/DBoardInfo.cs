using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "BoardInfo", menuName = "ScriptableObjects/BoardInfo", order = 1)]
public class DBoardInfo : ScriptableObject
{
    public byte startAP;
}
