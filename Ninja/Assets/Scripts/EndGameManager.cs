using System;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    public Action OnTimerEnded;
    public Action OnAllBodyPartsLost;
    public bool isTimerEnded => _timerEnded;

    [SerializeField] PauseManager _pauseManager;
    [SerializeField] BodyPartsManager _bodyPartsManager;
    [SerializeField] Image _image;
    [SerializeField] float _duration;

    float _currentDuration;
    bool _timerEnded;
    bool _timerActionInvoked;

    bool _allBodyPartsLost;
    bool _allBodyPartsLostActionInvoked;

    void Update()
    {
        if (_pauseManager.isPause)
            return;

        if (_currentDuration < _duration)
        {
            _currentDuration += Time.deltaTime;
            _image.fillAmount = _currentDuration / _duration;
        }
        else
        {
            _timerEnded = true;

        }

        if (_timerEnded && !_timerActionInvoked)
        {
            OnTimerEnded?.Invoke();
            _timerActionInvoked = true;
        }

        if (!_allBodyPartsLost)
        {
            int lostParts = 0;
            foreach (BodyPart bodyPart in _bodyPartsManager.bodyParts)
            {
                if (bodyPart.state == BodyPart.State.Lost)
                    lostParts++;
            }

            if (_bodyPartsManager.bodyParts.Count == lostParts)
            {
                _allBodyPartsLost = true;
            }
            else
            {
                _allBodyPartsLost = false;
            }
        }

        if (_allBodyPartsLost && !_allBodyPartsLostActionInvoked)
        {
            OnAllBodyPartsLost?.Invoke();
            _allBodyPartsLostActionInvoked = true;
        }
    }
}
