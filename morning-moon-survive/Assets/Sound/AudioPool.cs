using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    public static AudioPool Instance;

    public int poolSize = 10;
    public GameObject audioPrefab;
    private Queue<AudioSource> audioSources;
    public float globalVolume = 1f; // ตัวแปรสำหรับจัดการ volume ทั่วไป

    void Awake()
    {
        Instance = this;
        audioSources = new Queue<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(audioPrefab);
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            obj.SetActive(false);
            audioSources.Enqueue(audioSource);
        }
    }

    public void PlaySound(AudioClip clip, float startDelay = 0f, float endDelay = 0f, bool loop = false)
    {
        if (audioSources.Count > 0)
        {
            AudioSource audioSource = audioSources.Dequeue();
            audioSource.gameObject.SetActive(true);
            audioSource.clip = clip;
            audioSource.volume = globalVolume; // ใช้ globalVolume ในการตั้งค่า
            audioSource.loop = loop; // ตั้งค่า loop

            StartCoroutine(PlaySoundWithDelay(audioSource, clip.length, startDelay, endDelay, loop));
        }
    }

    private IEnumerator PlaySoundWithDelay(AudioSource audioSource, float clipLength, float startDelay, float endDelay, bool loop)
    {
        yield return new WaitForSeconds(startDelay);
        audioSource.Play();

        if (!loop)
        {
            yield return new WaitForSeconds(clipLength + endDelay);
            audioSource.gameObject.SetActive(false);
            audioSources.Enqueue(audioSource);
        }
    }

    public void StopLoopingSound(AudioClip clip)
    {
        foreach (var source in audioSources)
        {
            if (source.clip == clip && source.loop)
            {
                source.Stop();
                source.loop = false;
                source.gameObject.SetActive(false);
                audioSources.Enqueue(source);
                break;
            }
        }
    }
}
