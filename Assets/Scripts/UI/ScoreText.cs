using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] LevelGoal goal;
    [SerializeField] TMP_Text lootLabel;
    [SerializeField] TMP_Text timeLabel;

    void OnEnable() => goal.LootChanged += Show;
    void OnDisable() => goal.LootChanged -= Show;

    void Start() => Show(goal.Got, goal.Total);

    void Show(int got, int total)
    {
        lootLabel.SetText("Loot {0} / {1}", got, total);
    }

    void Update()
    {
        timeLabel.text = SaveData.FormatTime(Time.timeSinceLevelLoad);
    }
}
