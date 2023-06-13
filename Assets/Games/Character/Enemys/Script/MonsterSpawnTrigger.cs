using UnityEngine;
using System.Collections;

public class MonsterSpawnTrigger : MonoBehaviour
{
    public GameObject monsterPrefab; // Reference to the monster prefab
    public GameObject[] spawnPoints; // Array of spawn point game objects
    public int numberOfMonsters = 5; // Number of monsters to spawn
    public float spawnDelay = 0.5f; // Delay between each enemy spawn

    private bool hasSpawned = false; // Flag to track if the wave has already been spawned

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            hasSpawned = true;
            StartCoroutine(SpawnWaveOfMonsters(other.transform));
        }
    }

    private IEnumerator SpawnWaveOfMonsters(Transform playerTransform)
    {
        int numSpawnPoints = spawnPoints.Length;

        for (int i = 0; i < numberOfMonsters; i++)
        {
            GameObject spawnPoint = spawnPoints[i % numSpawnPoints]; // Cycle through the spawn points

            // Instantiate the monster prefab at the spawn point
            GameObject monster = Instantiate(monsterPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            // Get the enemy movement script attached to the instantiated monster
            EnemyMovement enemyMovement = monster.GetComponent<EnemyMovement>();

            // Pass the player's transform to the enemy movement script
            if (enemyMovement != null)
            {
                enemyMovement.SetTarget(playerTransform);
            }

            yield return new WaitForSeconds(spawnDelay); // Wait for the specified delay before spawning the next enemy
        }
    }
}



