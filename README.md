# IAV25-IglesiasCalvo
# Proyecto Final: El Baile De Las Flores
Proyecto final de la asignatura Inteligencia Artificial para Videojuegos.

![diagram](./Imagenes/Abeja.png)

## Autor
Nahia Iglesias Calvo
* GitHub: nahigles https://github.com/nahigles
* Correo: nahigles@ucm.es

## Resumen
Este proyecto es la práctica final de la asignatura de Inteligencia Artificial para Videojuegos del Grado en Desarrollo de Videojuegos de la UCM.

Proyecto de videojuego creado en Unity 2022.3.40f1 reutilizando código de la [Práctica 1]([https://narratech.com/es/inteligencia-artificial-para-videojuegos/percepcion-y-movimiento/plaga-de-ratas/](https://github.com/IAV25-G06/IAV25-G06-P1)) de esta misma asignatura .

El proyecto consiste en un entorno virtual 3D que representa una colmena con sus abejas y un campo de hierba en el que hay flores.

Este proyecto simula la comunicación de las abejas para encontrar comida y alimentar a la colmena, utilizando para ello la danza de las abejas.
Para ello, las abejas volarán por el campo y podrán detectar una flor, volver a la colmena para dar el mensaje a las demás abejas y recolectar el néctar entre varias.


## Instalación y uso
Todo el contenido del proyecto está disponible aquí en el repositorio, pues Unity 2022.3.40f1 o posterior debería ser capaz de bajar todos los paquetes necesarios. Además está subida una release para Windows descargable en esta misma página.


## Motivación  

Las abejas tienen distintas formas de comunicarse dependiendo del mensaje que quieren transmitir a otras abejas. Para ello, utilizan distintos métodos como la danza, las feromonas, el contacto físico y las vibraciones.

Especialmente la danza de la abeja, transmite información esencial sobre la ubicación y distancia de fuentes de alimento. 

Las feromonas, sustancias químicas, transmiten mensajes sobre la reina, peligros o estados de ánimo.

El contacto físico y las vibraciones son usados para transmitir mensajes de alarma o de otro tipo también.

Para este proyecto, me he centrado en la danza de las abejas, esa interesante manera de comunicarse que tienen para dar mensajes sobre nuevos alimentos encontrados y su dirección y distancia. Me parece asombroso lo mucho que se puede entender solo con mirar un movimiento o un gesto y es lo que quiero reflejar en esta simulación.
 
## Información
### El baile de la abeja
Cuando una abeja obrera encuentra una flor con néctar o polen o cualquier otro tipo de alimento, vuelve a la colmena e informa a las demás abejas utilizando la danza. Las demás abejas imitan su movimiento para que les llegue el mensaje a todas y lo puedan interpretar. 
La danza se realiza sobre el panal y puede haber dos tipos dependiendo de la distancia a la que se encuentra el alimento.
* __Danza en círculo__
Esta danza consiste en movimientos circulares y la hacen si el alimento se encuentra a menos de 100 metros de la colmena. Las compañeras al interpretar este mensaje vuelan en círculo alrededor del panal buscando la comida.
* __Danza en ocho__
Es la danza más conocida de las abejas, la realizan si el alimento está situado a una distancia mayor que 100 metros de la colmena y con ella comunican exactamente la dirección y la distancia a la que se encuentra la comida.
La danza sigue un patrón en forma de medio círculo como se puede ver en la siguiente imagen y la realizan a distintos ángulos sobre el panal.
Las líneas curvas del centro del círculo indican el movimiento del abdomen de las abejas.

![diagram](./Imagenes/Danza.jpg) ![diagram](./Imagenes/Danza2.jpg)

#### _Distancia_
La distancia del alimento es proporcional a la velocidad a la que realizan el baile. Si es más rápido, el alimento está más cerca, y si es más lento está a más distancia.

![diagram](./Imagenes/DireccionDistanciaAngulo.png)

#### _Dirección_
Para la dirección del alimento lo indican con el ángulo que forman el eje Y de la colmena y el centro del baile. Ese mismo ángulo equivale al ángulo que hay entre el sol y el alimento tomando como centro la colmena.

![diagram](./Imagenes/DistancaDireccion.jpg)

En resumen, las abejas utilizan una variedad de métodos de comunicación para mantener la organización social y la cooperación en la colmena. El método de la danza permite a la colmena conseguir comida en grupo para que pueda seguir desarrollándose.


## Punto de partida

El punto de partida serán varios scripts de la [Práctica 1]([https://narratech.com/es/inteligencia-artificial-para-videojuegos/percepcion-y-movimiento/plaga-de-ratas/](https://github.com/IAV25-G06/IAV25-G06-P1)) de la asignatura de Inteligencia Artificial para videojuegos.

### Diagrama de clases:

![diagram](./Imagenes/UML.png)


### Agente
El script Agente.cs se encarga de cohesionar el movimiento, combinando, en el caso del perro por ejemplo, su seguimiento del flautista y su huida de las ratas. 
* En su método __fixedUpdate()__ regula la velocidad, rotación y aceleración.
* En su __Update()__ también regula la velocidad y ajusta la rotación, pero no regula aceleración ni comprueba la rotación. 
* En su __lateUpdate()__ nos aseguramos de que la velocidad y la aceleración estén reguladas, y calculamos nuestra próxima rotación y velocidad. 
* Al método __setComportamientoDirección()__ se le puede pasar un peso o una prioridad para hacer una suma de velocidades y combinar movimientos.
* Existe también el método __getPrioridadComportamientoDirección()__, que calcula los movimientos almacenados en una lista por prioridad para devolver un único vector. 
* Por último, tenemos __OriTovector()__, que transforma un float representando un ángulo en un vector representando la rotación, y LookDirection, que rota al agente en esa dirección (de manera gradual).

### Comportamiento Agente
El script ComportamientoAgente.cs avisa a una instancia del script agente para que combine,bien por peso o por prioridad, las velocidades, y traduce las rotaciones.

### Comportamiento Dirección
El script ComportamientoDireccion.cs guarda los valores de la velocidad lineal y angular.

### Comportamiento Separación
El script Separación.cs hereda de ComportamientoAgente.cs y consiste en un cálculo de las distancias entre el target y el jugador. Si es menor a un número predeterminado por parámetros, añade una velocidad en dirección contraria para que se separen.

### Comportamiento Merodeo
El script Merodear.cs hereda de ComportamientoAgente.cs y consiste en calcular cada t tiempo una nueva dirección y velocidad aleatoria.

### Comportamiento Llegada
El script Llegada.c hereda de ComportamientoAgente.cs. Este script ralentiza la velocidad al llegar a una distancia del target hasta que termina parándose. 


Todos ellos están implementados para comportamientos 2D, así que tendré que cambiarlos para que funcionen con movimiento en los 3 ejes.


### Planteamiento del problema
Tenemos un campo con flores, la colmena y las abejas controladas por IA. Se quiere implementar los siguientes comportamientos.
* __A__: Campo con flores, con abejas controladas por la IA que entran y salen de su colmena. La interfaz permite crear y destruir abejas, cambiar de cámara al campo o dentro de la colmena y habilitar y deshabilitar en la interfaz datos y métricas de la colmena.
* __B__: La colmena dispondrá de cantidad de comida y se indicará en la interfaz con una barra. Si la barra llega a 0 la colmena no sobrevive.
* __C__: Las flores irán creando polen o néctar, por lo tanto habrá veces que tengan alimento y otras no. Las flores deben ser detectadas por las abejas en caso de que contengan comida.
* __D__: Las abejas vuelan desordenadamente de forma individual en un espacio 3D hasta que encuentran alimento. Una vez encontrado alimento, vuelve a la colmena a comunicar el mensaje a las demás abejas (Danza de la abeja).Las abejas que reciben el mensaje son capaces de interpretar e ir en búsqueda de la flor con comida. Cada abeja tiene una capacidad limitada para coger polen y néctar.
* __E__:  El mundo tendrá lluvia que se podrá activar y desactivar mediante un botón en la interfaz. Las abejas vuelan a la colmena para resguardarse de la lluvia.

## Diseño de la solución

* __A.__
Para crear el campo con flores utilizaré hierba, flores y modelos creados por terceros a excepción de la colmena. Todos los modelos utilizados están en las referencias.

La interfaz estará hecha con UI Document y la diseñaré con los botones +, -, Cambio de cámara, Métricas, Adelantar Tiempo y Lluvia.

![diagram](./Imagenes/InterfazCampo.jpg)

Tendré una lista de GameObjects Abejas, cada vez que se pulse el + instanciaré una de ellas y en caso de pulsar - destruiré una en caso de que haya algún GameObject en la lista abejas. 

La lluvia estará creada con partículas de Unity e irá controlada por el GameManager también.

'''
class Game Manager:

	AddBee():
		Instantiate (beePrefab);


	DestroyBee():
		if (!beesList.empty)
			Destroy(beesList.last);


	Rain():
		if(rain.active)
			rain.active = false;
			rain.Deactivate();
		else
			rain.active = true;
			rain.Activate();


	Metrics():
		if(metrics.active)
			metrics.active = false;
			metrics.Deactivate();
		else
			metrics.active = true;
			metrics.Activate();
'''

* __B.__
Para la colmena, habrá un contador interno que irá guardando la cantidad de comida. La colmena tendrá un mínimo y un máximo que se reflejará en la UI con una barra.

Cada vez que una abeja vuelva con comida a la colmena se sumará la cantidad de polen que lleve la abeja al contador, y se restará 1 cada x segundos, ya que esto simulará el gasto de polen para que las abbejas se alimenten.

![diagram](./Imagenes/InterfazColmena.jpg)

* __C.__
Para las flores las abejas tendrán un Trigger y en caso de que entre en contacto con una flor, mirará si tiene comida para recogerla.

Las flores tendrán un contador que comienza en 0 y va aumentando con el tiempo hasta llegar a una cantidad N. Las abejas podrán recolectar polen si el contador es mayor que 0.

Otra de las opciones es detectar la flor teniendo en cuenta la distancia:

```
    class RegionalSenseManager:
        # A record in the notification queue, ready to notify the sensor
        # at the correct time.
        class Notification:
            times int
            sensor: Sensor
            signal: Signal


        # The list of sensors.
        sensors: Sensor[]


        # A queue of notifications waiting to be honored.
        notificationQueue: Notification[]


        # Introduces a signal into the game. This also calculates the
        # notifications that this signal will be needed.
        function addsignal(stgnal: Sugnal):
            # Aggregation phase.
            validSensors: Sensor[] = []


            for sensor in sensors:
                # Testung phase.


                # Check the modality first.
                if not sensor.detectsModality(signal.nodaltty):
                    continue


                # Find the distance of the signal and check range.
                distance = distance(signal.position, sensor.position)
                if signal.modality.maximumRange ‹ distance:
                    continue


                # Find the intensity of the signal and check
                # the threshold.
                intensity = signal.strength *
                        pow(signal.modality attenuation, distance)
                if intensity ‹ sensor.threshold:
                    continue


                # Perform additional modality specific checks.
                if not signal.modality.extraChecks(signal, sensor):
                    continue
                # Notification phase.

```


* __D.__
Para el movimiento de las abejas utilizaré los siguientes Scripts y los cambiaré para que el movimiento funcione en los 3 ejes y no solo en 2.

Para ir a la colmena cuando tengan comida o para ir a la flor cuando detecten una:
```
    class Pursue extends Seek:
    # The maximum prediction time.
    maxPrediction: float


    function getSteering() -> SteeringOutput:
    # 1. Calculate the target to delegate to seek
    # Work out the distance to target.
    direction = target. position - character-position
    distance = direction. length( )


    # Work out our current speed.
    speed = character.velocity. length()
    # Check if speed gives a reasonable prediction time.


    if speed <= distance / maxPrediction:
    prediction = maxPrediction


    # Otherwise calculate the prediction time.
    else:
    prediction = distance / speed


    # Put the target together.
    Seek. target = explicitTarget
    Seek. target. position += target.velocity * prediction


    # 2. Delegate to seek.
    return Seek.getSteering()
```

Para una llegada más suave a la colmena o  la flor:
```
    class Arrive:
    character: Kinematic
    target: Kinematic


        maxAcceleration: float
        maxSpeed: float


    # The radius for arriving at the target
    targetRadius: float
    # The radius for beginning to slow down 
    slowRadius: float


    # The time over which to achieve target speed
    timeToTarget: float = 0.1


    function getSteering() -> SteeringOutput:
    result = new SteeringOutput()


    # Get the direction to the target.
    direction = target.position - character.position
    distance = direction.length()


    # Check if we are there, return no steering
    if distance < targetRadius;
        return null


    # If we are outside the slowRadius, then move at max speed.
    if distance > slowRadius:
        targetSpeed = maxSpeed
    #Otherwise calculate a scale speed
    else:
        targetSpeed = maxSpeed * distance / slowRadius
    #The target velocity combines speed and direction
    targetVelocity = direction
    targetVelocity.normalize()
    targetVelocity *= targetSpeed


    #Acceleration tries to get to the target velocity
    result.linear = targetVelocity - character.velocity
    result.linear /= timeToTarget


    #Check if the acceleration is too fast
    if result.linear.length() > maxAcceleration:
        result.linear.normalize()
        result.linear *= maxAcceleration


    result.angular = 0
    return result

```

Para separación entre las abejas cuando vuelan:
```
    class Separation:
    character: Kinematic 
    maxAcceleration: float


    # A list of potential targets. 
    targets: Kinematic[]


    # The threshold to take action.
    threshold: float


    # The constant coefficient of decay for the inverse square law. decayCoefficient: float


    function getSteering() -> SteeringOutput:
    result = new SteeringOutput()


    # Loop through each target.
    for target in targets:
    # Check if the target is close.
    direction = target.position - character.position 
    distance = direction.length()


    if distance < threshold:
    # Calculate the strength of repulsion
    # (here using the inverse square law). 
    strength = min(
    decayCoefficient / (distance * distance), maxAcceleration)


    # Add the acceleration.
    direction.normalize()
    result.linear += strength * direction


    return result

```

Para movimiento aleatorio al volar:
```
    class Wander extends Face:
    # The radius and forward offset of the wander circle.
    wanderOffset: float 
    wanderRadius: float


    # The maximum rate at which the wander orientation can change.
    wanderRate: float


    # The current orientation of the wander target.
    wanderOrientation: float


    # The maximum acceleration of the character.
    maxAcceleration: float


    # Again we don't need a new target.


    function getSteering() -> SteeringOutput:
    # 1. Calculate the target to delegate to face
    # Update the wander orientation.
    wanderOrientation += randomBinomial() * wanderRate


    # Calculate the combined target orientation.
    targetOrientation = wanderOrientation + character.orientation


    # Calculate the center of the wander circle.
    target = character.position + wanderOffset * character.orientation.asVector()


    # Calculate the target location.
    target += wanderRadius * targetOrientation.asVector()


    # 2. Delegate to face.
    result - Face|getSteering()
    # 3. Now set the linear acceleration to be at full
    # acceleration in the direction of the orientation.
    result.linear = maxAcceleration * character.orientation.asVector()


    # Return it.
    return result

```

Para detectar a cuanta distancia y en qué dirección está la flor, se verá la velocidad y el ángulo respecto al eje Y con el que hace el baile una abeja que ha encontrado comida. La abeja que interpreta hará un cálculo y volará en linea recta o en círculos dependiendo del baile que haya hecho la otra abeja.


* __E.__
Habrá un botón de lluvia que activará un sistema de partículas cada vez que esté activado. La lluvia afecta a las abejas ya que estas huyen de ella y se resguardan en la colmena.


## Implementación
Las tareas se han realizado y se pueden ver reflejadas en la [pestaña de proyectos](https://github.com/users/nahigles/projects/1/views/1) de github.

## Pruebas y métricas
A continuación se explican las distintas pruebas que se llevarán a cabo para comprobar el correcto funcionamiento de este proyecto. Estas han sido creadas prestando atención directamente a los puntos de evaluación, por lo que se organizan siguiendo la misma estructura. Asimismo, se elaborará un video en el que estas pruebas se vean en acción.

- __A.__ En este punto se describe el mundo: Un campo con flores que tienen alimento y unas abejas con su colmena. Una interfaz que permite cambiar de cámara, añadir abejas, quitar abejas y habilitar y deshabilitar datos y métricas de la colmena.
1. Visualizar mundo con abejas, colmena, flores e interfaz
2. Click en el botón cámara para comprobar que cambia de la colmena al campo y viceversa.
3. Pulsamos la tecla O y comprobamos que se añade una abeja.
4. Pulsamos la tecla P y comprobamos que se quita una abeja.
5. Pulsamos tecla T y comprobamos que aparecen los datos del juego y desaparecen. (Número de abejas, FPS, controles…)
6. Pulsamos tecla R y observamos que se reinicia el juego.


- __B.__ Este apartado habla sobre la cantidad de comida en la colmena y la barra que indica al jugador cómo de llena está.
1. El jugador dejará el juego ejecutar durante 10 segundos y observará que la barra se llena correctamente cuando las abejas llevan comida a la colmena.
2. El jugador hará que las abejas no lleven comida a la colmena. Comprobamos que la barra de comida de la comida baja.


- __C.__ Este apartado habla sobre las flores, estas irán creando polen o néctar, por lo tanto habrá veces que tengan alimento y otras no. Pueden ser detectadas por las abejas y recoger su polen.
1. Dejar 15 segundos ejecutando y comprobar que las flores crean polen a medida que pasa el tiempo.
2. Comprobar que las flores son detectadas por las abejas, para ello esperamos y vemos que las abejas se acercan.
3. Visualizar que el polen disminuye cuando una abeja recoge parte de él.


- __D.__ Este apartado explica el comportamiento de las abejas: Las abejas merodean por el aire de forma individual en un espacio 3D hasta que encuentran alimento. Si encuentran alimento, vuelven a la colmena a comunicar el mensaje a las demás abejas haciendo el baile de la abeja. También son capaces de interpretar el mensaje y tienen distintas velocidades y ángulos.
1. Comprobamos que las abejas vuelan por el campo con aleatoriedad.
2. Comprobamos que detectan las flores.
3. Comprobamos que recoge polen y lo lleva a la colmena.
4. Comprobamos que realiza el baile de la abeja y que las demás entienden cada baile y localizan la flor encontrada correctamente.


- __E.__ Este apartado explica la lluvia. El jugador puede activar y desactivar la lluvia.
1. Pulsamos botón de activar y desactivar y nos fijamos que empieza a llover y viceversa, y que las abejas se resguardan en la colmena.

- Video: https://github.com/nahigles/IAV25-IglesiasCalvo/tree/main/Video

## Licencia
Federico Peinado, autor de la documentación, código y recursos de este trabajo, concedo permiso permanente a los alumnos de la Facultad de Informática de la Universidad Complutense de Madrid para utilizar este material, con sus comentarios y evaluaciones, con fines educativos o de investigación; ya sea para obtener datos agregados de forma anónima como para utilizarlo total o parcialmente reconociendo expresamente mi autoría.

Nahia Iglesias Calvo, autora de la documentación, código y recursos de este trabajo concedo permiso para reutilizar este material de manera libre siempre y cuando se utilice con fines educativos.


## Referencias
Los recursos de terceros utilizados son de uso público.

#### Modelos 3D
* [Flores](https://loafbrr.itch.io/flower-arrangement-pack)

* [Hierba](https://poly.pizza/m/dz_TvM39dC7)
 
* [Abeja]( https://essssam.itch.io/3d-leap-land)

* [Panal](https://poly.pizza/m/6Mqdrv1n3Oo)

#### Información e Imágenes abejas
* [Abeja Real](https://www.earth.com/news/mysterious-origins-of-western-honey-bees-revealed/)
* [Baile de las abejas](https://tierrasapicolas.com/karl-von-frisch-y-la-danza-de-las-abejas/)
* [Wikipedia](https://es.wikipedia.org/wiki/Danza_de_la_abeja)
* [Video Animación Danza Abejas](https://youtu.be/6Lnq_zZlYa4)
* [Video Danzas Abejas](https://youtu.be/4udl87_OzJA)

#### Package de Unity
*  [Renderer RP - Universal Render Pipeline (URP)](https://unity.com/features/srp/universal-render-pipeline)

#### Libros y apuntes
* AI for Games, _Ian Millington_.
* [Apuntes de la asignatura Inteligencia Artificial para Videojuegos en la página Narratech](https://narratech.com/es/inteligencia-artificial-para-videojuegos/percepcion-y-movimiento/), _Federico Peinado Gil_: he tenido como referencia el pseudocódigo de los diferentes comportamientos que impementamos en la Práctica 1 y demás información.
