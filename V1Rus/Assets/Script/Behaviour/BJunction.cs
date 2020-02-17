using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJunction : MonoBehaviour
{

    public GameObject FrontTarget;
    public GameObject BackTarget;
    public GameObject RightTarget;
    public GameObject LeftTarget;

    // Diccionario con los objetivos (Event Points) por defecto en cada dirección
    public Dictionary<string, GameObject> Targets = new Dictionary<string, GameObject>();

    public void Awake()
    {
        Targets.Add("Front", FrontTarget);
        Targets.Add("Back", BackTarget);
        Targets.Add("Left", LeftTarget);
        Targets.Add("Right", RightTarget);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
