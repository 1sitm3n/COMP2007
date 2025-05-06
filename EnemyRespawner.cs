using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [Header("Spawner Setup")]
    public GameObject enemyPrefab;
    // The point in the scene where the enemy should appear
    public Transform spawnPoint;
    public float respawnDelay = 3f;

    private GameObject currentEnemy;

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        // Instantiate the enemy at specified position and rotation. 
        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Assign Player to AI script.
        EnemyAI ai = currentEnemy.GetComponent<EnemyAI>();
        if (ai != null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                ai.player = playerObj.transform;
            }
        }

        // Subscribes to the enemy's death event so it can trigger a respawn. 
        EnemyStats stats = currentEnemy.GetComponent<EnemyStats>();
        if (stats != null)
        {
            stats.onDeath += OnEnemyDied;
        }
    }

    void OnEnemyDied()
    {
        Debug.Log("Enemy died. Respawning in " + respawnDelay + " seconds...");
        // Delay before respawning the next enemy
        Invoke(nameof(SpawnEnemy), respawnDelay);
    }
}
