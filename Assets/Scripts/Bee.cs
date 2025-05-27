using System.Collections;
using System.Collections.Generic;
using UCM.IAV.Movimiento;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Bee : MonoBehaviour
{
    // Contador tiempo abeja recoger polen
    float danceTime = 13.0f;
    bool dancing = false;
    float t = 7.0f;
    float actualT = 0.0f;
    bool polen = false;
    int polenCont = 0;
    int maxPolen = 2;
    ParticleSystem particles = null;
    Llegada llegadaComp = null;

    public GameObject flower = null;
    GameObject colmena = null;
    GameObject campoFlores = null;
    Collider colliderComp = null;

    [SerializeField]
    Animator animator = null;
    [SerializeField]
    Animator childAnimator = null;
    float speedFactor = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        llegadaComp = GetComponent<Llegada>();
        colmena = GameObject.Find("TriggerEntrada");
        campoFlores = GameObject.Find("Flowers");
        colliderComp = GetComponent<Collider>();

        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (polen)
        {
            // Si ha pasado el tiempo para recoger 1 de polen
            if (t >= actualT)
            {
                polenCont = 1;
                particles.Play();
                polen = false;
                t = 0.0f; // Reinicio contador
                llegadaComp.objetivo = colmena; // Se dirige a la colmena
                gameObject.layer = LayerMask.NameToLayer("AbejaConPolen");
            }
            // Si no sigue contando el tiempo
            else
            {
                t += Time.deltaTime;
            }
        }
        if (dancing)
        {
            // Si ha pasado el tiempo de bailar
            if (t >= danceTime)
            {
                t = 0.0f; // Reinicio contador

                StopDancing();
            }
            // Si no sigue contando el tiempo
            else
            {
                t += Time.deltaTime;
            }
        }
    }

    // Coger polen
    public void TakePolen()
    {
        polen = true;
    }

    // Dejar polen
    public int PutDownPolen()
    {
        int aux = polenCont;
        polenCont = 0;
        particles.Stop();

        return aux;
    }

    // Devuelve true si esta lleno de polen
    public bool IsFull()
    {
        return (polenCont == maxPolen);
    }

    // Activa animacion de baile y desactiva el de volar
    public void Dance()
    {
        dancing = true;
        childAnimator.enabled = false;
        animator.enabled = true;

        // Velocidad del baile dependiente de la distancia de la distancia
        animator.speed = speedFactor / (flower.transform.position - colmena.transform.position).magnitude; 
    }

    private void StopDancing()
    {
        dancing = false;
        animator.enabled = false;
        animator.speed = 0.0f;
        childAnimator.enabled = true;


        //// Volver al campo
        //GetComponent<Agente>().enabled = true;
        //llegadaComp.enabled = true;

        //transform.position = colmena.transform.position;
        //transform.rotation = colmena.transform.rotation;
        //llegadaComp.objetivo = campoFlores; // Se dirige al campo
    }
}
