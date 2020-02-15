using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMuro : MonoBehaviour
{
    #region ATRIBUTES
    [Header("Bordes del Muro")]

    /// <summary>
    /// Borde Norte
    /// </summary>
    public int upEdge = 1;
    /// <summary>
    /// Borde Este
    /// </summary>
    public int leftEdge = 1;
    /// <summary>
    /// Borde Sur
    /// </summary>
    public int rightEdge = 1;
    /// <summary>
    /// Borde Oeste
    /// </summary>
    public int downEdge = 1;

    #endregion
}
