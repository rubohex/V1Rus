using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BHackeo : MonoBehaviour
{
    public GameObject player;

    public float tHack;
    private float t = 0;
    private bool listo = true;
    //private bool hackeado = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X)/* && t <= tHack */ && this.GetComponent<BTerminal>().getHackeable() && listo)
        {
            t += Time.deltaTime;
            if(t >= tHack)
            {
                this.GetComponent<BTerminal>().actualizaInteracciones(this.GetComponent<BTerminal>().getInteracciones() + 1);
                listo = false;
                player.GetComponent<BPlayer>().changeAP(-1);
            }
        }
        else
        {
            t = 0;
        }
        if (!Input.GetKey(KeyCode.X))
        {
            listo = true;
        }
    }
    /*
    public void hackeo(BTerminal terminal)
    {

    }
    */
}
