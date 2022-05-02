using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Behaviour
{
    public AnimationClip clip;
    public Sprite sprite;
}

public class EnemyBehaviourController : MonoBehaviour
{
    [SerializeField] Behaviour[] _behaviours;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animation _animation;

    public void SetMovement()
    {
        int rand = Random.Range(0, _behaviours.Length);
        Behaviour behaviour = _behaviours[rand];
        _spriteRenderer.sprite = behaviour.sprite;
        _animation.clip = behaviour.clip;
        _animation.Play();
    }

    public void UnsetMovement()
    {
        _animation.Stop();
    }
}
