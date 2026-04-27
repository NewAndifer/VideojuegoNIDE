using UnityEngine;

public class SelectorMusicaEscena : MonoBehaviour
{
    [SerializeField] private AudioClip musicaDeEstaEscena;

    void Start()
    {
        // Le ordenamos al Singleton que toque nuestra canción específica
        if (ControladorSonido.Instance != null && musicaDeEstaEscena != null)
        {
            ControladorSonido.Instance.PlayMusica(musicaDeEstaEscena);
        }
    }
}