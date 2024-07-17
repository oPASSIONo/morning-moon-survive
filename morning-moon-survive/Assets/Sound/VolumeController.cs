using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SoundEffectSlider;

    void Start()
    {
        // Set initial slider values
        if (SoundEffectSlider != null)
        {
            SoundEffectSlider.value = SoundManager.Instance.sfxVolume;
        }

        if (BGMSlider != null)
        {
            BGMSlider.value = SoundManager.Instance.bgmVolume;
        }

        // Add listeners to sliders
        SoundEffectSlider.onValueChanged.AddListener(SetSFXVolume);
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    public void SetSFXVolume(float volume)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.sfxVolume = volume;
        }
    }

    public void SetBGMVolume(float volume)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetBGMVolume(volume);
        }
    }
}