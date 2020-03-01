using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPathPlatform : MonoBehaviour
{


    /// <summary>
    /// Datos del nivel
    /// </summary>
    public DPathPlatformInfo PlatformInfo;

    /// <summary>
    /// Enumerado para definir el tipo de coordenada que vamos a usar
    /// </summary>
    public enum ECord
    {
        XY,
        XZ,
        YZ
    }

    /// Guardamos el valor del systema de coordenadas
    private ECord cordSys;

    private void Awake()
    {
        // Primero observamos el sistema de coordenadas que vamos a utilizar
        // El sistema elegido dependera de las coordenadas que provengan del boardInfo
        if (PlatformInfo.x && PlatformInfo.y)
        {
            cordSys = ECord.XY;
        }
        else if (PlatformInfo.x && PlatformInfo.z)
        {
            cordSys = ECord.XZ;
        }
        else if (PlatformInfo.y && PlatformInfo.z)
        {
            cordSys = ECord.YZ;
        }
        else
        {
            Debug.LogError("Por favor indique en la informacion del tablero dos coordenadas para el systema de coordenadas");
        }

        //Obtenemos todas las casillas de la escena activas
        Object[] boardTiles = FindObjectsOfType(typeof(BEventPoint));

        //A partir de todas estas casillas obtenemos sus posiciones en el sistema
        List<float> firstPositions = new List<float>();
        List<float> secondPositions = new List<float>();
        foreach (BTile item in boardTiles)
        {
            // Dependiendo del sistema de coordenadas guardamos unos valores diferentes
            switch (cordSys)
            {
                case ECord.XY:
                    firstPositions.Add(item.gameObject.transform.position.x);
                    secondPositions.Add(item.gameObject.transform.position.y);
                    break;
                case ECord.XZ:
                    firstPositions.Add(item.gameObject.transform.position.x);
                    secondPositions.Add(item.gameObject.transform.position.z);
                    break;
                case ECord.YZ:
                    firstPositions.Add(item.gameObject.transform.position.y);
                    secondPositions.Add(item.gameObject.transform.position.z);
                    break;
                default:
                    break;
            }
        }
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
