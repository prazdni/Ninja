using UnityEngine;

public class EnemyBodyInteractionManager : MonoBehaviour, IBodyPartPicker
{
    [SerializeField] Enemy _enemy;
    EnemySpawner _enemySpawner;

    public void SetEnemySpawner(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    public void Kill()
    {
        _enemySpawner.KillEnemy(_enemy);
    }

    public void Pick(BodyPart bodyPart)
    {
        bodyPart.transform.SetParent(transform);
        bodyPart.SetLocker(this);
    }

    public void Unpick(BodyPart bodyPart)
    {
        if (bodyPart != null)
        {
            bodyPart.SetLocker(null);
            bodyPart.transform.SetParent(null);
        }
    }
}
