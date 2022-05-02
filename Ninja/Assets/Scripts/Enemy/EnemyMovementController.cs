using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] AnimationClip[] _clips;
    [SerializeField] Animation _animation;

    public void SetMovement()
    {
        int randomClip = Random.Range(0, _clips.Length);
        _animation.clip = _clips[randomClip];
        _animation.Play();
    }

    public void UnsetMovement()
    {
        _animation.Stop();
    }
}
