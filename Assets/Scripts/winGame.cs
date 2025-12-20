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
        // Auto-make the collider a trigger (nice when attaching to the portal)
        var col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWon) return;

        // React to PLAYER ROOT (works even if player has child colliders)
        var playerRoot = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;
        if (!playerRoot.CompareTag(playerTag)) return;

        hasWon = true;

        string levelId = SceneManager.GetActiveScene().name;

        // Save time if stopwatch exists
        var sw = Object.FindFirstObjectByType<stopwatchControl>();
        if (sw != null)
        {
            LevelScoreboard.AddScore(levelId, sw.GetElapsedTime());
        }

        // Tell Win Scene which level was just completed
        PlayerPrefs.SetString(LevelScoreboard.LastLevelKey, levelId);
        PlayerPrefs.Save();

        SceneManager.LoadSceneAsync(winSceneBuildIndex);
    }
}