using System;
using UnityEngine;

public class SurikenController : MonoBehaviour
{
    public Action OnEnemyHit;

    [SerializeField] PoofManager _poofManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyBodyInteractionManager>();
            _poofManager.AddPoof(enemy.transform.position);
            enemy.Kill();
            OnEnemyHit?.Invoke();
        }
    }
}
