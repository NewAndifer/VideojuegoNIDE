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

    [Header("Valores Guardados")]
    public float ultimoVolumenMusica = 1f;
    public float ultimoVolumenSFX = 1f;

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
        // Guardamos el valor para que la UI lo pueda leer después
        ultimoVolumenMusica = sliderValue;
        mainMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetVolumenSFX(float sliderValue)
    {
        // Guardamos el valor
        ultimoVolumenSFX = sliderValue;
        mainMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }

    public void StopMusica()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }

    public void StopSFX()
    {
        sfxSource.Stop();
        sfxSource.clip = null;
    }
}