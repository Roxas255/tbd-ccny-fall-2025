using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class WinMenuController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject nextLevelButton; // drag your Next Level button here

    private List<string> levelOrder = new List<string>()
    {
        "Level 1",
        "Level 2",
        "Level 3"
    };

    void Start()
    {
        string lastLevel = PlayerPrefs.GetString(LevelScoreboard.LastLevelKey, "");

        // Hide next level button if you're on the last level
        if (nextLevelButton != null && lastLevel == "Level 3")
        {
            nextLevelButton.SetActive(false);
        }
    }

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
            // No next level so go to menu
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
