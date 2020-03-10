using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BMapPlayer : MonoBehaviour
{
    #region ATRIBUTES
    [Header("Data")]

    /// Datos de la Camara
    public DMapPlayerInfo mapPlayerInfo;

    ///Movierse hacia el EventPoint Seleccionado
    private KeyCode movement;

    /// Tester para ver si se puede mover hacia adelante
    public bool CanMove = true;

    /// El EventPoint actual en el que se situa
    public GameObject currentEventpoint;
    public GameObject targetEventPoint;
    public GameObject selectedEventPoint;
    private GameObject[] currentPath;
    private int pathPos;

    private Vector3 desplazamiento = Vector3.zero;

    /// Hacia donde mira el jugador respecto a la cara del cubo (Front, Back, Left, Right)
    private string Direction = "null";
    private string PrevDirection = "Front";

    //Es posible ir al EventPoint Seleccionado no
    private bool canGoToEP;

    /// Tiempo total del movimiento del muelle
    float lerpTime = 5f;
    /// Tiempo que lleva desplazandose en el movimiento del muelle
    float currentLerpTime;

    

    // Diccionario con los objetivos (Event Points) por defecto en cada dirección
    public Dictionary<GameObject, int> Targets = new Dictionary<GameObject, int>();
    public Dictionary<string, List<GameObject>> Paths = new Dictionary<string, List<GameObject>>();
    public enum ECord
    {
        XY,
        XZ,
        YZ
    }

    #endregion

    #region METHODS

    private void Awake()
    {
        movement = mapPlayerInfo.movement;
        lerpTime = mapPlayerInfo.moveTime;
        pathPos = 0;

    }
    // Start is called before the first frame update
    void Start()
    {
        Targets = GetTargets(currentEventpoint.gameObject.GetComponent<BEventPoint>().MyList);
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "EventPoint")
        {
            targetEventPoint = hitInfo.transform.gameObject;
            if (Targets.ContainsKey(targetEventPoint))
            {
                print("Se puede ir aquí");
                canGoToEP = true;
            }
            else
            {
                print("No se puede ir aquí");
                canGoToEP = false;
            }
            if (Input.GetMouseButtonDown(0) && canGoToEP)
            {
                if (selectedEventPoint == targetEventPoint)
                {
                    selectedEventPoint = null;
                }
                else
                {
                    
                    selectedEventPoint = targetEventPoint;
                    currentPath = currentEventpoint.GetComponent<BEventPoint>().MyList[Targets[selectedEventPoint]].Path;
                }
            }
        }

        if (Input.GetKey(movement))
        {
            if (Targets.ContainsKey(selectedEventPoint) && currentPath != null)
            {
                if (pathPos == 0)
                {
                    Move(currentEventpoint.transform.position, currentPath[pathPos].transform.position);
                }
                else
                {
                    Move(currentPath[pathPos-1].transform.position, currentPath[pathPos].transform.position);
                }
            }
        }

    }

    #endregion


    void Move(Vector3 startPos, Vector3 endPos)
    {
        lerpTime = Vector3.Distance(startPos, endPos) / 5f;
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float t = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(startPos, endPos, t);

        if (currentLerpTime >= lerpTime)
        {
            if (pathPos >= currentPath.Length - 1)
            {
                currentEventpoint = selectedEventPoint;
                Targets = GetTargets(currentEventpoint.gameObject.GetComponent<BEventPoint>().MyList);
                pathPos = 0;
                currentLerpTime = 0f;
                currentPath = null;
            }
            else
            {
                pathPos += 1;
                currentLerpTime = 0f;
            }
        }

    }

    private Dictionary<GameObject, int> GetTargets(List<BEventPoint.MyClass> ItemGroups)
    {
        int j = 0;
        Dictionary<GameObject, int>  targets = new Dictionary<GameObject, int>();
        foreach (BEventPoint.MyClass i in ItemGroups)
        {
            targets.Add(i.Path[i.Path.Length - 1],j);
            j += 1;
        }
        return targets;
    }
}
