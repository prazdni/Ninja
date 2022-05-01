using System;
using UnityEngine;

public class SurikenManager : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _suriken;
    [SerializeField] CharacterController _characterController;
    [SerializeField] float _speed;

    Vector2 _endPointPosition;
    float _distance;
    bool _needStartThrowSuriken;
    bool _isSurikenToTarget;
    bool _isSurikenFromTarget;

    void Awake()
    {
        _suriken.SetActive(_isSurikenToTarget);
    }

    void Update()
    {
        if (!_characterController.isInInteraction)
        {
            if (!_isSurikenToTarget && !_isSurikenFromTarget)
                if (Input.GetMouseButtonDown(0))
                    _needStartThrowSuriken = true;
        }

        if (_needStartThrowSuriken)
        {
            _needStartThrowSuriken = false;
            _isSurikenToTarget = true;
            _suriken.transform.position = _characterController.transform.position + _characterController.transform.forward;
            _suriken.SetActive(_isSurikenToTarget);
            _endPointPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _distance = ((Vector2)_suriken.transform.position - _endPointPosition).magnitude;
        }

        if (_isSurikenToTarget)
            MoveSuriken(_endPointPosition, 0.1f, ToTargetFormula, SwitchSurikenTargets);

        if (_isSurikenFromTarget)
            MoveSuriken(_characterController.transform.position, 1.0f, FromTargetFormula, SetSurikenUnabled);
    }

    void MoveSuriken(Vector2 endPointPosition, float delta, Func<float, float> formula, Action action)
    {
        float distance = formula((endPointPosition - (Vector2) _suriken.transform.position).magnitude / _distance);
        _suriken.transform.position = Vector2.MoveTowards(_suriken.transform.position, endPointPosition, distance * _speed * Time.deltaTime);
        if (((Vector2)_suriken.transform.position - endPointPosition).magnitude < delta)
            action.Invoke();
    }

    void SwitchSurikenTargets()
    {
        _isSurikenToTarget = false;
        _isSurikenFromTarget = true;
        _distance = (_suriken.transform.position - _characterController.transform.position).magnitude;
    }

    void SetSurikenUnabled()
    {
        _isSurikenFromTarget = false;
        _suriken.SetActive(false);
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
