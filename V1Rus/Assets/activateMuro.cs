using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateMuro : MonoBehaviour
{

    private float[] pos;
    private bool active;
    BBoard boardScript;
    BGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetupMuro(BGameManager manager)
    {
        active = true;
        boardScript = manager.GetActiveBoard();

        gameManager = manager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPos(float[] pos)
    {
        Vector3 newPos = Vector3.zero;

        switch (boardScript.GetCoordSys())
        {
            case BBoard.ECord.XY:
                newPos = new Vector3(pos[0], pos[1], transform.position.z);
                break;
            case BBoard.ECord.XZ:
                newPos = new Vector3(pos[0], transform.position.y, pos[1]);
                break;
            case BBoard.ECord.YZ:
                newPos = new Vector3(transform.position.x, pos[0], pos[1]);
                break;
        }
        //transform.position = newPos;
        //TODO actualizar casillas
    }
    public float[] getPos()
    {
        switch (boardScript.GetCoordSys())
        {
            case BBoard.ECord.XY:
                pos[0] = transform.position.x;
                pos[1] = transform.position.y;
                break;
            case BBoard.ECord.XZ:
                pos[0] = transform.position.x;
                pos[1] = transform.position.z;
                break;
            case BBoard.ECord.YZ:
                pos[0] = transform.position.y;
                pos[1] = transform.position.z;
                break;
        }

        return pos;
    }
}
