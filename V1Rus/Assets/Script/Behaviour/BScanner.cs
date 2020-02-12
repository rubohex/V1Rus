using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BScanner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<BMapPlayer>().CanMove = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<BMapPlayer>().CanMove = false;
    }
}
