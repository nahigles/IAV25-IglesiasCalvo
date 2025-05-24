using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    int contPolen = 3;
    int maxPolen = 3;

    // Contador tiempo
    float t = 3.0f; // cada cuanto aparece nuevo polen
    float actualT = 0.0f; // Contador de tiempo

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Si el polen de la flor no esta al maximo
        if (contPolen < maxPolen)
        {
            // Si ha pasado el tiempo para poder crear uno de polen
            if (t >= actualT)
            {
                contPolen++;
                t = 0.0f; // Reinicio contador
            }
            // Si no sigue contando el tiempo
            else
            {
                t += Time.deltaTime;
            }
        }
    }
}
