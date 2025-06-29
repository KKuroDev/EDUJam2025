using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {  get; private set; }

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void PlayMusic()
    {
        if (!musicAudioSource.isPlaying)
            musicAudioSource.Play();
    }

    public void StopMusic()
    {
        if (musicAudioSource.isPlaying)
            musicAudioSource.Stop();
    }

    public void PlaySFX()
    {

    }
}