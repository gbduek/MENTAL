using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1.0f;

    [Range(0f, 1f)]
    public float randomVolume = 0.1f;
    [Range(0f, 1f)]
    public float randomPitch = 0.1f;

    private AudioSource source;
    public void SetSource(AudioSource audioSource)
    {
        source = audioSource;
        source.clip = clip;
    }

    public void Play()
    {
        if (source == null)
        {
            Debug.LogWarning("Sound.Play() called but source is null for sound: " + name);
            return;
        }

        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
    public void Stop()
    {
        if (source != null)
            source.Stop();
    }
    public void Pause()
    {
        if (source != null)
            source.Pause();
    }
    public void UnPause()
    {
        if (source != null)
            source.UnPause();
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)  // só destrói se for outra instância diferente
            {
                Destroy(this.gameObject);
                Debug.LogWarning("AudioManager: Another instance already exists, destroying this one.");
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            // cria as fontes de áudio
            for (int i = 0; i < sounds.Length; i++)
            {
                GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
                _go.transform.SetParent(this.transform);
                sounds[i].SetSource(_go.AddComponent<AudioSource>());
            }
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("AudioManager: Sound " + _name + " not found!");
    }
    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        Debug.LogWarning("AudioManager: Sound " + _name + " not found!");
    }
    public void PauseSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Pause();
                return;
            }
        }
        Debug.LogWarning("AudioManager: Sound " + _name + " not found!");
    }
    public void UnPauseSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].UnPause();
                return;
            }
        }
        Debug.LogWarning("AudioManager: Sound " + _name + " not found!");
    }
}
