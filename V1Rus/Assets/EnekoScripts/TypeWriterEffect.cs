using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{

    public float speed = 0.1f;
    private string fullText = "System initializing...\n" + "Please stand by...\n" +"...\n" + "...\n" + "...\n" + "System succesfully initialized...";
    public string currentText="";
    public TextMeshProUGUI textoIntro;



    IEnumerator ShowText()
    {
        for(int i=0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textoIntro.text=currentText;
            yield return new WaitForSeconds(speed);
        }
    }

    public void restartText()
    {
        currentText = "";
    }

    public void StartText()
    {
        StartCoroutine(ShowText());
    }
}
