using System.Collections;
using UnityEngine;

public class Pawn_EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDelay = 3.0f;
    private int enemiesToSpawn = 5;
    private Vector2 spawnPosition;

    public void StartSpawning(Vector2 position)
    {
        spawnPosition = position;
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (enemyPrefab != null)
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                Debug.Log("Enemy spawned. Remaining: " + (enemiesToSpawn - i - 1));
            }
            else
            {
                Debug.LogError("enemyPrefab is null!");
            }
            yield return new WaitForSeconds(spawnDelay);
        }
        Debug.Log("Finished spawning enemies.");
    }
}
