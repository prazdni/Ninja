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

    [SerializeField] float _duration;
    float _currentDuration;
    bool _timerEnded;


    private void Awake()
    {
        for (int i = 0; i < initialEmemiesCount; i++)
        {
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
            InstantiateEnemy(spawnPoints[randomSpawnPointIndex]);
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_currentDuration < _duration)
            _currentDuration += Time.deltaTime;
        else
            _timerEnded = true;

        if (_timerEnded)
        {
            SpawnEnemy();
            _timerEnded = false;
            _currentDuration = 0f;
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
            InstantiateEnemy(spawnPoints[randomSpawnPointIndex]);
        }
    }

    public void KillEnemy(Enemy enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(false);
        enemy.transform.position = Vector3.zero;
    }

    public void InstantiateEnemy(Transform spawnPoint)
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        Enemy enemy = newEnemy.GetComponent<Enemy>();

        enemy.SetSpawner(this);
        enemy.SetInitialSpawnPoint(spawnPoint);

        newEnemy.GetComponent<Enemy>().SetSpawner(this);
        newEnemy.transform.position = spawnPoint.position;

        enemies.Add(newEnemy.GetComponent<Enemy>());
    }
}
