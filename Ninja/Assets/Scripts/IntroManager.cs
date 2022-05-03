using System;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public Action OnEnded;

    public bool ended => !_animation.isPlaying && _skipped;

    [SerializeField] GameObject _canvas;
    [SerializeField] Animation _animation;
    bool _skipped;

    void Awake()
    {
        _animation.Play();
    }

    void FixedUpdate()
    {
        if (_skipped)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _skipped = true;
            _canvas.SetActive(false);
            OnEnded?.Invoke();
        }
    }
}
