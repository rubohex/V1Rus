using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModificationsAvailableValue : MonoBehaviour
{
    public int modificationsAvailable;
    public TextMeshProUGUI modificationsAvailableText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        modificationsAvailableText.text = modificationsAvailable.ToString();
        if (modificationsAvailable <= 2 && modificationsAvailable>=0)
        {
            modificationsAvailableText.color = Color.red;
        }
        if(modificationsAvailable>2 && modificationsAvailable <= 5){

            modificationsAvailableText.color = Color.yellow;
        }
        if (modificationsAvailable > 5)
        {
            modificationsAvailableText.color = Color.green;
        }
    }
}
