﻿using System;
using UnityEngine;

public class SurikenManager : MonoBehaviour
{
    [SerializeField] SurikenController _surikenController;
    [SerializeField] Transform _mainCharacter;
    [SerializeField] float _speed;
    [SerializeField] float _maxDistance;

    Camera _camera;
    Vector2 _endPointPosition;
    float _distance;
    bool _needStartThrowSuriken;
    bool _isSurikenToTarget;
    bool _isSurikenFromTarget;

    void Awake()
    {
        _camera = Camera.main;
        _surikenController.gameObject.SetActive(_isSurikenToTarget);
    }

    void Update()
    {
        if (!_isSurikenToTarget && !_isSurikenFromTarget)
            if (Input.GetMouseButtonDown(0))
                _needStartThrowSuriken = true;

        if (_needStartThrowSuriken)
        {
            _needStartThrowSuriken = false;
            _isSurikenToTarget = true;
            _surikenController.transform.position = _mainCharacter.position + _mainCharacter.forward;
            _surikenController.gameObject.SetActive(_isSurikenToTarget);
            _endPointPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (_endPointPosition - (Vector2) _mainCharacter.position).normalized * _maxDistance;
            _endPointPosition = (Vector2) _mainCharacter.position + direction;
            _distance = ((Vector2)_surikenController.transform.position - _endPointPosition).magnitude;
        }

        if (_isSurikenToTarget)
            MoveSuriken(_endPointPosition, 0.1f, ToTargetFormula, SwitchSurikenTargets);

        if (_isSurikenFromTarget)
            MoveSuriken(_mainCharacter.position, 1.0f, FromTargetFormula, SetSurikenUnabled);
    }

    void MoveSuriken(Vector2 endPointPosition, float delta, Func<float, float> formula, Action action)
    {
        float distance = formula((endPointPosition - (Vector2) _surikenController.transform.position).magnitude / _distance);
        _surikenController.transform.position = Vector2.MoveTowards(_surikenController.transform.position, endPointPosition, distance * _speed * Time.deltaTime);
        if (((Vector2)_surikenController.transform.position - endPointPosition).magnitude < delta)
            action.Invoke();
    }

    void SwitchSurikenTargets()
    {
        _isSurikenToTarget = false;
        _isSurikenFromTarget = true;
        _distance = (_surikenController.transform.position - _mainCharacter.position).magnitude;
    }

    void SetSurikenUnabled()
    {
        _isSurikenFromTarget = false;
        _surikenController.gameObject.SetActive(false);
    }

    float ToTargetFormula(float x)
    {
        return Mathf.Sqrt(x);
    }

    float FromTargetFormula(float x)
    {
        return 1 / x;
    }
}
