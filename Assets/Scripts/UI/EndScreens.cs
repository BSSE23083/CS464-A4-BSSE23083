using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndScreens : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] LevelLoader loader;

    [Header("Game over")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameOverFirst;
    [SerializeField] AudioClip loseClip;

    [Header("Win")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject winFirst;
    [SerializeField] TMP_Text winTime;
    [SerializeField] GameObject newBestLabel;
    [SerializeField] AudioClip winClip;

    public bool IsOpen { get; private set; }

    void OnEnable() => health.OutOfLives += ShowGameOver;
    void OnDisable() => health.OutOfLives -= ShowGameOver;

    void ShowGameOver()
    {
        Open(gameOverPanel, gameOverFirst);
        SfxPlayer.Instance.Play(loseClip);
    }

    public void ShowWin(float time, bool best)
    {
        winTime.text = "Time " + SaveData.FormatTime(time);
        newBestLabel.SetActive(best);
        Open(winPanel, winFirst);
        SfxPlayer.Instance.Play(winClip);
    }

    void Open(GameObject panel, GameObject first)
    {
        IsOpen = true;
        panel.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(first);
    }

    // buttons
    public void Restart() => loader.Reload();
    public void NextLevel() => loader.LoadNext();
    public void MainMenu() => loader.LoadMenu();
}
