using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    public static PersistentObject Instance { get; private set; }

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // Ensure this object persists across scene changes
    }
}