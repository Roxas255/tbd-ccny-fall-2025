using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LevelScoreboard
{
    private static string Key(string levelId) => $"Top5_{levelId}";
    public static string LastLevelKey => "LastLevelCompleted";

    public static List<float> GetTop5(string levelId)
    {
        string raw = PlayerPrefs.GetString(Key(levelId), "");
        if (string.IsNullOrEmpty(raw)) return new List<float>();

        return raw.Split('|')
                  .Select(s => float.TryParse(s, out var v) ? v : float.PositiveInfinity)
                  .Where(v => !float.IsInfinity(v))
                  .OrderBy(v => v)
                  .Take(5)
                  .ToList();
    }

    public static void AddScore(string levelId, float timeSeconds)
    {
        var scores = GetTop5(levelId);
        scores.Add(timeSeconds);
        scores = scores.OrderBy(v => v).Take(5).ToList();

        string raw = string.Join("|", scores.Select(v => v.ToString("F3")));
        PlayerPrefs.SetString(Key(levelId), raw);
        PlayerPrefs.Save();
    }
}
