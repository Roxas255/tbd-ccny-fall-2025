using UnityEngine;

public class spawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    private GameObject currentPlayer;

    void Start()
    {
        Respawn();
    }

    public void Respawn()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            if (currentPlayer != null)
                Destroy(currentPlayer);

            currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Player prefab or spawn point not set!");
        }
    }
}
