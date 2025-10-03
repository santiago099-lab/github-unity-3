using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 5;
    public Transform waypoint1;
    public Transform waypoint2;

    private List<GameObject> Pool;
    void Start()
    {
        Pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            Pool.Add(enemy);
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
