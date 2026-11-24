using UnityEngine;
using UnityEngine.EventSystems;

// For screens that only have one panel (Credits), so the keyboard works right away.
public class SelectOnStart : MonoBehaviour
{
    [SerializeField] GameObject first;

    void Start() => EventSystem.current.SetSelectedGameObject(first);
}
