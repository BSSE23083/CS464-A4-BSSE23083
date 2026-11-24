using UnityEngine;

public class HitSound : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] AudioClip hitClip;

    void OnEnable() => health.Damaged += OnHit;
    void OnDisable() => health.Damaged -= OnHit;

    void OnHit(int current, int max)
    {
        SfxPlayer.Instance.Play(hitClip);
    }
}
