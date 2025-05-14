/*    
   Copyright (C) 2020-2025 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using System.Collections;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Networking.UnityWebRequest;
using UnityEngine.TextCore.Text;
//using UnityEditor.PackageManager;
using UnityEngine;


namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de WANDER a otro agente
    /// </summary>
    public class Merodear : ComportamientoAgente
    {
        [SerializeField]
        float tiempoMaximo = 5.0f;

        [SerializeField]
        float tiempoMinimo = 1.0f;

        //para q primero rote
        float t = 3.0f;
        float actualT = 2.0f;

        ComportamientoDireccion lastDir = new ComportamientoDireccion();

        public override ComportamientoDireccion GetComportamientoDireccion(){


            if (t >= actualT) 
            {
                float rotation = Random.Range(-1f, 1f) * agente.aceleracionAngularMax;

                lastDir.angular += rotation;

                lastDir.lineal = OriToVec(lastDir.angular).normalized; //la transformamos en su velocidad y la normalizamos
                lastDir.lineal *= agente.aceleracionMax;  //va a maxima velocidad

                //calculamos el tiempo q vamos a estar en esta dirección
                actualT = Random.Range(tiempoMinimo, tiempoMaximo);
                t = 0;
            }
            else
            {
                t += Time.deltaTime;
            }

            return lastDir;
        }

    }
}



