using System.Collections;
using System.Collections.Generic;
using UCM.IAV.Movimiento;
using UnityEngine;

public class Flower : MonoBehaviour
{
    int contPolen = 3;
    int maxPolen = 3;

    // Contador tiempo polen
    float t = 60.0f; // cada cuanto aparece nuevo polen
    float actualT = 0.0f; // Contador de tiempo

    ParticleSystem particles = null;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si el polen de la flor no esta al maximo
        if (contPolen < maxPolen)
        {
            // Si ha pasado el tiempo para poder crear uno de polen
            if (t <= actualT)
            {
                contPolen++;
                var main = particles.main;
                main.startLifetime = contPolen;
                actualT = 0.0f; // Reinicio contador
            }
            // Si no sigue contando el tiempo
            else
            {
                actualT += Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Llegada llegadaBeeComp = other.GetComponent<Llegada>();
        Merodear merodeoBeeComp = other.GetComponent<Merodear>();
        Bee beeComp = other.GetComponent<Bee>();

        // Si es una abeja
        if (llegadaBeeComp != null && merodeoBeeComp != null && beeComp != null)
        {

            if (contPolen > 0 && !beeComp.IsFull())
            {
                // Desactiva componentee merodeo
                merodeoBeeComp.enabled = false;

                // Activa componente llegada
                llegadaBeeComp.objetivo = gameObject; // Esta flower como objetivo
                llegadaBeeComp.enabled = true;

                beeComp.TakePolen();

                contPolen--;
                var main = particles.main;
                main.startLifetime = contPolen;
            }
        }
    }
}
