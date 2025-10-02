using System;
using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDelay = 5f;
    public int maxEnemies = 1;

    [Header("Waypoints")]
    public Transform waypoint1;
    public Transform waypoint2;


    private GameObject currentEnemy;
    private int enemiesSpawned = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (enemiesSpawned < maxEnemies)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
            enemiesSpawned++;

            while (currentEnemy != null)
            {
                yield return null;
            }
        }
    }
    void SpawnEnemy()
    {
        currentEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        var enemyScript = currentEnemy.GetComponent<AIBasics>();
        if (enemyScript != null && waypoint1 != null && waypoint2 != null)
        {
            enemyScript.moveSpots = new Transform[2];
            enemyScript.moveSpots[0] = waypoint1;
            enemyScript.moveSpots[1] = waypoint2;
        }
    }
}
