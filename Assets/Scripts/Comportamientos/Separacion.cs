/*    
   Copyright (C) 2020-2025 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.TextCore.Text;

namespace UCM.IAV.Movimiento
{
    public class Separacion : ComportamientoAgente
    {
        /// <summary>
        /// Separa al agente
        /// </summary>
        /// <returns></returns>

        // Entidades potenciales de las que huir
        public GameObject targEmpty;

        // Umbral en el que se activa
        [SerializeField]
        float umbral;

        // Coeficiente de reducción de la fuerza de repulsión
        [SerializeField]
        float decayCoefficient;

        public void Start()
        {
            umbral = 20.0f;
            decayCoefficient = 2.0f;
            enabled = false;
        }

        public override ComportamientoDireccion GetComportamientoDireccion()
        {
            ComportamientoDireccion result = new ComportamientoDireccion();
            // IMPLEMENTAR separación
            foreach (Transform child in targEmpty.transform)
            {
                //Check if the target is close.
                Vector3 dir = child.position - transform.position;
                float distance = dir.magnitude;

                if (distance < umbral)
                {
                    float strength = Math.Min(decayCoefficient / (distance * distance), agente.aceleracionMax);
                    dir.Normalize();
                    result.lineal += strength * dir;
                }
            }
            return result;
        }
    }
}