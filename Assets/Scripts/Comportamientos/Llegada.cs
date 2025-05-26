/*    
   Copyright (C) 2020-2025 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using System;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class Llegada : ComportamientoAgente
    {  
        // El radio para considerar que ya ha llegado al objetivo  
        public float targetRadius;

        // El radio en el que se empieza a ralentizarse
        public float slowRadius;

        // El tiempo en el que conseguir la aceleracion objetivo
        float timeToTarget = 0.1f;

        // Tiempo maximo de prediccion para persecucion
        public float maxPrediction = 1.0f;

        public void Start()
        { 
            enabled = false;
        }

        public override ComportamientoDireccion GetComportamientoDireccion()
        {
            ComportamientoDireccion result = new ComportamientoDireccion();

            //Calcular direccion y distancia hasta el objetivo
            Vector3 direction = objetivo.transform.position - agente.transform.position;
            float distance = direction.magnitude;

            //Velocidad actual del agente
            float speed = agente.velocidad.magnitude;


            //Determinar el tiempo de prediccion
            float prediction;
          
            if (speed <= distance / maxPrediction)
            {
                prediction = maxPrediction;
            }

            else
            {
                prediction = distance / speed;
            }

            //Obtener el componente Agente del objetivo
            //Agente agenteObjetivo = objetivo.GetComponent<Agente>();

            //Verificar si el objetivo tiene el script Agente antes de usarlo
            if(objetivo != null)
            {
                //Calcular la posicion futura del objetivo
                Vector3 targetPosition = objetivo.transform.position;
                //Recalcular direccion con la posicion predicha
                direction = targetPosition - agente.transform.position;
                distance = direction.magnitude;
            }


            //Verificar si se encuentra dentro del radio de llegada para detenerse
            if (distance < targetRadius)
            { 
                return result;

            }
                
            float targetSpeed;
            //Si esta fuera de slowRadius se mueve a maxima velocidad
            if (distance > slowRadius)
            {
                targetSpeed = agente.velocidadMax;
            }
            //Si esta dentro se ajusta la velocidad entre la velocidad maxima y 0
            //a medida que se acerca al objetivo
            else {

                targetSpeed = agente.velocidadMax * distance / slowRadius;
            }

            //Velocidad en direccion al objetivo
            Vector3 targetVelocity = direction;
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            //La aceleracion necesaria            
            result.lineal = targetVelocity - agente.velocidad;
            result.lineal /= timeToTarget;

            //Limitar la aceleracion para que no sea mayor a la maxima
            if(result.lineal.magnitude > agente.aceleracionMax)
            {
                result.lineal.Normalize();
                result.lineal *= agente.aceleracionMax;
            }

            //Sin rotacion
            result.angular = 0;
            return result;
        }

    }
}
