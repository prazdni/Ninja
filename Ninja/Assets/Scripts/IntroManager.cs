using System;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public Action OnEnded;

    public bool ended => _skipped;

    [SerializeField] GameObject _canvas;
    bool _skipped;

    void Update()
    {
        if (_skipped)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            _skipped = true;
            _canvas.SetActive(false);
            OnEnded?.Invoke();
        }
    }
}
