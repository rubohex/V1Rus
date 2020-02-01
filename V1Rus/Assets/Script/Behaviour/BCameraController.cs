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
    private float cameraRotationSpeed;

    /// Borde a partir del que notamos el raton en el mapa
    private float mapBorderThickness = 10f;

    /// Controlar que mientras rote no se haga nada mas
    private bool isRotating = false;

    /// Limites del mapa en Eje X y Z
    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    /// Target de la camara
    private Transform target;

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
        cameraMoveSpeed = cameraInfo.moveSpeed;
        cameraRotationSpeed = cameraInfo.rotationSpeed;
    }

    /// Start is called before the first frame update
    void Start()
    {
        // Guardamos el target
        target = transform.parent;

        // Hacemos que la camara se centre en el target
        transform.LookAt(target);

        // Obtenemos los limites del tablero
        Vector4 aux = FindObjectOfType<BBoard>().getBoardLimits();
        minX = aux.x;
        maxX = aux.y;
        minZ = aux.z;
        maxZ = aux.w;
    }

    /// Update is called once per frame
    private void Update()
    {
        ZoomHandler();
    }

    /// Late Update is called once per frame after Update
    private void LateUpdate()
    {
        // Control del giro a la izquierda
        if (Input.GetKeyDown(rotateLeft) && !isRotating)
        {
            // Añadimos una velocidad de giro
            GetComponentInParent<Rigidbody>().angularVelocity = new Vector3(0f, -cameraRotationSpeed, 0f);
            isRotating = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(-1));
        }

        // Control del giro a la derecha
        if (Input.GetKeyDown(rotateRight) && !isRotating)
        {
            // Añadimos una velocidad de giro
            GetComponentInParent<Rigidbody>().angularVelocity = new Vector3(0f, cameraRotationSpeed, 0f);
            isRotating = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(1));
        }

        // Guardamos la posicion del target y las direcciones de la camara
        Vector3 pos = target.position;
        Vector3 camRight = Camera.main.transform.right;
        Vector3 camFordward = Camera.main.transform.forward;

        //Comprobamos los movimientos en todas las direcciones tanto con raton como con las teclas designadas
        if (Input.GetKey(moveUp) || Input.mousePosition.y >= Screen.height - mapBorderThickness)
        {
            camFordward.y = 0;
            camFordward.Normalize();

            pos.x += camFordward.x * cameraMoveSpeed * Time.deltaTime;
            pos.z += camFordward.z * cameraMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveDown) || Input.mousePosition.y <= mapBorderThickness)
        {
            camFordward.y = 0;
            camFordward.Normalize();

            pos.x -= camFordward.x * cameraMoveSpeed * Time.deltaTime;
            pos.z -= camFordward.z * cameraMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveRight) || Input.mousePosition.x >= Screen.width - mapBorderThickness)
        {
            pos.x += camRight.x * cameraMoveSpeed * Time.deltaTime;
            pos.z += camRight.z * cameraMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveLeft) || Input.mousePosition.x <= mapBorderThickness)
        {
            pos.x -= camRight.x * cameraMoveSpeed * Time.deltaTime;
            pos.z -= camRight.z * cameraMoveSpeed * Time.deltaTime;
        }

        // Acotamos los movimientos dentro del tablerro
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        // Asignamos las nueva posicion a la camara
        transform.parent.transform.position = pos;
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
    /// Corrutina encargada de controlar que la rotacion sea de 90 grados
    /// </summary>
    /// <param name="direction"> -1 o 1 dependiendo del sentido de la rotacion </param>
    IEnumerator stopRotation(int direction)
    {
        // Guardamos la rotacion anterior
        Vector3 prevRotation = target.eulerAngles;

        // Calculamos el tiempo que tenemos que esperar en funcion a la velocidad de rotacion
        float timeToWait = (Mathf.PI / 2) / cameraRotationSpeed;
        // Esperamos el tiempo necesario
        yield return new WaitForSeconds(timeToWait);

        // Paramos el giro
        GetComponentInParent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        // Redondeamos la rotacion para que sea exacta
        transform.parent.transform.eulerAngles = prevRotation + new Vector3(0, direction * 90, 0);
        // Informamos que ha parado la rotacion
        isRotating = false;
    }

    #endregion
}
