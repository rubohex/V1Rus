using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBaliza : MonoBehaviour
{

    #region Atributos

    /// <summary>
    /// Baliza de destino a la que irá el jugador
    /// </summary>
    public GameObject balizaDestino;


    /// Lugar que ocupará la pantalla de la terminal
    private GameObject pantalla1;

    /// Pantalla de la terminal
    private GameObject testPantalla;

    private BGameManager gameManager;

    /// Determina si se puede usar o no
    public bool utilizable = true;

    private BBoard boardScript;
    #endregion

    public void SetupBaliza(BGameManager manager)
    {
        boardScript = manager.GetActiveBoard();
        gameManager = manager;

        pantalla1 = this.transform.Find("Pantalla").gameObject;

        testPantalla = this.transform.Find("CanvasPrueba2/Pantalla").gameObject;
        testPantalla.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {

        pantalla1 = this.transform.Find("Pantalla").gameObject;

        testPantalla = this.transform.Find("CanvasPrueba2/Pantalla").gameObject;
        testPantalla.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (utilizable && (boardScript != null) && (boardScript == gameManager.GetActiveBoard()))
        {
            // Actualiza la posición de la pantalla en el canvas
            BBoard board = gameManager.GetActiveBoard();
            Camera cam = board.GetCamera().GetComponent<Camera>();
            Vector3 newPos = cam.WorldToScreenPoint(pantalla1.transform.position);
            testPantalla.transform.position = newPos;

            // Actualiza la escala de la pantalla para simular la perspectiva
            float dist = Vector3.Distance(cam.transform.position, this.transform.position);
            testPantalla.transform.localScale = Vector3.one / dist * 10f;
        }
    }

    /// Mientras el jugador esté al lado de la terminal se muestra la pantalla y se actualiza el texto
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player") && utilizable)
        {
            testPantalla.SetActive(true);
        }
            

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            pantalla1.SetActive(false);
            testPantalla.SetActive(false);
        }
    }


    public void goNextPlatform()
    {
        pantalla1.SetActive(false);
        testPantalla.SetActive(false);
        //StartCoroutine(gameManager.SwapActiveBoard(true));
    }

}
