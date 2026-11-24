using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] CameraFollow cam;
    [SerializeField] float amount = 0.25f;
    [SerializeField] float time = 0.2f;

    void OnEnable() => health.Damaged += OnHit;
    void OnDisable() => health.Damaged -= OnHit;

    void OnHit(int current, int max)
    {
        cam.Shake(amount, time);
    }
}
