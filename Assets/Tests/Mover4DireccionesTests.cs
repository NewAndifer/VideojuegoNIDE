using NUnit.Framework;
using UnityEngine;

public class Mover4DireccionesTests
{
    private Mover4Direcciones scriptMovimiento;
    private GameObject gameObjectPrueba;

    [SetUp]
    public void PrepararTerreno()
    {
        gameObjectPrueba = new GameObject();
        scriptMovimiento = gameObjectPrueba.AddComponent<Mover4Direcciones>();
    }

    [TearDown]
    public void LimpiarTerreno()
    {
        Object.DestroyImmediate(gameObjectPrueba);
    }

    [Test]
    public void CalcularNuevaPosicion_SinInput_NoDeberiaMoverse()
    {
        Vector2 posInicial = new Vector2(5, 5);
        Vector2 inputFalso = Vector2.zero; 
        float velocidad = 5f;
        float tiempoSimulado = 1f;

        Vector2 resultado = scriptMovimiento.CalcularNuevaPosicion(posInicial, inputFalso, velocidad, tiempoSimulado);

        Assert.AreEqual(new Vector2(5, 5), resultado);
    }

    [Test]
    public void CalcularNuevaPosicion_InputDerecha_DeberiaSumarEnEjeX()
    {
        Vector2 posInicial = new Vector2(0, 0);
        Vector2 inputFalso = new Vector2(1, 0); 
        float velocidad = 3f;
        float tiempoSimulado = 1f;

        Vector2 resultado = scriptMovimiento.CalcularNuevaPosicion(posInicial, inputFalso, velocidad, tiempoSimulado);

        Assert.AreEqual(new Vector2(3, 0), resultado);
    }

    [Test]
    public void CalcularNuevaPosicion_InputDiagonal_DeberiaSumarEnAmbosEjes()
    {
        Vector2 posInicial = new Vector2(0, 0);
        
        Vector2 inputFalso = new Vector2(1, 1).normalized; 
        
        float velocidad = 5f;
        float tiempoSimulado = 1f;

        Vector2 resultado = scriptMovimiento.CalcularNuevaPosicion(posInicial, inputFalso, velocidad, tiempoSimulado);
        Assert.AreEqual(inputFalso.x * velocidad, resultado.x, 0.01f);
        Assert.AreEqual(inputFalso.y * velocidad, resultado.y, 0.01f);
    }
}