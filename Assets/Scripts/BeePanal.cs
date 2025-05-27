using System.Collections;
using System.Collections.Generic;
using UCM.IAV.Movimiento;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BeePanal : MonoBehaviour
{
    // Contador tiempo abeja recoger polen
    float t = 7.0f;
    float actualT = 0.0f;
    bool contadorTiempo = false;

    bool ocupada = false;

    Vector3 initialPos;
    Quaternion initialRot;

    Transform myTransform;

    Transform salidaColmena;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        initialPos = myTransform.position;
        initialRot = myTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (contadorTiempo)
        {
            // Si ha pasado el tiempo para ir a la flor
            if (t <= actualT)
            {
                myTransform.position = salidaColmena.transform.position + initialPos;
                myTransform.rotation = salidaColmena.transform.rotation;

                // Activo agente y llegada
                Agente agenteBeePanal = GetComponent<Agente>();
                agenteBeePanal.enabled = true;
                Llegada llegadaCompAbjPanal = GetComponent<Llegada>();
                Debug.Log("Llegada null " + llegadaCompAbjPanal == null);
                llegadaCompAbjPanal.enabled = true;

                // Desfrezzeo
                GetComponent<Rigidbody>().freezeRotation = false;

                contadorTiempo = false;
                actualT = 0.0f;
            }
            // Si no sigue contando el tiempo
            else
            {
                actualT += Time.deltaTime;
            }
        }
    }

    public bool IsFree()
    {
        return !ocupada;
    }

    public void Work()
    {
        ocupada = true;
        contadorTiempo = true;
    }

    public void Free()
    {
        ocupada = false;
    }

    public Vector3 GetInitialPos()
    {
        return initialPos;
    }

    public Quaternion GetInitialRot()
    {
        return initialRot;
    }

    public void SetSalidaColmena(Transform salidaCol)
    {
        salidaColmena = salidaCol;
    }
}
