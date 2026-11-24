using UnityEngine;
using UnityEngine.Audio;

// One player for every sound effect. Lives through scene loads.
public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer Instance { get; private set; }

    [SerializeField] AudioMixerGroup sfxGroup;
    [SerializeField] int voices = 4;
    [SerializeField] float pitchSemitones = 2f;

    AudioSource[] sources;
    int nextSource;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        sources = new AudioSource[voices];
        for (int i = 0; i < voices; i++)
        {
            var s = gameObject.AddComponent<AudioSource>();
            s.playOnAwake = false;
            s.outputAudioMixerGroup = sfxGroup;
            s.ignoreListenerPause = true; // so menu clicks still play while paused
            sources[i] = s;
        }
    }

    public void Play(AudioClip clip)
    {
        if (clip == null) return;

        // each voice gets its own pitch, so sounds that overlap don't share one
        AudioSource s = sources[nextSource];
        nextSource = (nextSource + 1) % sources.Length;

        float semis = Random.Range(-pitchSemitones, pitchSemitones);
        s.pitch = Mathf.Pow(2f, semis / 12f);
        s.PlayOneShot(clip);
    }
}
