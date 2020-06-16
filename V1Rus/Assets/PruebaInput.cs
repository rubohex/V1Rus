using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class PruebaInput : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        setDefault();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeActive(bool estado)
    {
        target.SetActive(estado);
    }

    public void changePosX(TMPro.TMP_InputField input)
    {
        string texto = input.text;

        if(Regex.IsMatch(texto, "^([+-]?[1-9]\\d*|0)$"))
        {
            int posX = int.Parse(texto);
            print("X = "+ posX);
            target.transform.position = new Vector3(posX, target.transform.position.y, target.transform.position.z);
        }
        else
        {
            print("X = error");
        }

    }
    public void changePosY(TMPro.TMP_InputField input)
    {
        string texto = input.text;

        if (Regex.IsMatch(texto, "^([+-]?[1-9]\\d*|0)$"))
        {
            int posY = int.Parse(texto);
            print("Y = " + posY);
            target.transform.position = new Vector3(target.transform.position.x, posY, target.transform.position.z);
        }
        else
        {
            print("Y = error");
        }

    }

    public void setDefault()
    {

    }
}
