using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIWinBoard : MonoBehaviour
{
    public TMP_Text scoreboardText;

    void Start()
    {
        string levelId = PlayerPrefs.GetString(LevelScoreboard.LastLevelKey, "Unknown Level");
        List<float> top5 = LevelScoreboard.GetTop5(levelId);

        scoreboardText.text = BuildText(levelId, top5);
    }

    string BuildText(string levelId, List<float> scores)
    {
        string s = $"Top 5 — {levelId}\n";

        if (scores.Count == 0) return s + "No records yet.";

        for (int i = 0; i < scores.Count; i++)
            s += $"{i + 1}. {Format(scores[i])}\n";

        return s.TrimEnd();
    }

    string Format(float t)
    {
        int min = Mathf.FloorToInt(t / 60f);
        int sec = Mathf.FloorToInt(t % 60f);
        int ms = Mathf.FloorToInt((t * 1000f) % 1000f);
        return $"{min:00}:{sec:00}:{ms:000}";
    }
}
