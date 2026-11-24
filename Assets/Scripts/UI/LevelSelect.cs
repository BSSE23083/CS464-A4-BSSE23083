using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] LevelLoader loader;
    [SerializeField] Button buttonTemplate;
    [SerializeField] Transform grid;
    [SerializeField] Color lockedText = Color.white; // dark label is hard to read on the locked colour

    Button firstButton;

    void Awake()
    {
        buttonTemplate.gameObject.SetActive(false);

        for (int level = 1; level <= SaveData.LevelCount; level++)
        {
            Button b = Instantiate(buttonTemplate, grid);
            b.gameObject.SetActive(true);
            b.name = "Level " + level;

            bool open = level <= SaveData.Unlocked;
            b.interactable = open;

            string best = SaveData.HasBest(level) ? "Best " + SaveData.FormatTime(SaveData.Best(level)) : "No time yet";
            string text = open ? "Level " + level + "\n<size=70%>" + best + "</size>" : "Level " + level + "\n<size=70%>Locked</size>";
            var label = b.GetComponentInChildren<TMP_Text>();
            label.text = text;
            if (!open) label.color = lockedText;

            int index = level; // build index matches level number
            b.onClick.AddListener(() => loader.Load(index));

            if (level == 1) firstButton = b;
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
}
