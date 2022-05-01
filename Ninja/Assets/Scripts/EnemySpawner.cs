using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public BodyPartsManager bodyPartsManager;
    public List<Enemy> enemies;
    public GameObject enemyPrefab;
    public int initialEmemiesCount;

    public List<Transform> spawnPoints;

    private void Awake()
    {
        for (int i = 0; i < initialEmemiesCount; i++)
        {
            InstantiateEnemy(Vector3.zero);
        }

        foreach (Enemy e in enemies)
            e.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnEnemy();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            int i = Random.Range(0, enemies.Count);
            if (enemies[i].gameObject.activeSelf)
                KillEnemy(enemies[i]);
        }
    }

    public void SpawnEnemy() 
    {
        int availibleEnemyIndex = -1;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].gameObject.activeSelf) 
            {
                availibleEnemyIndex = i;
            }
        }

        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);

        if (availibleEnemyIndex != -1)
        {
            
            enemies[availibleEnemyIndex].gameObject.transform.position = spawnPoints[randomSpawnPointIndex].position;
            enemies[availibleEnemyIndex].gameObject.SetActive(true);
        }
        else
        {
            InstantiateEnemy(spawnPoints[randomSpawnPointIndex].position);
        }
    }

    public void KillEnemy(Enemy enemy) 
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.position = Vector3.zero;
    }

    public void InstantiateEnemy(Vector3 instantiatePosition) 
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = instantiatePosition;
        enemies.Add(newEnemy.GetComponent<Enemy>());
    }
}
