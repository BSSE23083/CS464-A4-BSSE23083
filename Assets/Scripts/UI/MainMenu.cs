using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] LevelLoader loader;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject mainFirst;
    [SerializeField] LevelSelect levelSelect;

    void Start() => ShowMain();

    public void ShowMain()
    {
        levelSelect.gameObject.SetActive(false);
        mainPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(mainFirst);
    }

    public void ShowLevelSelect()
    {
        mainPanel.SetActive(false);
        levelSelect.Open();
    }

    public void Play() => loader.LoadNext();

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
