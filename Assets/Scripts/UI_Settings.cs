using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private TextMeshProUGUI masterPercent;
    [SerializeField] private TextMeshProUGUI musicPercent;
    [SerializeField] private TextMeshProUGUI sfxPercent;

    void Start()
    {
        masterSlider.onValueChanged.AddListener(OnMasterVolumeValueChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeValueChanged);

        masterSlider.SetValueWithoutNotify(AudioManager.instance.GetMixerVolume(MixerGroup.Master));
        musicSlider.SetValueWithoutNotify(AudioManager.instance.GetMixerVolume(MixerGroup.Music));
        sfxSlider.SetValueWithoutNotify(AudioManager.instance.GetMixerVolume(MixerGroup.SFX));
        
        UpdatePercentText(masterSlider, masterPercent);
        UpdatePercentText(musicSlider, musicPercent);
        UpdatePercentText(sfxSlider, sfxPercent);
    }

    private void UpdatePercentText(Slider slider, TextMeshProUGUI text)
    {
        text.text = $"{Mathf.Floor(slider.value * 100)}%";
    }

    private void OnMasterVolumeValueChanged(float value)
    {
        AudioManager.instance.SetMixerVolume(MixerGroup.Master, value);
        UpdatePercentText(masterSlider, masterPercent);
    }
    
    private void OnMusicVolumeValueChanged(float value)
    {
        AudioManager.instance.SetMixerVolume(MixerGroup.Music, value);
        UpdatePercentText(musicSlider, musicPercent);
    }
    
    private void OnSFXVolumeValueChanged(float value)
    {
        AudioManager.instance.SetMixerVolume(MixerGroup.SFX, value);
        UpdatePercentText(sfxSlider, sfxPercent);
    }
}