using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    // Contador tiempo abeja recoger polen
    float t = 7.0f;
    float actualT = 0.0f;
    bool polen = false;
    int polenCont = 0;
    int maxPolen = 2;
    ParticleSystem particles = null;


    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
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
            }
            // Si no sigue contando el tiempo
            else
            {
                t += Time.deltaTime;
            }
        }
    }

    public void TakePolen()
    {
        polen = true;
    }

    public bool IsFull()
    {
        return (polenCont == maxPolen);
    }
}
