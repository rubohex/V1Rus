using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BMapCameraController : MonoBehaviour
{

    #region ATRIBUTES
    [Header("Data")]

    /// Datos de la Camara
    public DMapCameraInfo cameraInfo;

    /// Codigo de tecla para rotar -90º
    private KeyCode rotateLeft;
    /// Codigo de tecla para rotar -90º
    private KeyCode rotateRight;

    /// Codigo de tecla para mover hacia arriba
    private KeyCode rotateUp;

    /// Codigo de tecla para mover hacia abajo
    private KeyCode rotateDown;

    /// Codigo de tecla para mover a la izquierda
    private KeyCode rotateForward;

    /// Codigo de tecla para rotar -90º
    private KeyCode rotateBack;

    /// Velocidad de movimiento transversal de la camara
    private float cameraMoveSpeed;

    /// Velocidad de rotacion de la camara
    private float cameraRotationSpeed;

    /// Cubo al que enfocamos;
    public GameObject CurrentCenter;

    /// Controlar que mientras rote no se haga nada mas
    private bool isRotating = false;

    /// Base de coordenadas para controlar el giro de la cámara
    private int[] Base = new int[3];

    /// Target de la camara
    private Transform target;
    #endregion

    #region METHODS

    private void Awake()
    {
        // Inicializamos los valores en funcion de los datos
        rotateLeft = cameraInfo.rotateLeft;
        rotateRight = cameraInfo.rotateRight;
        rotateUp = cameraInfo.rotateUp;
        rotateDown = cameraInfo.rotateDown;
        rotateForward = cameraInfo.rotateForward;
        rotateBack = cameraInfo.rotateBack;
        cameraMoveSpeed = cameraInfo.moveSpeed;
        cameraRotationSpeed = cameraInfo.rotationSpeed;

        transform.parent.position = CurrentCenter.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = transform.parent;

        Base[0] = 1;
        Base[1] = 2;
        Base[2] = 3;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(rotateLeft))
        {
            transform.RotateAround(CurrentCenter.transform.position, Vector3.up, cameraRotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(rotateRight))
        {
            transform.RotateAround(CurrentCenter.transform.position, Vector3.down, cameraRotationSpeed * Time.deltaTime);
        }*/

        if (Input.GetKeyDown(rotateLeft) && !isRotating)
        {
            Vector3 NuevoVector = TranformaVectores(1, 1) * cameraRotationSpeed;
            // Añadimos una velocidad de giro

            GetComponentInParent<Rigidbody>().angularVelocity = NuevoVector;
            isRotating = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(NuevoVector));
        }
        else if (Input.GetKeyDown(rotateRight) && !isRotating)
        {
            Vector3 NuevoVector = TranformaVectores(-1, 1) * cameraRotationSpeed;
            // Añadimos una velocidad de giro

            GetComponentInParent<Rigidbody>().angularVelocity = NuevoVector;
            isRotating = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(NuevoVector));
        }
        else if (Input.GetKeyDown(rotateUp) && !isRotating)
        {
            Vector3 NuevoVector = TranformaVectores(1, 2) * cameraRotationSpeed;
            // Añadimos una velocidad de giro
            GetComponentInParent<Rigidbody>().angularVelocity = NuevoVector;
            isRotating = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(NuevoVector));
        }
        else if (Input.GetKeyDown(rotateDown) && !isRotating)
        {
            Vector3 NuevoVector = TranformaVectores(-1, 2) * cameraRotationSpeed;
            // Añadimos una velocidad de giro
            GetComponentInParent<Rigidbody>().angularVelocity = NuevoVector;
            isRotating = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(NuevoVector));
        }
        else if (Input.GetKeyDown(rotateForward) && !isRotating)
        {
            Vector3 NuevoVector = TranformaVectores(1, 0) * cameraRotationSpeed;
            // Añadimos una velocidad de giro
            GetComponentInParent<Rigidbody>().angularVelocity = NuevoVector;
            isRotating = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(NuevoVector));
        }
        else if (Input.GetKeyDown(rotateBack) && !isRotating)
        {
            Vector3 NuevoVector = TranformaVectores(-1, 0) * cameraRotationSpeed;
            // Añadimos una velocidad de giro
            GetComponentInParent<Rigidbody>().angularVelocity = NuevoVector;
            isRotating = true;
            // Llamamos a una rutina paralela para que pare el giro
            StartCoroutine(stopRotation(NuevoVector));
        }
    }
    #endregion

    /// <summary>
    /// Corrutina encargada de controlar que la rotacion sea de 90 grados
    /// </summary>
    /// <param name="vec"> vector con el sentido de rotación </param>
    IEnumerator stopRotation(Vector3 vec)
    {
        // Calculamos la nueva rotación.
        
        // Redondeamos el vector de rotación
        

        // Calculamos el tiempo que tenemos que esperar en funcion a la velocidad de rotacion
        float timeToWait = (Mathf.PI / 2) / cameraRotationSpeed;
        // Esperamos el tiempo necesario
        yield return new WaitForSeconds(timeToWait);

        // Paramos el giro
        GetComponentInParent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        // Redondeamos la rotacion para que sea exacta
        Vector3 newRotation = transform.parent.eulerAngles;
        Vector3 RoundedRotation = new Vector3(
            Mathf.Round(newRotation.x * 4 / 360) * 90,
            Mathf.Round(newRotation.y * 4 / 360) * 90,
            Mathf.Round(newRotation.z * 4 / 360) * 90);
        transform.parent.transform.eulerAngles = RoundedRotation;
        // Informamos que ha parado la rotacion
        isRotating = false;
    }

    /// <summary>
    /// Función para obtener el vector de giro según el giro solicitado y la Base de coordenadas actual
    /// </summary>
    /// <param name="direction"> 1 o -1 dependiendo de hacia donde gire</param>
    /// <param name="position"> 0, 1 o 2 dependiendo de la posición del eje de rotamiento que se vaya a modificar</param>

    Vector3 TranformaVectores(int direction, int position)
    {
        // Obtener el eje de rotamiento según la base
        int valor = Base[position];
        // Inicializa un array de 0
        int[] ret = new int[3];
        foreach (int i in ret)
        {
            ret[i] = 0;
        }

        // Obtener la dirección de rotamiento según la dirección solicitada y la que hay en la base
        int b = direction * (valor / Mathf.Abs(valor));
        ret[Mathf.Abs(valor)-1] = b;

        // Actualizar la nueva Base
        switch (position)
        {
            case 2:
                ActualizaBase(1, 0, direction*b);
                break;
            case 1:
                ActualizaBase(0, 2, direction*b);
                break;
            case 0:
                ActualizaBase(2, 1, direction*b);
                break;
        }

        return new Vector3(ret[0], ret[1], ret[2]); 
    }

    /// <summary>
    /// Función para actualizar la nueva Base, intercambiando dos elementos de la antigua Base y cambiando el signo de uno de ellos
    /// </summary>
    /// <param name="izq"> 0, 1 o 2 dependiendo de la posición del elemento de la izquierda del intercambio en la Base</param>
    /// <param name="der"> 0, 1 o 2 dependiendo de la posición del elemento de la derecha del intercambio en la Base</param>
    /// <param name="direction"> 1 o -1 dependiendo de hacia donde gire</param>
    /// 
    void ActualizaBase(int izq, int der, int direction)
    {
        int aux = Base[izq];
        Base[izq] = Base[der];
        Base[der] = aux;
        Base[der] *= -direction;
        Base[izq] *= direction;
    }


}
