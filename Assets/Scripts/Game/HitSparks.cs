using UnityEngine;

public class HitSparks : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] ParticleSystem sparks; // one burst, no loop

    void OnEnable() => health.Damaged += OnHit;
    void OnDisable() => health.Damaged -= OnHit;

    void OnHit(int current, int max)
    {
        sparks.Play();
    }
}
