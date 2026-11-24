using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Sits on the Canvas (not the pause panel) so Start runs as soon as the
// scene loads and the saved volume is applied straight away.
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;

    void Start()
    {
        slider.minValue = 0.0001f;
        slider.maxValue = 1f;
        slider.value = SaveData.MusicVolume;
        Apply(slider.value);
        slider.onValueChanged.AddListener(OnChanged);
    }

    void OnChanged(float value)
    {
        Apply(value);
        SaveData.MusicVolume = value;
    }

    void Apply(float value)
    {
        // 1 -> 0 dB, 0.5 -> -6 dB, 0.0001 -> -80 dB
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
    }
}
