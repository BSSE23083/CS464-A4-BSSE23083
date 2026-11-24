using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip clickClip;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => SfxPlayer.Instance.Play(clickClip));
    }
}
