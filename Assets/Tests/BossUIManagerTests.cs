using NUnit.Framework;
using UnityEngine;

public class BossUIManagerTests
{
    private BossUIManager bossUI;
    private GameObject gameObjectPrueba;

    [SetUp]
    public void PrepararTerreno()
    {
        gameObjectPrueba = new GameObject();
        bossUI = gameObjectPrueba.AddComponent<BossUIManager>();
    }

    [TearDown]
    public void LimpiarTerreno()
    {
        Object.DestroyImmediate(gameObjectPrueba);
    }

    [Test]
    public void StartTimer_DeberiaReiniciarTiempoYActivar()
    {
        //Iniciamos el temporizador
        bossUI.StartTimer();

        //Comprobamos que el tiempo sea 60 y esté activo
        Assert.AreEqual(60f, bossUI.GetTimeRemaining());
        Assert.IsTrue(bossUI.GetIsTimerActive());
    }

    [Test]
    public void StopTimer_DeberiaDesactivarTemporizador()
    {
        //Primero lo iniciamos para que esté activo
        bossUI.StartTimer();

        //Lo detenemos
        bossUI.StopTimer();

        //Comprobamos que ya no esté activo
        Assert.IsFalse(bossUI.GetIsTimerActive());
    }


    [Test]
    public void FormatearTiempo_DeberiaConvertirSegundosAString()
    {
        //Evaluamos diferentes escenarios
        
        // 60 segundos = 1 minuto
        string resultado1 = bossUI.FormatearTiempo(60f);
        
        // 65 segundos = 1 minuto con 5 segundos
        string resultado2 = bossUI.FormatearTiempo(65f);
        
        // 0 segundos = 00:00
        string resultado3 = bossUI.FormatearTiempo(0f);
        
        // 125 segundos = 2 minutos con 5 segundos
        string resultado4 = bossUI.FormatearTiempo(125f);

        Assert.AreEqual("01:00", resultado1);
        Assert.AreEqual("01:05", resultado2);
        Assert.AreEqual("00:00", resultado3);
        Assert.AreEqual("02:05", resultado4);
    }
}