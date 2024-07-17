using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public float globalVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, Transform parent, float startDelay = 0f, float endDelay = 0f, bool loop = false)
    {
        StartCoroutine(PlaySoundCoroutine(clip, parent, startDelay, endDelay, loop));
    }

    private IEnumerator PlaySoundCoroutine(AudioClip clip, Transform parent, float startDelay, float endDelay, bool loop)
    {
        yield return new WaitForSeconds(startDelay);

        GameObject audioObject = new GameObject("AudioSource_" + clip.name);
        audioObject.transform.SetParent(parent);
        audioObject.transform.localPosition = Vector3.zero;

        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = globalVolume;
        //audioSource.spatialBlend = 1.0f; // ทำให้เสียงเป็น 3D
        audioSource.loop = loop;
        audioSource.Play();

        if (!loop)
        {
            yield return new WaitForSeconds(clip.length + endDelay);
            Destroy(audioObject);
        }
    }

    public void StopLoopingSound(AudioClip clip)
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allAudioSources)
        {
            if (source.clip == clip && source.loop)
            {
                source.Stop();
                Destroy(source.gameObject);
                break;
            }
        }
    }
}