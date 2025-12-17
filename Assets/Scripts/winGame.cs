using UnityEngine;
using UnityEngine.SceneManagement;

public class winGame : MonoBehaviour
{
    [SerializeField] private int winSceneBuildIndex = 3;
    private bool hasWon = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWon) return;

        // IMPORTANT: make sure we're reacting to the PLAYER ROOT object
        var playerRoot = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;
        if (!playerRoot.CompareTag("Player")) return;

        hasWon = true;

        string levelId = SceneManager.GetActiveScene().name;

        var sw = UnityEngine.Object.FindFirstObjectByType<stopwatchControl>();
        if (sw != null)
            LevelScoreboard.AddScore(levelId, sw.GetElapsedTime());

        PlayerPrefs.SetString(LevelScoreboard.LastLevelKey, levelId);
        PlayerPrefs.Save();

        SceneManager.LoadSceneAsync(winSceneBuildIndex);
    }
}
