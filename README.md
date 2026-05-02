# Kid Plays: CatWest


## Contexto Académico
Este proyecto es el repositorio del videojuego "CatWest Redemption" de la plataforma "Kid Plays".

* **Materia:** Construcción de software y toma de decisiones (Grupo 402)
* **Fecha:** 01 de Mayo de 2026
* **Profesores:** 
  * Antonio Cedillo Hernández
  * Roberto Martínez Román
  * Ariel Ortíz Ramírez
  * Jossian Abimelec García Quijano

## Sobre el Proyecto (Videojuego)
Este repositorio contiene **exclusivamente el desarrollo en Unity** del módulo "CatWest" para la plataforma educativa **Kid Plays**. Es un entorno interactivo diseñado para que los niños pierdan el miedo a las matemáticas a través de mecánicas de videojuegos clásicos.

El objetivo de esta arquitectura fue construir un juego modular, optimizado para exportarse a WebGL, permitiendo que en un futuro se integre sin problemas en una plataforma web.

## Arquitectura y Tecnologías Aplicadas
* **Motor Gráfico:** Unity 6 (Configurado para WebGL Build).
* **Lenguaje:** C#.
* **Patrones de Diseño Implementados:** 
  * **Singleton:** Aplicado en el `GameManager` y `ControladorSonido` para mantener la persistencia de datos (como el volumen y los estados de la interfaz) entre distintas escenas sin destruir los objetos.

## Retos Técnicos y Aprendizajes
Durante el desarrollo de la lógica del juego, resolvimos diversos problemas de ingeniería:
1. **Sincronización de Corrutinas:** Creación de secuencias de eventos asíncronos (como la cuenta regresiva del Boss y las animaciones) mediante `IEnumerator` sin bloquear el hilo principal (`Update`).
2. **Asimetría de Estados en UI:** Resolución de problemas de renderizado superpuesto al manejar las ventanas de "Pausa" y "Opciones" de forma modular como overlays.
3. **Optimización de Instancias:** Manejo de prefabs y ciclos de vida de proyectiles teledirigidos optimizados para no saturar la memoria en el entorno WebGL.

## Instrucciones de Ejecución (Unity)
Para evaluar el código fuente o probar el juego localmente en el motor:
1. Clona este repositorio: `git clone [tu-enlace-de-github]`
2. Abre **Unity Hub** y selecciona *Add Project from disk*.
3. Selecciona la carpeta clonada (Asegúrate de usar **Unity 6**).
4. En la ventana *Project*, ve a la carpeta de `Scenes` y abre la escena inicial (ej. `LogIn` o `MenuInicio`).
5. Presiona el botón de **Play** en el editor.

## Integrantes del Equipo
* Fernando Tovar Mejia - A01666534
* Patricio Carreras Baez - A01754749
* Jose Manuel Bañuelos Silva - A01803173
* Juan Manuel Sánchez Velázquez - A01802469
* David Elias Vargas Ayala - A01753587

---
*Desarrollado para la materia de Construcción de software y toma de decisiones.*
