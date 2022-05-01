using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public enum State
    {
        InsideGrave,
        OutsideGrave,
        Lost
    }

    public State state;

    IBodyPartPicker _locker;

    public void SetLocker(IBodyPartPicker locker)
    {
        _locker = locker;
    }

    public bool IsPlayerInteractive()
    {
        return state == State.OutsideGrave && _locker == null;
    }

    public bool IsEnemyInteractive()
    {
        return state != State.Lost && _locker == null;
    }

    public void SetState(State newState)
    {
        if (state == newState)
            return;

        switch (newState)
        {
            case State.InsideGrave:
                state = State.InsideGrave;
                break;
            case State.OutsideGrave:
                state = State.OutsideGrave;
                break;
            case State.Lost:
                state = State.Lost;
                break;
        }
    }
}
