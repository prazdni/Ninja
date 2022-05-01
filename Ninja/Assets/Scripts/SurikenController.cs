using System;
using UnityEngine;

public class SurikenController : MonoBehaviour
{
    public Action OnEnemyHit;

    [SerializeField] EnemySpawner _enemySpawner;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            _enemySpawner.KillEnemy(enemy);
            OnEnemyHit?.Invoke();
        }
    }
}
