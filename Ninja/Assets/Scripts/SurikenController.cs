using System;
using UnityEngine;

public class SurikenController : MonoBehaviour
{
    public Action OnEnemyHit;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            OnEnemyHit?.Invoke();
    }
}
