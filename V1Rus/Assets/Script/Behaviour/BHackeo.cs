using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHackeo : MonoBehaviour
{
    public float tHack;
    private float t = 0;
    //private bool hackeado = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X)/* && t <= tHack */)
        {
            t += Time.deltaTime;
            if(t >= tHack)
            {
                //hackeado = true;
                print("HACKEADOWO");
            }
        }
        else
        {
            t = 0;
        }
    }
    /*
    public void hackeo(BTerminal terminal)
    {

    }
    */
}
