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
    private List<Material> materials;

    #endregion

    #region METHODS
    private void Awake()
    {
        materials = new List<Material>();
        materials.Add(GetComponent<Renderer>().material);
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

    #endregion
}
