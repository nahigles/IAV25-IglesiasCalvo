/*    
   Copyright (C) 2020-2025 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

namespace UCM.IAV.Movimiento
{


    /// <summary>
    /// Clase para modelar el comportamiento de HUIR a otro agente
    /// </summary>
    public class Huir : ComportamientoAgente
    {
        [SerializeField]
        float rango = 3;

        [SerializeField]
        int maxRats = 3;

        bool following = true;
        Llegada llegadaComp = null;
        private AudioSource audioSource;


        private void Start()
        {
            llegadaComp = GetComponent<Llegada>();
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Obtiene la dirección
        /// </summary>
        /// <returns></returns>
        public override ComportamientoDireccion GetComportamientoDireccion()
        {
            ComportamientoDireccion result = new ComportamientoDireccion();
            Vector3 velAcum = Vector3.zero;
            Vector3 aux = Vector3.zero;
            int numrats = 0;

            // Recorro hijos
            foreach (Transform child in objetivo.transform)
            {
                aux = transform.position - child.position;

                if (aux.magnitude < rango)
                {
                    velAcum += aux;
                    numrats++;
                }
            }

            if (numrats > maxRats && following)
            {
                // Deja de seguir al jugador
                following = false;
                llegadaComp.enabled = false;
                audioSource.Play();
            }
            else if (numrats < maxRats && !following)
            {
                // Vuelve a seguir al jugador
                following = true;
                llegadaComp.enabled = true;
            }

            // Get the direction away from the target.
            result.lineal = velAcum;

            // The velocity is along this direction, at full speed.
            result.lineal.Normalize();
            result.lineal *= agente.velocidadMax;

            result.angular = 0;

            return result;
        }
    }
}
