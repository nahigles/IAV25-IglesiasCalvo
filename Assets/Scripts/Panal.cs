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

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = polenMax;
        slider.minValue = polenMin;
        slider.value = polenNumber;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        // Si es una abeja se mete a la colmena
        Bee beeComp = other.GetComponent<Bee>();
        Llegada llegadaComp = other.GetComponent<Llegada>();
        Agente agenteComp = other.GetComponent<Agente>();

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

        }
    }
}
