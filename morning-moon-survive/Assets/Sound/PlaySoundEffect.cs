using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlaySoundEffect : MonoBehaviour
{
    [System.Serializable]
    public class SoundSettings
    {
        public GameObject targetObject;
        public AudioClip soundEffect;
        public float startDelay = 0f;
        public float endDelay = 0f;
        public bool loop = false;
    }
    
    public List<SoundSettings> objectSoundSettings = new List<SoundSettings>();
    public SoundSettings bgmSettings;
    public bool loopBGM = true;

    private void Start()
    {
        PlayBGM();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PlaySound(0);
        }
        
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            PlaySound(1);
        }

    }

    public void AddSoundSettings(SoundSettings settings)
    {
        objectSoundSettings.Add(settings);
    }

    public void PlaySound(int index)
    {
        if (index >= 0 && index < objectSoundSettings.Count)
        {
            var settings = objectSoundSettings[index];

            // Create an AudioSource dynamically
            if (settings.targetObject != null)
            {
                AudioSource audioSource = CreateAudioSource(settings.targetObject, settings.soundEffect, settings.loop);
                
                audioSource.Play();
                
                if (!settings.loop)
                {
                    Destroy(audioSource.gameObject, settings.soundEffect.length + settings.endDelay);
                }
            }
            else
            {
                Debug.LogWarning("Target object is not assigned for SoundSettings index " + index);
            }
        }
    }

    /*public void StopSound(int index)
    {
        
    }*/

    private void PlayBGM()
    {
        if (bgmSettings != null && bgmSettings.soundEffect != null)
        {
            SoundManager.Instance.PlaySound(bgmSettings.soundEffect, transform, bgmSettings.startDelay, bgmSettings.endDelay, loopBGM);
        }
    }

    private AudioSource CreateAudioSource(GameObject parent, AudioClip clip, bool loop)
    {
        GameObject audioObject = new GameObject("AudioSource_" + clip.name);
        audioObject.transform.SetParent(parent.transform);
        audioObject.transform.localPosition = Vector3.zero;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = SoundManager.Instance.globalVolume;
        audioSource.spatialBlend = 1.0f;
        audioSource.loop = loop;

        return audioSource;
    }
}
