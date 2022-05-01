using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Action OnTimerEnded;

    public bool isTimerEnded => _timerEnded;

    [SerializeField] PauseManager _pauseManager;
    [SerializeField] Image _image;
    [SerializeField] float _duration;

    float _currentDuration;
    bool _timerEnded;
    bool _invoked;

    void Update()
    {
        if (_pauseManager.isPause)
            return;

        if (_currentDuration < _duration)
            _currentDuration += Time.deltaTime;
        else
            _timerEnded = true;

        if (_timerEnded && !_invoked)
        {
            OnTimerEnded?.Invoke();
            _invoked = true;
        }
    }
}
