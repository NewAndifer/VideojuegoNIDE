using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PreguntasTests
{
    private Preguntas generadorPreguntas;
    private GameObject gameObjectPrueba;

    [SetUp]
    public void PrepararTerreno()
    {
        gameObjectPrueba = new GameObject();
        generadorPreguntas = gameObjectPrueba.AddComponent<Preguntas>();
    }

    [TearDown]
    public void LimpiarTerreno()
    {
        Object.DestroyImmediate(gameObjectPrueba);
    }


    [Test]
    public void ResultOperation_Suma_DeberiaSumarTodosLosNumeros()
    {
        List<int> numeros = new List<int> { 5, 10 };

        int resultado = generadorPreguntas.resultOperation("suma", numeros);

        Assert.AreEqual(15, resultado);
    }

    [Test]
    public void ResultOperation_Resta_DeberiaOrdenarYRestarCorrectamente()
    {
        List<int> numeros = new List<int> { 3, 10 };

        int resultado = generadorPreguntas.resultOperation("resta", numeros);

        Assert.AreEqual(7, resultado);
    }

    [Test]
    public void ResultOperation_Multiplicacion_DeberiaMultiplicarTodosLosNumeros()
    {
        List<int> numeros = new List<int> { 4, 5 };

        int resultado = generadorPreguntas.resultOperation("multiplicacion", numeros);

        Assert.AreEqual(20, resultado);
    }


    [Test]
    public void StringOperation_Suma_DeberiaGenerarTextoConSimboloMas()
    {
        List<int> numeros = new List<int> { 8, 4 };

        string textoGenerado = generadorPreguntas.stringOperation("suma", numeros);

        Assert.AreEqual("8+4", textoGenerado);
    }

    [Test]
    public void StringOperation_Multiplicacion_DeberiaGenerarTextoConSimboloX()
    {
        List<int> numeros = new List<int> { 7, 3 };

        string textoGenerado = generadorPreguntas.stringOperation("multiplicacion", numeros);

        Assert.AreEqual("7x3", textoGenerado);
    }


    [Test]
    public void BotonIncorrecto_DeberiaRestarUnaVida()
    {
        generadorPreguntas.vidas = 5;

        generadorPreguntas.botonIncorrecto(0);

        Assert.AreEqual(4, generadorPreguntas.vidas);
    }

    [Test]
    public void BotonCorrecto_DeberiaSumarUnAcierto()
    {
        generadorPreguntas.aciertosAcumulados = 0;

        generadorPreguntas.botonCorrecto(0);

        Assert.AreEqual(1, generadorPreguntas.aciertosAcumulados);
    }


    [Test]
    public void BotonIncorrecto_DeberiaDispararEventoFallo()
    {
        bool eventoDisparado = false;
        
        System.Action accionFallo = () => eventoDisparado = true;
        Preguntas.OnFallo += accionFallo;

        generadorPreguntas.botonIncorrecto(0);

        Assert.IsTrue(eventoDisparado);

        Preguntas.OnFallo -= accionFallo;
    }

    [Test]
    public void BotonCorrecto_DeberiaDispararEventoAcierto()
    {
        bool eventoDisparado = false;
        System.Action accionAcierto = () => eventoDisparado = true;
        Preguntas.OnAcierto += accionAcierto;

        generadorPreguntas.botonCorrecto(0);

        Assert.IsTrue(eventoDisparado);

        Preguntas.OnAcierto -= accionAcierto;
    }
}