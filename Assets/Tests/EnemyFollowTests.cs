using NUnit.Framework;
using UnityEngine;

public class EnemyFollowTests
{
    private EnemyFollow enemigo;
    private GameObject gameObjectPrueba;

    [SetUp]
    public void PrepararTerreno()
    {
        gameObjectPrueba = new GameObject();
        enemigo = gameObjectPrueba.AddComponent<EnemyFollow>();
    }

    [TearDown]
    public void LimpiarTerreno()
    {
        Object.DestroyImmediate(gameObjectPrueba);
    }

    [Test]
    public void CalcularNuevaPosicion_DeberiaMoverse_SiEstaLejosDelJugador()
    {
        Vector2 posicionEnemigo = new Vector2(0, 0);
        Vector2 posicionJugador = new Vector2(10, 0); 
        float velocidad = 2f;
        float distanciaFrenado = 4f;
        float tiempoSimulado = 1f; 

        Vector2 resultado = enemigo.CalcularNuevaPosicion(posicionEnemigo, posicionJugador, velocidad, distanciaFrenado, tiempoSimulado);

        
        Assert.AreEqual(new Vector2(2, 0), resultado);
    }

    [Test]
    public void CalcularNuevaPosicion_NoDeberiaMoverse_SiEstaDentroDeDistanciaDeFrenado()
    {
        Vector2 posicionEnemigo = new Vector2(0, 0);
        Vector2 posicionJugador = new Vector2(3, 0); // Distancia de 3 (menor al frenado de 4)
        float velocidad = 2f;
        float distanciaFrenado = 4f;
        float tiempoSimulado = 1f;

        Vector2 resultado = enemigo.CalcularNuevaPosicion(posicionEnemigo, posicionJugador, velocidad, distanciaFrenado, tiempoSimulado);

        Assert.AreEqual(new Vector2(0, 0), resultado);
    }
}
