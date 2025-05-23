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

        // Dir vertical
        int dirY = 1;
        bool up = true;

        ComportamientoDireccion lastDir = new ComportamientoDireccion();

        public override ComportamientoDireccion GetComportamientoDireccion()
        {


            if (t >= actualT)
            {

                Debug.Log("Vel Angular: " + lastDir.angular);
                Debug.Log("Vel Lineal: " + lastDir.lineal);

                float rotation = Random.Range(-1f, 1f) * agente.aceleracionAngularMax;

                lastDir.angular += rotation;

                lastDir.lineal = OriToVec(lastDir.angular).normalized; //la transformamos en su velocidad y la normalizamos

                Vector3 auxDir = Vector3.zero;
                if (up)                
                    auxDir = new Vector3(lastDir.lineal.x, -lastDir.lineal.y + dirY, lastDir.lineal.z);
                else
                    auxDir = new Vector3(lastDir.lineal.x, -lastDir.lineal.y -2*dirY, lastDir.lineal.z);
                

                lastDir.lineal = auxDir;


                //lastDir.lineal *= agente.aceleracionMax;  //va a maxima velocidad

                //calculamos el tiempo q vamos a estar en esta dirección
                actualT = Random.Range(tiempoMinimo, tiempoMaximo);
                t = 0;
                up = !up;
            }
            else
            {
                t += Time.deltaTime;
            }

            lastDir.angular = 0;

            if(transform.position.y < 0.5)
            {
                lastDir.lineal = new Vector3(lastDir.lineal.x, -lastDir.lineal.y + dirY, lastDir.lineal.z);
            }
            else if(transform.position.y > 4.5)
            {
                lastDir.lineal = new Vector3(lastDir.lineal.x, -lastDir.lineal.y - dirY, lastDir.lineal.z);
            }
            else if(transform.position.z > 5)
            {
                lastDir.lineal = new Vector3(lastDir.lineal.x, lastDir.lineal.y, -lastDir.lineal.z - dirY*5);
            }
            else if(transform.position.z < -11)
            {
                lastDir.lineal = new Vector3(lastDir.lineal.x, lastDir.lineal.y, -lastDir.lineal.z + dirY*5);
            }
            else if(transform.position.x < -19)
            {
                lastDir.lineal = new Vector3(-lastDir.lineal.x + dirY * 5, lastDir.lineal.y, lastDir.lineal.z);
            }
            else if(transform.position.x > 17)
            {
                lastDir.lineal = new Vector3(-lastDir.lineal.x - dirY * 5, lastDir.lineal.y, lastDir.lineal.z);
            }

            return lastDir;
        }

        private void Start()
        {
            //lastDir.lineal = 
        }
    }
}



