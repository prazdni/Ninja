using System;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public enum BodyPartState
    {
        InsideGrave,
        OutsideGrave,
        Lost
    }

    public Action<BodyPartState> OnStateChanged;

    public BodyPartState state;

    IBodyPartPicker _locker;

    public void SetLocker(IBodyPartPicker locker)
    {
        _locker = locker;
    }

    public bool IsPlayerInteractive()
    {
        return state == BodyPartState.OutsideGrave && _locker == null;
    }

    public bool IsEnemyInteractive()
    {
        return state != BodyPartState.Lost && _locker == null;
    }

    void SetBodyPartState(BodyPartState newState)
    {
        if (state == newState)
            return;

        switch (newState)
        {
            case BodyPartState.InsideGrave:
                state = BodyPartState.InsideGrave;
                break;
            case BodyPartState.OutsideGrave:
                state = BodyPartState.OutsideGrave;
                break;
            case BodyPartState.Lost:
                state = BodyPartState.Lost;
                break;
        }

        OnStateChanged?.Invoke(newState);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Grave"))
        {
            SetBodyPartState(BodyPartState.InsideGrave);
        }
        else if (other.gameObject.CompareTag("OutOfBorder"))
        {
            SetBodyPartState(BodyPartState.Lost);
        }
        else
        {
            SetBodyPartState(BodyPartState.OutsideGrave);
        }
    }
}
