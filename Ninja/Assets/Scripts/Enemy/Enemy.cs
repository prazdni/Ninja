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
        _bodyInteractionManager.Unpick(_targetBodyPart);
        _enemyState = EnemyState.StandingStill;
        _targetBodyPart = null;
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        _enemySpawner = spawner;
    }

    public void SetInitialSpawnPoint(Transform spawnPoint)
    {
        _initialSpawnPoint = spawnPoint;
    }

    void FindTarget()
    {
        foreach (BodyPart bodyPart in _enemySpawner.bodyPartsManager.bodyParts)
        {
            if (bodyPart.IsEnemyInteractive())
            {
                _targetBodyPart = bodyPart;
                _enemyState = EnemyState.GoingToBodyPart;
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
            bodyPart.RefreshState();
            _targetBodyPart = null;
        }
    }

    void MoveToSpawn()
    {
        transform.position = Vector2.MoveTowards(transform.position, _initialSpawnPoint.position, moveSpeed * Time.deltaTime);
        if ((transform.position - _initialSpawnPoint.position).magnitude < 0.1f)
        {
            _enemyState = EnemyState.GoneAway;
            _targetBodyPart.SetState(BodyPart.State.Lost);
            _bodyInteractionManager.Unpick(_targetBodyPart);
        }
    }
}
