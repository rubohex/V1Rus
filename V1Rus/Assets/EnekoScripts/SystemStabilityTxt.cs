using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemStabilityTxt : MonoBehaviour
{

    public TextMeshProUGUI stabilityValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stabilityValue.text == "High")
        {
            stabilityValue.color = Color.green;
        }
        if (stabilityValue.text == "Medium")
        {
            stabilityValue.color = Color.yellow;
        }
        if (stabilityValue.text == "Low")
        {
            stabilityValue.color = Color.red;
        }
    }
}
