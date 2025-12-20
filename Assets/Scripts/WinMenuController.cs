using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class WinMenuController : MonoBehaviour
{
    private List<string> levelOrder = new List<string>()
    {
        "Level 1",
        "Level 2",
        "Level 3"
    };

    private void ResetAndStartStopwatch()
    {
        var sw = Object.FindFirstObjectByType<stopwatchControl>();
        if (sw != null)
        {
            sw.resetStopwatch();
            sw.startStopwatch();
        }
    }

    public void PlayAgain()
    {
        string lastLevel = PlayerPrefs.GetString(LevelScoreboard.LastLevelKey, "");
        if (!string.IsNullOrEmpty(lastLevel))
        {
            ResetAndStartStopwatch();
            SceneManager.LoadSceneAsync(lastLevel);
        }
    }

    public void NextLevel()
    {
        string lastLevel = PlayerPrefs.GetString(LevelScoreboard.LastLevelKey, "");
        if (string.IsNullOrEmpty(lastLevel)) return;

        int currentIndex = levelOrder.IndexOf(lastLevel);

        if (currentIndex >= 0 && currentIndex + 1 < levelOrder.Count)
        {
            ResetAndStartStopwatch();
            SceneManager.LoadSceneAsync(levelOrder[currentIndex + 1]);
        }
        else
        {
            ResetAndStartStopwatch();
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }

    public void MainMenu()
    {
        ResetAndStartStopwatch();
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
