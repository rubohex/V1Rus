using System.Collections;
using UnityEngine;

public class BCameraController : MonoBehaviour
{
    #region ATRIBUTES

    [Header("Data")]

    /// Datos de la Camara
    public DCameraInfo cameraInfo;

    /// Codigo de tecla para rotar -90º
    private KeyCode rotateLeft;
    /// Codigo de tecla para rotar -90º
    private KeyCode rotateRight;

    /// Codigo de tecla para mover hacia arriba
    private KeyCode moveUp;

    /// Codigo de tecla para mover hacia abajo
    private KeyCode moveDown;

    /// Codigo de tecla para mover a la izquierda
    private KeyCode moveLeft;

    /// Codigo de tecla para rotar -90º
    private KeyCode moveRight;

    /// Velocidad de movimiento transversal de la camara
    private float cameraMoveSpeed;

    /// Velocidad de rotacion de la camara
    private float cameraRotationTime;

    /// Borde a partir del que notamos el raton en el mapa
    private float mapBorderThickness = 10f;

    /// Controlar que mientras rote no se haga nada mas
    private bool isRotating = false;

    /// Limites del mapa en Eje X y Z
    private float min1;
    private float max1;
    private float min2;
    private float max2;

    /// Target de la camara
    private Transform parentT;

    /// Enumerado del tipo de sistema de coordenadas
    private BBoard.ECord coordSys;

    #endregion

    #region METHODS

    private void Awake()
    {
        // Inicializamos los valores en funcion de los datos
        rotateLeft = cameraInfo.rotateLeft;
        rotateRight = cameraInfo.rotateRight;
        moveUp = cameraInfo.moveUp;
        moveDown = cameraInfo.moveDown;
        moveLeft = cameraInfo.moveLeft;
        moveRight = cameraInfo.moveRight;
        cameraMoveSpeed = cameraInfo.cameraSpeed;
        cameraRotationTime = cameraInfo.rotationTime;
    }

    /// Start is called before the first frame update
    void Start()
    {
        // Guardamos el target
        parentT = transform.parent;

        // Hacemos que la camara se centre en el target
        transform.LookAt(parentT);

        // Obtenemos el tablero
        BBoard board = FindObjectOfType<BBoard>();

        // Obtenemos los limites del tablero
        Vector4 aux = board.getBoardLimits();
        min1 = aux.x;
        max1 = aux.y;
        min2 = aux.z;
        max2 = aux.w;

        // Colocamos la camara con la posicion inicial
        parentT.position = board.getPlayerSpawnPos(0f);
        parentT.rotation = board.getPlayerSpawnRot();

        coordSys = board.getCoordSys();
    }

    /// Update is called once per frame
    private void Update()
    {
        ZoomHandler();
    }

    /// Late Update is called once per frame after Update
    private void LateUpdate()
    {
        Vector3 pos = parentT.position;
        Vector3 camRight = transform.right;
        Vector3 camFordward = transform.forward;

        // Control del giro a la izquierda
        if (Input.GetKeyDown(rotateLeft) && !isRotating)
        {
            // Llamamos a una rutina paralela para que controle el giro
            StartCoroutine(RotateOverTimeCoroutine(parentT, cameraRotationTime, parentT.rotation, Quaternion.LookRotation(-parentT.right, parentT.up)));
        }

        // Control del giro a la derecha
        if (Input.GetKeyDown(rotateRight) && !isRotating)
        {
            // Llamamos a una rutina paralela para que controle el giro
            StartCoroutine(RotateOverTimeCoroutine(parentT, cameraRotationTime, parentT.rotation, Quaternion.LookRotation(parentT.right, parentT.up)));
        }


        if (Input.GetKey(moveUp) || Input.mousePosition.y >= Screen.height - mapBorderThickness)
        {
            pos.x += camFordward.x * cameraMoveSpeed * Time.deltaTime;
            pos.y += camFordward.y * cameraMoveSpeed * Time.deltaTime;
            pos.z += camFordward.z * cameraMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveDown) || Input.mousePosition.y <= mapBorderThickness)
        {
            pos.x -= camFordward.x * cameraMoveSpeed * Time.deltaTime;
            pos.y -= camFordward.y * cameraMoveSpeed * Time.deltaTime;
            pos.z -= camFordward.z * cameraMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveRight) || Input.mousePosition.x >= Screen.width - mapBorderThickness)
        {
            pos.x += camRight.x * cameraMoveSpeed * Time.deltaTime;
            pos.y += camRight.y * cameraMoveSpeed * Time.deltaTime;
            pos.z += camRight.z * cameraMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveLeft) || Input.mousePosition.x <= mapBorderThickness)
        {
            pos.x -= camRight.x * cameraMoveSpeed * Time.deltaTime;
            pos.y -= camRight.y * cameraMoveSpeed * Time.deltaTime;
            pos.z -= camRight.z * cameraMoveSpeed * Time.deltaTime;
        }

        switch (coordSys)
        {
            case BBoard.ECord.XY:
                parentT.position = new Vector3(Mathf.Clamp(pos.x,min1,max1), Mathf.Clamp(pos.y, min2, max2),0);
                break;
            case BBoard.ECord.XZ:
                parentT.position = new Vector3(Mathf.Clamp(pos.x, min1, max1),0,Mathf.Clamp(pos.z, min2, max2));
                break;
            case BBoard.ECord.YZ:
                parentT.position = new Vector3(0, Mathf.Clamp(pos.y, min1, max1), Mathf.Clamp(pos.z, min2, max2));
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Control del zoom
    /// </summary>
    private void ZoomHandler()
    {
        // Obtenemos el valor del zooom a partir de la recta
        float scrollValue = Input.mouseScrollDelta.y;

        // Calculamos el nuevo valor de la ventana a partir del zoom
        float newSize = Camera.main.orthographicSize - scrollValue;

        // Colocamos el nuevo valor de la camara
        Camera.main.orthographicSize = Mathf.Clamp(newSize,3.0f,20.0f);
    }

    /// <summary>
    /// Corutina encargada de rotar un objeto a una rotacion indicada en el tiempo indicado
    /// </summary>
    /// <param name="targetObject"> GameObject Objeto que vamos a mover </param>
    /// <param name="transitionDuration"> Float Tiempo que dura la transicion </param>
    /// <param name="start"> Quaternion Marca la rotacion de inicio </param>
    /// <param name="target"> Quaternion Marca la rotacion final </param>
    private IEnumerator RotateOverTimeCoroutine(Transform targetTransform, float transitionDuration, Quaternion start, Quaternion target)
    {
        // Marcamos que la camara esta rotando
        isRotating = true;

        // Iniciamos el timer a 0
        float timer = 0.0f;

        // Mientras el tiempo sea menor que la duracion de la transicion repetimos
        while (timer < transitionDuration)
        {
            // Aumentamos el tiempo en funcion de el Time.deltaTime
            timer += Time.deltaTime;
            // Calculamos el porcentaje del tiempo que llevamos
            float t = timer / transitionDuration;
            // Transformamos el porcentaje segun una funcion para que la transicion sea suave
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            // Hacemos un Lerp en funcion de la rotacion objetivo la original y el procentaje de tiempo que ha pasado
            targetTransform.rotation = Quaternion.Slerp(start, target, t);

            yield return null;
        }

        isRotating = false;

        yield return null;
    }

    #endregion
}
