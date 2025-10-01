using System;
using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [Header("Spawn Configuration")]
    public GameObject enemyPrefab;
    public float spawnDelay = 5f;
    public int maxEnemies = 5;
    public Transform spawnPoint;

    [Header("Patrol Points")]
    public Transform patrolPointB;
    public Transform patrolPointA;

    private GameObject currentEnemy;
    private int enemiesSpawned = 0;

    void Start()
    {
        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }
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
        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        EnemyAI enemyAI = currentEnemy.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.pointA = patrolPointA;
            enemyAI.pointB = patrolPointB;
        }
    }
}
