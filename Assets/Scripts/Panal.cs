using System.Collections;
using System.Collections.Generic;
using UCM.IAV.Movimiento;
using UnityEngine;
using UnityEngine.UI;

public class Panal : MonoBehaviour
{
    int polenNumber = 0;

    [SerializeField]
    int polenMin = 0;

    [SerializeField]
    int polenMax = 100;

    [SerializeField]
    GameObject colmena = null;

    [SerializeField]
    GameObject salidaColmena = null;

    [SerializeField]
    Slider slider = null;

    [SerializeField]
    GameObject abejasPanal = null;
    List<GameObject> abejasColmenaLista;

    float restaPolemTiempo = 15.0f;
    float contTiempo = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = polenMax;
        slider.minValue = polenMin;
        slider.value = polenNumber;

        // Guardo las abejas del panal en una lista
        int numAbejasColmena = abejasPanal.transform.childCount;
        abejasColmenaLista = new List<GameObject>(numAbejasColmena);
        for (int i = 0; i < numAbejasColmena; i++)
        {
            abejasColmenaLista.Add(abejasPanal.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(contTiempo >= restaPolemTiempo)
        {
            // Resta 1 de polen
            polenNumber--;
            slider.value = polenNumber;
            contTiempo = 0.0f;
        }
        else
        {
            contTiempo += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BeePanal beePanal = other.GetComponent<BeePanal>();
        // Si es una abeja se mete a la colmena
        Bee beeComp = other.GetComponent<Bee>();
        Llegada llegadaComp = other.GetComponent<Llegada>();
        Agente agenteComp = other.GetComponent<Agente>();


        // Si es una abeja obrera del panal
        if (beePanal != null)
        {
            // Se setea como libre
            beePanal.Free();

            // Se coloca en su posicion en el panal
            other.transform.position = beePanal.GetInitialPos();
            other.transform.rotation = beePanal.GetInitialRot();

            if (beeComp != null && llegadaComp != null && agenteComp != null)
            {
                // La abeja deja la comida que tenga
                int beePolen = beeComp.PutDownPolen();
                polenNumber += beePolen;
                slider.value = polenNumber;


                // Desactiva componente llegada
                llegadaComp.enabled = false;
                agenteComp.enabled = false;
                other.attachedRigidbody.velocity = Vector3.zero;
                other.attachedRigidbody.freezeRotation = true;
            }
        }
        // Si es una abeja obrera recolectora
        else
        {
            if (beeComp != null && llegadaComp != null && agenteComp != null)
            {
                // La abeja deja la comida que tenga
                int beePolen = beeComp.PutDownPolen();
                polenNumber += beePolen;
                slider.value = polenNumber;

                // Posicion de abeja a dentro del panal
                other.transform.position = colmena.transform.position;

                // Rotacion dependiendo de donde esta la flor
                salidaColmena.transform.LookAt(new Vector3(beeComp.flower.transform.position.x, salidaColmena.transform.position.y, beeComp.flower.transform.position.z));
                other.transform.rotation = colmena.transform.rotation * salidaColmena.transform.rotation; // Suma el angulo de la flor con el sol

                // Desactiva componente llegada
                llegadaComp.enabled = false;
                agenteComp.enabled = false;
                other.attachedRigidbody.velocity = Vector3.zero;
                other.attachedRigidbody.freezeRotation = true;

                // Hace baile para comunicar
                beeComp.Dance();

                // Si las abejas del panal estan libres, buscan la flor de la abeja que hace el baile
                for (int i = 0; i < abejasColmenaLista.Count; i++)
                {
                    BeePanal beePanalComp = abejasColmenaLista[i].GetComponent<BeePanal>();
                    if (beePanalComp != null && beePanalComp.IsFree())
                    {
                        beePanalComp.Work();

                        // Posiciono la abeja en la salida
                        beePanalComp.SetSalidaColmena(salidaColmena.transform);

                        // Activo llegada
                        Llegada llegadaCompAbjPanal = abejasColmenaLista[i].GetComponent<Llegada>();

                        // Seteo su objetivo hacia la flor de la abeja del baile
                        llegadaCompAbjPanal.objetivo = beeComp.flower;
                        abejasColmenaLista[i].GetComponent<Bee>().flower = beeComp.flower;
                    }
                }
            }
        }
    }
}
