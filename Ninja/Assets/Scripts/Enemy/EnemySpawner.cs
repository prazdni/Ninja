using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public BodyPartsManager bodyPartsManager;
    public List<Enemy> enemies;
    public GameObject enemyPrefab;
    public int initialEmemiesCount;

    public List<Transform> spawnPoints;
    public List<float> difficultyRaiseTimes;

    int _currentDifficultyIndex;

    [SerializeField] float _durationBetweenSpawns;
    [SerializeField] EndGameManager _endGameManager;
    [SerializeField] int _enemiesToSpawnMin;
    [SerializeField] int _enemiesToSpawnMax;

    float _currentDuration;
    bool _timerEnded;
    int _lastSpawnPointIndex;

    int _enemiesToSpawn;

    private void Awake()
    {
        _currentDifficultyIndex = 0;

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
        TryRaiseDifficulty();

        if (_currentDuration < _durationBetweenSpawns)
            _currentDuration += Time.deltaTime;
        else
            _timerEnded = true;

        if (_timerEnded)
        {
            _enemiesToSpawn = Random.Range(_enemiesToSpawnMin, _enemiesToSpawnMax+1);

            for (int i = 0; i < _enemiesToSpawn; i++)
            {
                SpawnEnemy();
            }
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

        if (randomSpawnPointIndex == _lastSpawnPointIndex) 
        {
            if (randomSpawnPointIndex == spawnPoints.Count-1)
            {
                randomSpawnPointIndex--;
            }
            else
            {
                randomSpawnPointIndex++;
            }
        }

        if (availibleEnemyIndex != -1)
        {
            enemies[availibleEnemyIndex].gameObject.transform.position = spawnPoints[randomSpawnPointIndex].position;
            enemies[availibleEnemyIndex].gameObject.SetActive(true);
        }
        else
        {
            InstantiateEnemy(spawnPoints[randomSpawnPointIndex]);
        }

        _lastSpawnPointIndex = randomSpawnPointIndex;
    }

    public void KillEnemy(Enemy enemy)
    {
        enemy.Kill();
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

    private void TryRaiseDifficulty() 
    {
        if (_currentDifficultyIndex < difficultyRaiseTimes.Count) 
        {
            if (_endGameManager.currentDuration >= difficultyRaiseTimes[_currentDifficultyIndex])
            {
                _currentDifficultyIndex++;

                switch (_currentDifficultyIndex)
                {
                    case 1:
                        RaiseDifficulty(-0.5f, 0, 1);
                        break;
                    case 2:
                        RaiseDifficulty(-0.5f, 1, 1);
                        break;
                    case 3:
                        RaiseDifficulty(-0.5f, 1, 0);
                        break;
                }
            }
        }
    }

    private void RaiseDifficulty(float durationBetweenSpawnsAdd, int enemiesToSpawnMinAdd, int enemiesToSpawnMaxAdd) 
    {
        _durationBetweenSpawns += durationBetweenSpawnsAdd;
        _enemiesToSpawnMin += enemiesToSpawnMinAdd;
        _enemiesToSpawnMax += enemiesToSpawnMaxAdd;
    }
}
