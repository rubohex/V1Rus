using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour
{
    class objectState
    {
        public bool active;
        public float[] position;
        public float rotation;

        public objectState(bool active, float[] position, float rotation)
        {
            this.active = active;
            this.position = position;
            this.rotation = rotation;
        }
    }
    objectState estadoIni;

    string planeRef = "YZ";

    // Start is called before the first frame update
    void Start()
    {
        estadoIni = new objectState(getActivePower(), getActivePosition(), getActiveRotation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activate(bool estado)
    {
        if (name.Contains("Terminal"))
        {
            GetComponent<BTerminal>().setActivada(estado);
        }
    }
    public void activate(float valor)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, valor);
    }
    public void activate(float[] pos)
    {
        Vector3 newPos = Vector3.zero;

        switch (planeRef)
        {
            case "XY":
                newPos = new Vector3(pos[0], pos[1], transform.position.z);
                break;
            case "XZ":
                newPos = new Vector3(pos[0], transform.position.y, pos[1]);
                break;
            case "YZ":
                newPos = new Vector3(transform.position.x, pos[0], pos[1]);
                break;
        }
        transform.position = newPos;
    }

    public bool getActivePower()
    {
        if (name.Contains("Terminal"))
        {
            return GetComponent<BTerminal>().getActivada();
        }
        else
        {
            return false;
        }
    }
    public float getActiveRotation()
    {
        return transform.eulerAngles.z;
    }
    public float[] getActivePosition()
    {
        float[] pos = { 0, 0 };

        switch (planeRef)
        {
            case "XY":
                pos[0] = transform.position.x;
                pos[1] = transform.position.y;
                break;
            case "XZ":
                pos[0] = transform.position.x;
                pos[1] = transform.position.z;
                break;
            case "YZ":
                pos[0] = transform.position.y;
                pos[1] = transform.position.z;
                break;
        }

        return pos;
    }

    public int getCoste()
    {
        int coste = 0;

        if(getActivePower() != estadoIni.active)
        {
            coste++;
        }

        coste += (int)Mathf.Abs(getActiveRotation() - estadoIni.rotation) / 90;

        float[] pos = getActivePosition();
        coste += (int)Mathf.Abs(pos[0] - estadoIni.position[0]) + (int)Mathf.Abs(pos[1] - estadoIni.position[1]);

        return coste;
    }
}
