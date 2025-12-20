using UnityEngine;
using UnityEngine.SceneManagement;

public class winGame : MonoBehaviour
{
    [Header("Win Scene")]
    [SerializeField] private int winSceneBuildIndex = 3;

    [Header("Trigger")]
    [SerializeField] private string playerTag = "Player";

    private bool hasWon = false;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWon) return;

        var playerRoot = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;
        if (!playerRoot.CompareTag(playerTag)) return;

        hasWon = true;

        string levelId = SceneManager.GetActiveScene().name;

        var sw = Object.FindFirstObjectByType<stopwatchControl>();
        if (sw != null)
        {
            LevelScoreboard.AddScore(levelId, sw.GetElapsedTime());
            sw.stopStopwatch(); // stop timer on win
        }

        PlayerPrefs.SetString(LevelScoreboard.LastLevelKey, levelId);
        PlayerPrefs.Save();

        SceneManager.LoadSceneAsync(winSceneBuildIndex);
    }
}