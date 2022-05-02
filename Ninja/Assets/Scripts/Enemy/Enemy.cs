using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        StandingStill,
        GoingToBodyPart,
        GoingAway,
        GoneAway
    }

    public float moveSpeed;

    [SerializeField] EnemyBodyInteractionManager _bodyInteractionManager;
    [SerializeField] EnemyMovementController _enemyMovementController;
    EnemySpawner _enemySpawner;
    Transform _initialSpawnPoint;
    BodyPart _targetBodyPart;
    EnemyState _enemyState;

    void Update()
    {
        if (_targetBodyPart != null)
        {
            if (_enemyState == EnemyState.GoingToBodyPart)
            {
                MoveToTarget(_targetBodyPart);
            }

            if (_enemyState == EnemyState.GoingAway)
            {
                MoveToSpawn();
            }
        }
        else
        {
            if (_enemyState == EnemyState.StandingStill)
            {
                FindTarget();
            }

            if (_enemyState == EnemyState.GoneAway)
            {
                _enemySpawner.KillEnemy(this);
            }
        }
    }

    public void Kill()
    {
        _enemyMovementController.UnsetMovement();
        _bodyInteractionManager.Unpick(_targetBodyPart);
        _enemyState = EnemyState.StandingStill;
        _targetBodyPart = null;
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        _enemySpawner = spawner;
        _bodyInteractionManager.SetEnemySpawner(_enemySpawner);
    }

    public void SetInitialSpawnPoint(Transform spawnPoint)
    {
        _initialSpawnPoint = spawnPoint;
    }

    public void SpawnEnemy()
    {
        _enemyMovementController.SetMovement();
    }

    void SetTargetBodyPart(BodyPart bodyPart)
    {
        _targetBodyPart = bodyPart;
        _enemyState = _targetBodyPart == null ? EnemyState.StandingStill : EnemyState.GoingToBodyPart;
    }

    void FindTarget()
    {
        List<BodyPart> shuffledBodyParts = _enemySpawner.bodyPartsManager.bodyParts;
        Shuffle(shuffledBodyParts);
        foreach (BodyPart bodyPart in shuffledBodyParts)
        {
            if (bodyPart.IsEnemyInteractive())
            {
                SetTargetBodyPart(bodyPart);
                break;
            }
        }
    }

    void MoveToTarget(BodyPart bodyPart)
    {
        if (bodyPart.IsEnemyInteractive())
        {
            transform.position = Vector2.MoveTowards(transform.position, bodyPart.gameObject.transform.position, moveSpeed * Time.deltaTime);
            if ((_targetBodyPart.transform.position - transform.position).magnitude < 0.1f)
            {
                _bodyInteractionManager.Pick(_targetBodyPart);
                _enemyState = EnemyState.GoingAway;
            }
        }
        else
        {
            SetTargetBodyPart(null);
        }
    }

    void MoveToSpawn()
    {
        transform.position = Vector2.MoveTowards(transform.position, _initialSpawnPoint.position, moveSpeed * Time.deltaTime);
        if ((transform.position - _initialSpawnPoint.position).magnitude < 0.1f)
        {
            _enemyState = EnemyState.GoneAway;
            _bodyInteractionManager.Unpick(_targetBodyPart);
        }
    }

    public void Shuffle<T>(List<T> list)
    {
        var rand = new System.Random();
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rand.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}
