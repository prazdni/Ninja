using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BodyPart targetBodyPart;
    public float moveSpeed;

    [SerializeField] EnemyBodyInteractionManager _bodyInteractionManager;
    private EnemySpawner enemySpawner;
    private Transform initialSpawnPoint;

    public EnemyState enemyState;
    public enum EnemyState
    {
        StandingStill,
        GoingToBodyPart,
        GoingAway,
        GoneAway
    }

    private void Update()
    {
        if (targetBodyPart != null)
        {
            if (enemyState == EnemyState.GoingToBodyPart && !targetBodyPart.IsEnemyInteractive())
            {
                targetBodyPart = null;
                enemyState = EnemyState.StandingStill;
            }

            if (gameObject.transform.childCount > 0)
            {
                enemyState = EnemyState.GoingAway;
            }
        }

        if (enemyState == EnemyState.StandingStill)
        {
            FindTarget();
        }

        if (enemyState == EnemyState.GoingToBodyPart)
        {
            MoveToTarget(targetBodyPart);
        }
        else if (enemyState == EnemyState.GoingAway)
        {
            MoveToSpawn();
        }
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }

    public void SetInitialSpawnPoint(Transform spawnPoint)
    {
        initialSpawnPoint = spawnPoint;
    }

    public bool IsTargetBodyPartSet()
    {
        return targetBodyPart != null;
    }

    private void FindTarget()
    {
        foreach (BodyPart bodyPart in enemySpawner.bodyPartsManager.bodyParts)
        {
            if (bodyPart.IsEnemyInteractive())
            {
                targetBodyPart = bodyPart;
                enemyState = EnemyState.GoingToBodyPart;
                break;
            }
            else
            {
                enemyState = EnemyState.StandingStill;
            }
        }
    }

    private void MoveToTarget(BodyPart bodyPart)
    {
        transform.position = Vector2.MoveTowards(transform.position, bodyPart.gameObject.transform.position, moveSpeed * Time.deltaTime);
    }

    private void MoveToSpawn()
    {
        transform.position = Vector2.MoveTowards(transform.position, initialSpawnPoint.position, moveSpeed * Time.deltaTime);
        if ((transform.position - initialSpawnPoint.position).magnitude < 0.1f)
        {
            enemyState = EnemyState.GoneAway;
            _bodyInteractionManager.Unpick();
        }
    }
}
