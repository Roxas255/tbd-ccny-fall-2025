using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class WinMenuController : MonoBehaviour
{
    
    private List<string> levelOrder = new List<string>()
    {
        "SampleScene", // Level 1
        "Level 2",     
        "Level 3"      
    };

    public void PlayAgain()
    {
        string lastLevel = PlayerPrefs.GetString(LevelScoreboard.LastLevelKey, "");
        if (!string.IsNullOrEmpty(lastLevel))
        {
            SceneManager.LoadSceneAsync(lastLevel);
        }
    }

    public void NextLevel()
    {
        string lastLevel = PlayerPrefs.GetString(LevelScoreboard.LastLevelKey, "");
        if (string.IsNullOrEmpty(lastLevel)) return;

        int currentIndex = levelOrder.IndexOf(lastLevel);

        // If there's a next level it will load it
        if (currentIndex >= 0 && currentIndex + 1 < levelOrder.Count)
        {
            SceneManager.LoadSceneAsync(levelOrder[currentIndex + 1]);
        }
        else
        {
            // if there is no next level go back to menu
            SceneManager.LoadSceneAsync("Main Menu");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
