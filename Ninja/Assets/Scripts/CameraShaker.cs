using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] SurikenController _surikenManager;
    Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();

        _surikenManager.OnEnemyHit += OnEnemyHit;
    }

    void OnDestroy()
    {
        _surikenManager.OnEnemyHit -= OnEnemyHit;
    }

    void OnEnemyHit()
    {
        _camera.DOShakePosition(0.25f, 0.25f);
    }
}
