using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] LevelGoal goal;
    [SerializeField] AudioClip pickupClip;
    [SerializeField] GameObject pickupEffect;
    [SerializeField] float spinSpeed = 90f;

    Vector3 startPos;

    void Start() => startPos = transform.position;

    void Update()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0, Space.World);
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * 2f) * 0.15f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() == null) return;

        // sound goes through the SfxPlayer so it isn't cut off when this is destroyed
        SfxPlayer.Instance.Play(pickupClip);
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
        goal.Collect();
        Destroy(gameObject);
    }
}
