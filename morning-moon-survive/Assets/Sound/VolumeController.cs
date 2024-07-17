using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SoundEffectSlider;
    
    void Start()
    {
        // ตั้งค่า slider เริ่มต้น
        if (SoundEffectSlider != null)
        {
            SoundEffectSlider.value = SoundManager.Instance.globalVolume;
        }

        // เพิ่ม listener ให้ slider
        SoundEffectSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.globalVolume = volume;
        }
    }
}