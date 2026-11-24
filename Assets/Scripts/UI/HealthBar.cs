using UnityEngine;
using UnityEngine.UI;

// From lecture 08. Also listens to Health itself.
public class HealthBar : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Image fill;             // Filled, Horizontal, Origin Left
    [SerializeField] Image lag;              // same, behind it, paler
    [SerializeField] Gradient colour;
    [SerializeField] float lagDelay = 0.4f;
    [SerializeField] float lagSpeed = 0.8f;
    float target = 1f;
    float lagStartsAt;

    void OnEnable()
    {
        health.Damaged += Set;
        health.Healed += Set;
    }

    void OnDisable()
    {
        health.Damaged -= Set;
        health.Healed -= Set;
    }

    void Start() => Set(health.Current, health.Max);

    public void Set(int current, int max)
    {
        float next = Mathf.Clamp01((float)current / max);
        if (next < target) lagStartsAt = Time.unscaledTime + lagDelay;
        else lag.fillAmount = next;
        target = next;
        fill.fillAmount = target;
        fill.color = colour.Evaluate(target);
    }

    void Update()
    {
        if (Time.unscaledTime < lagStartsAt) return;
        lag.fillAmount = Mathf.MoveTowards(lag.fillAmount, target, lagSpeed * Time.unscaledDeltaTime);
    }
}
