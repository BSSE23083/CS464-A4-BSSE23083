using System;
using UnityEngine;

// Player health + lives. Other scripts listen to the events instead of
// Health calling them directly.
public class Health : MonoBehaviour
{
    [SerializeField] int max = 100;
    [SerializeField] int lives = 3;
    [SerializeField] float safeTime = 1f; // can't be hit again for this long

    public event Action<int, int> Damaged;   // current, max
    public event Action<int, int> Healed;    // current, max
    public event Action<int> LivesChanged;   // lives left
    public event Action OutOfLives;

    public int Current { get; private set; }
    public int Max => max;
    public int Lives => lives;
    public bool IsDead => lives <= 0;

    Vector3 spawnPoint;
    float safeUntil;

    void Awake()
    {
        Current = max;
        spawnPoint = transform.position;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead || Time.time < safeUntil) return;

        safeUntil = Time.time + safeTime;
        Current = Mathf.Max(Current - amount, 0);
        Damaged?.Invoke(Current, max);

        if (Current == 0) LoseLife();
    }

    void LoseLife()
    {
        lives--;
        LivesChanged?.Invoke(lives);

        if (lives <= 0)
        {
            OutOfLives?.Invoke();
            return;
        }

        // back to the start with full health
        var cc = GetComponent<CharacterController>();
        cc.enabled = false;
        transform.position = spawnPoint;
        cc.enabled = true;

        Current = max;
        safeUntil = Time.time + safeTime * 2f;
        Healed?.Invoke(Current, max);
    }
}
