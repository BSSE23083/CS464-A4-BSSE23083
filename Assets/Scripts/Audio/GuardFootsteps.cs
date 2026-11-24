using UnityEngine;

// Plays a footstep on the guard's own 3D AudioSource while he walks,
// so you can hear him coming before he walks round the corner.
[RequireComponent(typeof(AudioSource))]
public class GuardFootsteps : MonoBehaviour
{
    [SerializeField] AudioClip[] steps;
    [SerializeField] float stepTime = 0.5f;

    AudioSource source;
    Vector3 lastPos;
    float nextStep;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        lastPos = transform.position;
    }

    void Update()
    {
        if (Time.deltaTime <= 0f) return; // paused

        // speed instead of distance, so it works at any frame rate
        float speed = (transform.position - lastPos).magnitude / Time.deltaTime;
        lastPos = transform.position;
        if (speed < 0.5f || Time.time < nextStep) return;

        nextStep = Time.time + stepTime;
        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(steps[Random.Range(0, steps.Length)]);
    }
}
