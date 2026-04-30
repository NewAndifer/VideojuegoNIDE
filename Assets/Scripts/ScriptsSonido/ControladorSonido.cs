using UnityEngine;
using UnityEngine.Audio;

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

            // --- NUEVO: Cargamos los valores al abrir el juego ---
            // El '1f' es el valor por defecto si es la primera vez que juegan
            ultimoVolumenMusica = PlayerPrefs.GetFloat("VolumenMusica", 1f);
            ultimoVolumenSFX = PlayerPrefs.GetFloat("VolumenSFX", 1f);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Aplicamos el volumen cargado al Mixer justo al iniciar
        SetVolumenMusica(ultimoVolumenMusica);
        SetVolumenSFX(ultimoVolumenSFX);
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
        ultimoVolumenMusica = sliderValue;
        mainMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);

        // --- NUEVO: Guardamos permanentemente ---
        PlayerPrefs.SetFloat("VolumenMusica", sliderValue);
        PlayerPrefs.Save();
    }

    public void SetVolumenSFX(float sliderValue)
    {
        ultimoVolumenSFX = sliderValue;
        mainMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);

        // --- NUEVO: Guardamos permanentemente ---
        PlayerPrefs.SetFloat("VolumenSFX", sliderValue);
        PlayerPrefs.Save();
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