using UnityEngine;

// Put one in every scene so any scene can be played on its own.
// Only the first one survives, the rest destroy themselves.
[RequireComponent(typeof(AudioSource))]
public class PersistentMusic : MonoBehaviour
{
    static PersistentMusic instance;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
