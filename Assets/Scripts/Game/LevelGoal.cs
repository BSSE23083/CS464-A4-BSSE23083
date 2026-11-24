using System;
using UnityEngine;

// The exit. It only opens once all the loot in the level is picked up.
public class LevelGoal : MonoBehaviour
{
    [SerializeField] int totalLoot = 3;
    [SerializeField] EndScreens endScreens;
    [SerializeField] Renderer pad;
    [SerializeField] Color lockedColour = Color.gray;
    [SerializeField] Color openColour = Color.white;

    public event Action<int, int> LootChanged; // got, total

    int got;
    bool done;

    public int Got => got;
    public int Total => totalLoot;
    bool IsOpen => got >= totalLoot;

    void Start()
    {
        pad.material.color = lockedColour;
        LootChanged?.Invoke(got, totalLoot);
    }

    public void Collect()
    {
        got++;
        LootChanged?.Invoke(got, totalLoot);
        if (IsOpen) pad.material.color = openColour;
    }

    void OnTriggerEnter(Collider other)
    {
        if (done || !IsOpen) return;
        if (other.GetComponent<Health>() == null) return;
        done = true;
        Won();
    }

    public void Won()
    {
        int level = gameObject.scene.buildIndex;
        float time = Time.timeSinceLevelLoad;
        bool best = SaveData.Record(level, time);
        SaveData.Unlock(level + 1);
        endScreens.ShowWin(time, best);
    }
}
