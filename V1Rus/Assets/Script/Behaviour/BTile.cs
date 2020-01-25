using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTile : MonoBehaviour
{
    #region ATTRIBUTES

    // Enumerado para poder definir los distintos estados en los que puede estar una etiqueta
    public enum ETileState 
    {
        Empty,
        Start,
        End,
        Obstacle,
        Enemy
    }

    // Estado de una casilla
    public ETileState currentState;

    // Atributos temporales
    private bool show = false;
    private bool hide = false;
    private float transparency;
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        transparency = this.gameObject.GetComponent<Renderer>().material.GetFloat("_Transparency");
    }

    // Cambio del estado de la Casilla
    void SwitchCurrentState(ETileState newState)
    {
        currentState = newState;
    }

    #endregion

    #region Temporal hasta que decidamos como aparecen los niveles

    void Update()
    {
        if (show && transform.position.y > 0)
        {
            transform.position -= transform.up * Time.deltaTime;
            if (transform.position.y < 0)
            {
                transform.position.Set(transform.position.x, 0, transform.position.z);
            }
            transparency += 5 * Time.deltaTime;
            this.gameObject.GetComponent<Renderer>().material.SetFloat("_Transparency", transparency);
        }
        else if (show && transparency < 1)
        {
            transparency += 5 * Time.deltaTime;
            this.gameObject.GetComponent<Renderer>().material.SetFloat("_Transparency", transparency);
        }

        if(hide && transparency > 0)
        {
            transform.position -= transform.up * Time.deltaTime;
            transparency -= 1 * Time.deltaTime;
            if (transparency < 0)
            {
                transparency = 0;
            }
            this.gameObject.GetComponent<Renderer>().material.SetFloat("_Transparency", transparency);
        }
    }

    public void ShowTile()
    {

    }
    public void HideTile()
    {
        StartCoroutine("Hide");
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(Random.Range(0f,2f));
        hide = true;
    }
    #endregion
}
