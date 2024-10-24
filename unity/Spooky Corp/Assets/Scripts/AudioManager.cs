using UnityEngine;
using System.Collections;  // Added this line for IEnumerator

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    private static AudioManager instance;
    
    // Audio source components
    public AudioSource musicSource;
    public AudioSource sfxSource;
    
    // Audio clips
    public AudioClip backgroundMusic;
    public float musicVolume = 1f;
    
    // Optional: Fade settings
    public float fadeSpeed = 1f;
    private float targetVolume;
    
    void Awake()
    {
        // Singleton pattern to ensure only one AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Initialize audio sources if not set in inspector
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.playOnAwake = false;
            }
            
            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
                sfxSource.loop = false;
                sfxSource.playOnAwake = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Start playing background music if assigned
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
    }
    
    // Play background music with optional fade
    public void PlayMusic(AudioClip musicClip, bool fade = false)
    {
        if (musicClip == null) return;
        
        musicSource.clip = musicClip;
        
        if (fade)
        {
            musicSource.volume = 0f;
            targetVolume = musicVolume;
            musicSource.Play();
            StartCoroutine(FadeMusicRoutine());
        }
        else
        {
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }
    
    // Play a sound effect
    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxClip == null) return;
        sfxSource.PlayOneShot(sfxClip);
    }
    
    // Pause/Resume music
    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
        else
            musicSource.UnPause();
    }
    
    // Adjust music volume
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
    }
    
    // Fade music volume
    private IEnumerator FadeMusicRoutine()
    {
        while (!Mathf.Approximately(musicSource.volume, targetVolume))
        {
            musicSource.volume = Mathf.MoveTowards(musicSource.volume, targetVolume, 
                fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
    
    // Stop music
    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    // Public access to instance
    public static AudioManager Instance
    {
        get { return instance; }
    }
}