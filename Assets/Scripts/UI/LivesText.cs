using TMPro;
using UnityEngine;

public class LivesText : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] TMP_Text label;

    void OnEnable() => health.LivesChanged += Show;
    void OnDisable() => health.LivesChanged -= Show;

    void Start() => Show(health.Lives);

    void Show(int lives)
    {
        label.SetText("Lives {0}", lives);
    }
}
