using System.Collections;
using UnityEngine;

public class Pawn_EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private float timeSpawn = 3f;

    [SerializeField]
    private int maxEnemies = 10;
    private int currentEnemies = 0;

    void Start()
    {
        StartCoroutine(SwanEnemies());
    }

    private IEnumerator SwanEnemies()
    {
        while (currentEnemies < maxEnemies)
        {
            yield return new WaitForSeconds(timeSpawn);
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            currentEnemies++;
        }
    }

}
