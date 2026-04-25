using UnityEngine;
using TMPro;

public class LetreroDinamico : MonoBehaviour
{
    [SerializeField] private GameObject botonPrefab;
    [SerializeField] private Transform canvasPadre;

    // Eliminamos OnValidate y AlinearConCabeza para que no te mueva nada a mano

    public void AgregarOpcion(string tecla, string accion)
    {
        if (botonPrefab == null || canvasPadre == null) return;

        // Crea el botón dentro del canvas
        GameObject nuevoBoton = Instantiate(botonPrefab, canvasPadre);

        // RESET DE ESCALA: Muy importante para que el botón no se deforme 
        // si el NPC tiene escalas diferentes (ej. 1.5, 0.8)
        nuevoBoton.transform.localScale = Vector3.one;
        nuevoBoton.transform.localPosition = Vector3.zero;

        TMP_Text texto = nuevoBoton.GetComponentInChildren<TMP_Text>();
        if (texto != null)
        {
            texto.text = $"Presiona {tecla} para {accion}";
        }
    }

    public void Limpiar()
    {
        if (canvasPadre == null) return;
        
        foreach (Transform hijo in canvasPadre)
        {
            Destroy(hijo.gameObject);
        }
    }

    public void AjustarLado(bool aLaDerecha)
    {
        if (canvasPadre == null) return;
        
        RectTransform rect = canvasPadre.GetComponent<RectTransform>();
        
        // Valores que puedes ajustar manualmente según tu diseño
        float posX = aLaDerecha ? 4f : -4f;
        
        rect.localPosition = new Vector3(posX, rect.localPosition.y, rect.localPosition.z);
    }
}