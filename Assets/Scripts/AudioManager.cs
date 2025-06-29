using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum MixerGroup
{
    Master,
    Music,
    SFX
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {  get; private set; }

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private Dictionary<MixerGroup, string> mixerGroups;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        mixerGroups = new()
        {
            { MixerGroup.Master, "MasterVolume" },
            { MixerGroup.Music, "MusicVolume" },
            { MixerGroup.SFX, "SFXVolume" }
        };
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

    public void SetMixerVolume(MixerGroup group, float normalizedValue)
    {
        string groupString = mixerGroups[group];
        float volume = Mathf.Log10(normalizedValue) * 20;

        audioMixer.SetFloat(groupString, volume);
    }

    public float GetMixerVolume(MixerGroup group, bool normalize = true)
    {
        string groupString = mixerGroups[group];
        
        audioMixer.GetFloat(groupString, out float volume);

        if (normalize)
            volume = Mathf.Pow(10, volume / 20);

        return volume;
    }
}