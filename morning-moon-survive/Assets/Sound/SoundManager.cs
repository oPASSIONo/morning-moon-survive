using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public float sfxVolume = 1f;
    public float bgmVolume = 1f;

    private AudioSource bgmAudioSource;

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

    public void PlayBGM(AudioClip clip, float startDelay = 0f, float endDelay = 0f, bool loop = true)
    {
        StartCoroutine(PlayBGMCoroutine(clip, startDelay, endDelay, loop));
    }

    private IEnumerator PlayBGMCoroutine(AudioClip clip, float startDelay, float endDelay, bool loop)
    {
        yield return new WaitForSeconds(startDelay);

        if (bgmAudioSource == null)
        {
            GameObject bgmObject = new GameObject("BGM_AudioSource");
            bgmObject.transform.SetParent(transform);
            bgmAudioSource = bgmObject.AddComponent<AudioSource>();
        }

        bgmAudioSource.clip = clip;
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.loop = loop;
        bgmAudioSource.Play();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = bgmVolume;
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
        audioSource.volume = sfxVolume;
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
