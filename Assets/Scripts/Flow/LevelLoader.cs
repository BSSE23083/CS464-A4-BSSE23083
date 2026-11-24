using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int menuIndex = 0;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Image bar;
    [SerializeField] float minShowTime = 0.6f; // so the bar is actually visible

    bool loading;

    public void LoadMenu() => Load(menuIndex);
    public void Reload() => Load(SceneManager.GetActiveScene().buildIndex);

    public void LoadNext()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next >= SceneManager.sceneCountInBuildSettings) next = menuIndex; // after Credits
        Load(next);
    }

    public void Load(int index)
    {
        if (loading) return;
        loading = true;
        Time.timeScale = 1f;
        StartCoroutine(LoadRoutine(index));
    }

    IEnumerator LoadRoutine(int index)
    {
        loadingPanel.SetActive(true);
        bar.fillAmount = 0f;
        float shownAt = Time.unscaledTime;

        AsyncOperation op = SceneManager.LoadSceneAsync(index);
        op.allowSceneActivation = false;

        // progress stops at 0.9 until we let the scene activate
        while (op.progress < 0.9f || Time.unscaledTime - shownAt < minShowTime)
        {
            float loaded = Mathf.Clamp01(op.progress / 0.9f);
            float shown = Mathf.Clamp01((Time.unscaledTime - shownAt) / minShowTime);
            bar.fillAmount = Mathf.Min(loaded, shown);
            yield return null;
        }

        bar.fillAmount = 1f;
        yield return null;
        op.allowSceneActivation = true;
    }
}
