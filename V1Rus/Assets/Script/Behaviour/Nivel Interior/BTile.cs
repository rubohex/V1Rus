using Jint.Parser;
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
        EVision,
        End,
    }

    /// Estado de la casilla
    public ETileState currentState;
    /// Estado original de la casill
    public ETileState originalState;
    /// Material original de la casilla
    public Material originalMaterial;


    /// Lista de Materiales de la casilla
    private List<Material> materials;
    /// Lista de Estados de la casilla
    private List<ETileState> states;

    #endregion

    #region METHODS
    private void Awake()
    {
        materials = new List<Material>();
        originalMaterial = GetComponent<Renderer>().material;
        materials.Add(originalMaterial);

        states = new List<ETileState>();
        originalState = currentState;
        states.Add(currentState);
    }

    /// <summary>
    /// La funcion cambia el estado de la casilla
    /// </summary> 
    /// <param name="newState"> ETileState estado que queremos aplicar a la casilla </param>
    public void ChangeState(ETileState newState)
    {
        states.Insert(states.Count, newState);
        currentState = newState;
    }

    /// <summary>
    /// Devuelve el estado de la casilla al anterior o al original
    /// </summary>
    public void ResetState()
    {
        if (states.Count > 1)
        {
            currentState = states[states.Count - 2];
            states.RemoveAt(states.Count - 1);
        }
    }

    /// <summary>
    /// Elimina el estado que recibe de su lista de materiales
    /// </summary>
    /// <param name="material">Estado a eliminar</param>
    public void RemoveState(ETileState state)
    {
        if (states.Contains(state))
        {
            states.Remove(state);
            currentState = states[states.Count - 1];
        }
    }

    /// <summary>
    /// Cambia el material de la casilla por el nuevo material
    /// </summary>
    /// <param name="newMaterial">Nuevo Material</param>
    public void ChangeMaterial(Material newMaterial)
    {
           
        materials.Insert(materials.Count,newMaterial);

        GetComponent<Renderer>().material = newMaterial;
    }

    /// <summary>
    /// Devuelve el material de la casilla al anterior o al original
    /// </summary>
    public void ResetMaterial()
    {
        if(materials.Count > 1)
        {
            GetComponent<Renderer>().material = materials[materials.Count - 2];

            materials.RemoveAt(materials.Count - 1);

        }
    }

    /// <summary>
    /// Elimina el material que recibe de su lista de materiales
    /// </summary>
    /// <param name="material">Material a eliminar</param>
    public void RemoveMaterial(Material material)
    {
        if (materials.Contains(material))
        {
            materials.Remove(material);
            GetComponent<Renderer>().material = materials[materials.Count - 1];
        }
    }

    /// <summary>
    /// Devuelve la casilla a su estado y material original vaciando los arrays
    /// </summary>
    public void BackToOriginal()
    {
        materials.Clear();
        states.Clear();

        currentState = originalState;
        states.Add(currentState);
        GetComponent<Renderer>().material = originalMaterial;
        materials.Add(originalMaterial);        
    }

    #endregion
}
