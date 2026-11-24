using UnityEngine;

// The only place that knows the PlayerPrefs keys. Every read has a default.
public static class SaveData
{
    public const int LevelCount = 5;

    const string UnlockedKey = "unlocked";
    const string MusicKey = "MusicVolume";
    static string BestKey(int level) => "best" + level;

    public static int Unlocked => PlayerPrefs.GetInt(UnlockedKey, 1);

    public static void Unlock(int level)
    {
        level = Mathf.Min(level, LevelCount);
        if (level <= Unlocked) return;
        PlayerPrefs.SetInt(UnlockedKey, level);
        PlayerPrefs.Save();
    }

    public static bool HasBest(int level) => PlayerPrefs.HasKey(BestKey(level));

    public static float Best(int level) => PlayerPrefs.GetFloat(BestKey(level), 0f);

    // returns true if this is a new best time
    public static bool Record(int level, float time)
    {
        if (HasBest(level) && time >= Best(level)) return false;
        PlayerPrefs.SetFloat(BestKey(level), time);
        PlayerPrefs.Save();
        return true;
    }

    public static float MusicVolume
    {
        get => PlayerPrefs.GetFloat(MusicKey, 1f);
        set { PlayerPrefs.SetFloat(MusicKey, value); PlayerPrefs.Save(); }
    }

    public static string FormatTime(float seconds)
    {
        int m = (int)(seconds / 60);
        float s = seconds % 60;
        return string.Format("{0}:{1:00.0}", m, s);
    }
}
