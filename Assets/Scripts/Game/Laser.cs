using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] int damage = 25;
    [SerializeField] float onTime = 2f;
    [SerializeField] float offTime = 1.5f;
    [SerializeField] float offset = 0f;
    [SerializeField] GameObject beam;

    bool isOn = true;

    void Update()
    {
        float t = (Time.time + offset) % (onTime + offTime);
        isOn = t < onTime;
        beam.SetActive(isOn);
    }

    void OnTriggerStay(Collider other)
    {
        if (!isOn) return;
        if (other.TryGetComponent(out Health health))
            health.TakeDamage(damage);
    }
}
