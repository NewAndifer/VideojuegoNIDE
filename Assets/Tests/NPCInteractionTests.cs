using NUnit.Framework;
using UnityEngine;

public class NPCInteractionTests
{
    private NPCInteraction npc;
    private GameObject gameObjectPrueba;

    [SetUp]
    public void PrepararTerreno()
    {
        gameObjectPrueba = new GameObject();
        npc = gameObjectPrueba.AddComponent<NPCInteraction>();

        npc.instructionalCards = new string[] 
        { 
            "Hola viajero", 
            "Tengo una misión para ti", 
            "Ve al bosque" 
        };
    }

    [TearDown]
    public void LimpiarTerreno()
    {
        Object.DestroyImmediate(gameObjectPrueba);
    }

    [Test]
    public void Interaccion_DeberiaActivarDialogo_SiEstaEnRango()
    {
        npc.SimularEntrarRango();
        npc.SimularBotonInteraccion();

        Assert.IsTrue(npc.GetIsDialogueActive());
        Assert.AreEqual(0, npc.GetCurrentCardIndex());
    }

    [Test]
    public void Interaccion_NoDeberiaHacerNada_SiNoEstaEnRango()
    {
        npc.SimularBotonInteraccion();

        Assert.IsFalse(npc.GetIsDialogueActive());
    }

    [Test]
    public void Interaccion_DeberiaAvanzarTarjeta_SiYaEstaActivo()
    {
        npc.SimularEntrarRango();
        
        npc.SimularBotonInteraccion(); 
        npc.SimularBotonInteraccion(); 


        Assert.IsTrue(npc.GetIsDialogueActive());
        Assert.AreEqual(1, npc.GetCurrentCardIndex());
    }

    [Test]

    public void SalirRango_DeberiaApagarDialogo_Instantaneamente()
    {
        npc.SimularEntrarRango();


        npc.SimularSalirRango();

        Assert.IsFalse(npc.GetIsDialogueActive());
        Assert.IsFalse(npc.GetIsPlayerInRange());
    }
}