using UnityEngine;
using UnityEngine.Audio; // Necesario para el Mixer

public class ControladorSonido : MonoBehaviour
{
    public static ControladorSonido Instance;

    [Header("Canales de Audio")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Mixer")]
    [SerializeField] private AudioMixer mainMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // --- EFECTOS (SFX) ---
    public void EjecutarSonido(AudioClip sonido)
    {
        sfxSource.PlayOneShot(sonido);
    }

    // --- MÚSICA (BGM) ---
    public void PlayMusica(AudioClip cancion, bool loop = true)
    {
        if (musicSource.clip == cancion) return;
        
        musicSource.clip = cancion;
        musicSource.loop = loop;
        musicSource.Play();
    }

    // --- MÉTODOS PARA EL MENÚ DE AJUSTES ---
    public void SetVolumenMusica(float sliderValue)
    {
        // El Mixer usa escala logarítmica (dB), por eso usamos Mathf.Log10
        mainMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetVolumenSFX(float sliderValue)
    {
        mainMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }
}