using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // ใช้สำหรับการอ้างอิง Slider

[System.Serializable]
public class BGMSetting
{
    public AudioClip bgmClip;
    public float volume = 1.0f;
    public bool loop = true;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField]
    private BGMSetting defaultBGMSetting;

    [SerializeField]
    private BGMSetting[] bgmSettings;  // Array สำหรับเก็บการตั้งค่า BGM ตาม Scene

    [SerializeField]
    private Slider volumeSlider;  // ฟิลด์สำหรับอ้างอิง Slider

    private AudioSource bgmSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            bgmSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ตั้งค่า BGM เริ่มต้นตาม Scene ปัจจุบัน
        ChangeBGMForScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;

        // ตั้งค่า Slider และเพิ่ม event listener
        if (volumeSlider != null)
        {
            volumeSlider.value = GetBGMVolume();  // ตั้งค่าเริ่มต้นของ Slider จากระดับเสียงปัจจุบัน
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeBGMForScene(scene.name);
    }

    private void ChangeBGMForScene(string sceneName)
    {
        BGMSetting setting = System.Array.Find(bgmSettings, bgm => bgm.bgmClip.name == sceneName);

        if (setting != null)
        {
            PlayBGM(setting);
        }
    }

    public void PlayBGM(AudioClip bgmClip, float volume = 1.0f, bool loop = true)
    {
        if (bgmSource.isPlaying && bgmSource.clip == bgmClip)
            return;

        bgmSource.clip = bgmClip;
        bgmSource.volume = volume;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void PlayBGM(BGMSetting setting = null)
    {
        if (setting == null)
        {
            setting = defaultBGMSetting;
        }

        PlayBGM(setting.bgmClip, setting.volume, setting.loop);
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public float GetBGMVolume()
    {
        return bgmSource.volume;
    }

    private void OnVolumeChanged(float value)
    {
        SetBGMVolume(value);
    }

    public void PauseBGM()
    {
        bgmSource.Pause();
    }

    public void UnPauseBGM()
    {
        bgmSource.UnPause();
    }
}
