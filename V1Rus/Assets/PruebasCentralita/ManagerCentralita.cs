using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using PowerUI;
using System.Text.RegularExpressions;

public class ManagerCentralita : MonoBehaviour
{
    public GameObject[] objetos;
    public GameObject centralita;

    HtmlElement Scrolling;
    HtmlDocument file;

    public int accionesMax = 3;

    int seed = 0;
    int nRelleno = 0;
    char[] caracteresHex = { 'A','B','C','D','E','F','0','1','2','3','4','5','6','7','8','9' };
    char[] caracteresBin = { '0', '1' };

    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        string nombreC = gameObject.name;
        byte[] nombreSeed = System.Text.Encoding.UTF8.GetBytes(nombreC);
        seed = 0;
        foreach (byte b in nombreSeed)
        {
            seed += b;
        }
        UnityEngine.Random.InitState(seed);

        centralita.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (centralita.activeSelf)
        {
            if (costeTotal() > accionesMax)
            {
                file = UI.document;
                HtmlElement elem = file.getById("centralita");
                elem.style.background = "#f00";
            }
            else
            {
                file = UI.document;
                HtmlElement elem = file.getById("centralita");
                elem.style.background = "#fff";
            }

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    HtmlElement elemDiv;
                    float scrollPoin;

                    foreach (GameObject g in objetos)
                    {
                        if (hit.collider.gameObject == g)
                        {
                            elemDiv = file.getElementById(hit.collider.gameObject.name) as HtmlElement;
                            scrollPoin = elemDiv.offsetTop;

                            float limit = Scrolling.contentHeight - Scrolling.scrollHeight;

                            if (limit < 0)
                            {
                                limit = 0;
                            }

                            // Clip it by the limits:
                            if (scrollPoin < 0)
                            {
                                // Dragged the content down really far.
                                scrollPoin = 0;
                            }
                            else if (scrollPoin > limit)
                            {
                                // Dragged the content up too far.
                                scrollPoin = limit;
                            }

                            Scrolling.scrollTo(0, scrollPoin);
                        }

                    }
                }
            }
        }
    }
    public void changedValuePower(Dom.Event e)
    {
        String name;
        HtmlElement target = e.target as HtmlElement;
        name = target.htmlParentNode.id;
        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i].name == name)
            {
                
                bool valor = int.Parse(target.value) == 1;
                objetos[i].GetComponent<Activable>().activate(valor);
            }
        }
        print(costeTotal());
    }
    public void changedValueRotation(Dom.Event e)
    {
        String name;
        HtmlElement target = e.target as HtmlElement;
        name = target.htmlParentNode.id;

        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i].name == name)
            {
                float valor = float.Parse(target.value);
                objetos[i].GetComponent<Activable>().activate(valor);
            }
        }
        print(costeTotal());
    }
    public void changedValuePosition(Dom.Event e)
    {
        String name;
        HtmlElement target = (e.target as HtmlElement).htmlParentNode;
        name = target.htmlParentNode.id;

        for (int i = 0; i < objetos.Length; i++)
        {
            if (objetos[i].name == name)
            {
                float[] pos = { 0, 0 };
                for (int j = 0; j < target.children.length; j++)
                {
                    HtmlInputElement elem = target.children[j] as HtmlInputElement;
                    if (elem.value != null)
                    {
                        string value = Regex.Replace(elem.value, "[^-0-9]", "");
                        int val;
                        bool result = int.TryParse(value, out val);
                        if (result)
                        {
                            pos[j] = val;
                        }
                    }                   
                }
                objetos[i].GetComponent<Activable>().activate(pos);
            }
        }
        print(costeTotal());
    }

    void setUpCentralita()
    {
        UnityEngine.Random.InitState(seed);

        file = UI.document;
        string contenido = file.innerHTML;
        string[] trozos = contenido.Split(new string[] { "##ELEM##" }, StringSplitOptions.None);
        string cadena = "";

        foreach (GameObject g in objetos)
        {
            cadena = addRelleno(cadena);
            string trozo = trozos[1].Replace("##NAME##", g.name);

            string[] trozosAux = trozo.Split(new string[] { "##POWER##" }, StringSplitOptions.None);
            string trozoIni = trozosAux[0];
            string trozoPower = trozosAux[1];

            trozosAux = trozo.Split(new string[] { "##ROTATION##" }, StringSplitOptions.None);
            string trozoRotation = trozosAux[1];

            trozosAux = trozo.Split(new string[] { "##POSITION##" }, StringSplitOptions.None);
            string trozoPosition = trozosAux[1];
            string trozoFin = trozosAux[2];

            string trozoTot = "";
            if (g.name.Contains("Terminal"))
            {
                trozoTot = trozoPower;
            }
            else if (g.name.Contains("Muro"))
            {
                trozoTot = trozoPosition;
            }
            else if(g.name.Contains("Capsule"))
            {
                trozoTot = trozoPower + trozoRotation + trozoPosition;
            }
            else if (g.name.Contains("Cylinder"))
            {
                trozoTot = trozoPower + trozoRotation;
            }

            string aux = trozoIni + trozoTot + trozoFin;
            cadena = cadena + aux;
        }
        cadena = addRelleno(cadena);

        file.innerHTML = trozos[0] + cadena + trozos[2];

        Scrolling = (file.getElementById("textoScroll") as HtmlElement);
        setListeners();
        setSelected();
    }

    private string addRelleno(string cadena)
    {
        char[] caracteres = caracteresBin;

        float val = UnityEngine.Random.value;
        nRelleno = (int)(val*10) + 100;
        nRelleno = 20 * (nRelleno % 20);

        string relleno = "<div style=\"width: 100%;overflow-wrap: break-word;color: #808080;\">";
        for (int i = 0; i < nRelleno; i++)
        {
            int indice = (int)(UnityEngine.Random.value * (caracteres.Length));
            while(indice >= caracteres.Length)
            {
                indice = (int)(UnityEngine.Random.value * (caracteres.Length));
            }
            relleno += caracteres[indice];
        }

        cadena = cadena + relleno + "</div>";

        return cadena;
    }

    void setSelected()
    {
        file = UI.document;
        foreach (GameObject g in objetos)
        {
            HtmlElement elem;
            elem = (file.getElementById(g.name + "Power") as HtmlSelectElement);
            if(elem != null)
            {
                int val = Convert.ToInt32(g.GetComponent<Activable>().getActivePower());
                elem.selectedIndex = val;
            }

            elem = (file.getElementById(g.name + "Rotation") as HtmlSelectElement);
            if (elem != null)
            {
                int val = (int)g.GetComponent<Activable>().getActiveRotation() / 90;
                elem.selectedIndex = val;
            }

            elem = (file.getElementById(g.name + "Position") as HtmlElement);
            if (elem != null)
            {
                float[] pos = g.GetComponent<Activable>().getActivePosition();
                HtmlElement elem1 = file.getElementById(g.name + "Position1") as HtmlInputElement;
                int val1 = (int)pos[0];
                HtmlElement elem2 = file.getElementById(g.name + "Position2") as HtmlInputElement;
                int val2 = (int)pos[1];

                elem1.value = val1.ToString();
                elem2.value = val2.ToString();
            }
        }
    }

    void setListeners()
    {
        file = UI.document;
        foreach (GameObject g in objetos)
        {
            if (g.name.Contains("Terminal"))
            {
                file.getElementById(g.name + "Power").addEventListener("change", changedValuePower);
            }
            else if (g.name.Contains("Muro"))
            {
                file.getElementById(g.name + "Position").addEventListener("change", changedValuePosition);
            }
            else if (g.name.Contains("Capsule"))
            {
                file.getElementById(g.name + "Power").addEventListener("change", changedValuePower);
                file.getElementById(g.name + "Rotation").addEventListener("change", changedValueRotation);
                file.getElementById(g.name + "Position").addEventListener("change", changedValuePosition);
            }
            else if (g.name.Contains("Cylinder"))
            {
                file.getElementById(g.name + "Power").addEventListener("change", changedValuePower);
                file.getElementById(g.name + "Rotation").addEventListener("change", changedValueRotation);
            }
        }
    }

    int costeTotal()
    {
        int coste = 0;
        foreach (GameObject g in objetos)
        {
            coste += g.GetComponent<Activable>().getCoste();
        }
        return coste;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            centralita.SetActive(true);
            setUpCentralita();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            centralita.SetActive(false);

        }
    }
}
