using UnityEngine;

// A sound that has a place in the level (fully 3D).
// Loop is off for the guards, their footsteps are played one at a time.
[RequireComponent(typeof(AudioSource))]
public class WorldSound : MonoBehaviour
{
    [SerializeField] AudioRolloffMode rolloff = AudioRolloffMode.Logarithmic;
    [SerializeField] float minDistance = 1f;
    [SerializeField] float maxDistance = 15f;
    [SerializeField] bool loop = true;

    void Awake()
    {
        var source = GetComponent<AudioSource>();
        source.spatialBlend = 1f;
        source.rolloffMode = rolloff;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.loop = loop;
        if (loop && !source.isPlaying) source.Play();
    }
}
