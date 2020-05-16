using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Strenght : MonoBehaviour
{
    public int unitValor;
    public TextMeshProUGUI unitText;



    // Update is called once per frame
    void Update()
    {
        unitText.text = unitValor.ToString();


        if(unitValor>=0 && unitValor < 4)
        {
            unitText.color = Color.red;

            
        }
        if (unitValor >= 4 && unitValor < 8)
        {
            unitText.color = Color.yellow;


        }
        if (unitValor >= 8)
        {
            unitText.color = Color.green;


        }
    }
}
