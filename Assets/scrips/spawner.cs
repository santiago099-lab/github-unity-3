using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
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

    private List<GameObject> Pool = new List<GameObject>();

    void Start()
    {
       for (int i = 0; i < maxEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            Pool.Add(enemy);
        }
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            int activeEnemies = 0;
            foreach (var enemy in Pool)
            {
                if (enemy.activeInHierarchy)
                {
                    activeEnemies++;
                }
            }

            if (activeEnemies < maxEnemies)
            {
               yield return new WaitForSeconds(spawnDelay);
                SpawnEnemy();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
    void SpawnEnemy()
    {
        currentEnemy = GetEnemyFromPool();

        var enemyScript = currentEnemy.GetComponent<AIBasics>();
        if (enemyScript != null && waypoint1 != null && waypoint2 != null)
        {
            enemyScript.moveSpots = new Transform[2];
            enemyScript.moveSpots[0] = waypoint1;
            enemyScript.moveSpots[1] = waypoint2;
        }
    }
    public GameObject GetEnemyFromPool()
    {
        foreach (var enemy in Pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = transform.position; 

                var enemyScript = enemy.GetComponent<AIBasics>();
                if (enemyScript != null)
                {
                    enemyScript.moveSpots = new Transform[2];
                    enemyScript.moveSpots[0] = waypoint1;
                    enemyScript.moveSpots[1] = waypoint2;
                }
                enemy.SetActive(true);
                return enemy;
            }
        }
      return null;
    }

}
