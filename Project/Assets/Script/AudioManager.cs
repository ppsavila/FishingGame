using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [field: SerializeField]
    private AudioMixer AudioMixer { get; set; }

    [field: SerializeField]
    private Slider MusicSlider { get; set; }

    [field: SerializeField]
    private Slider AmbientSlider { get; set; }
    [field: SerializeField]
    private Slider MasterSlider { get; set; }

    void Start()
    {
        // Inicializa os valores dos sliders e adiciona listeners
        if (MusicSlider != null)
        {
            MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        if (AmbientSlider != null)
        {
            AmbientSlider.onValueChanged.AddListener(SetAmbientVolume);
        }
        if (MasterSlider != null)
        {
            MasterSlider.onValueChanged.AddListener(SetMasterVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetAmbientVolume(float volume)
    {
        AudioMixer.SetFloat("Ambient", Mathf.Log10(volume) * 20);
    }

    public void SetMasterVolume(float volume)
    {
        AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
}
