using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTile : MonoBehaviour
{
    #region ATTRIBUTES

    /// <summary>
    /// Enumerado para poder definir los distintos estados en los que puede estar una casilla
    /// </summary>
    public enum ETileState 
    {
        Empty,
        Start,
        End,
    }

    /// <summary>
    /// Estado de la casilla
    /// </summary>
    public ETileState currentState;

    /// Material anterior de la casilla
    private Material oldMaterial;
    #endregion

    #region METHODS
    private void Start()
    {
        oldMaterial = GetComponent<Renderer>().material;
    }

    /// <summary>
    /// La funcion cambia el estado de la casilla
    /// </summary> 
    /// <param name="newState"> ETileState estado que queremos aplicar a la casilla </param>
    public void SwitchCurrentState(ETileState newState)
    {
        currentState = newState;
    }

    /// <summary>
    /// Cambia el material de la casilla por el nuevo material
    /// </summary>
    /// <param name="newMaterial">Nuevo Material</param>
    public void ChangeMaterial(Material newMaterial)
    {
        oldMaterial = GetComponent<Renderer>().material;

        GetComponent<Renderer>().material = newMaterial;
    }

    /// <summary>
    /// Devuelve el material a su antiguo material
    /// </summary>
    public void ResetMaterial()
    {
        GetComponent<Renderer>().material = oldMaterial;
    }

    #endregion
}
