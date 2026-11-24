using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject firstButton;
    [SerializeField] LevelLoader loader;
    [SerializeField] EndScreens endScreens;
    public static bool IsPaused { get; private set; }

    void Update()
    {
        if (endScreens.IsOpen) return;

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (IsPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        IsPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    public void Resume()
    {
        IsPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    // Resume first, otherwise the next scene starts frozen and silent
    public void Restart()
    {
        Resume();
        loader.Reload();
    }

    public void QuitToMenu()
    {
        Resume();
        loader.LoadMenu();
    }
}
