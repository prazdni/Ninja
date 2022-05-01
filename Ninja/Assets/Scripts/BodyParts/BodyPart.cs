using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public enum BodyPartState
    {
        InsideGrave,
        OutsideGrave,
        Lost
    }

    public BodyPartState state;

    IBodyPartPicker _locker;

    public void SetLocker(IBodyPartPicker locker)
    {
        _locker = locker;
        _locker.Pick(this);
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Grave"))
        {
            _locker?.Unpick(this);
            SetBodyPartState(BodyPartState.InsideGrave);
        }
        else if (other.gameObject.CompareTag("OutOfBorder"))
        {
            _locker?.Unpick(this);
            SetBodyPartState(BodyPartState.Lost);
        }
        else
        {
            SetBodyPartState(BodyPartState.OutsideGrave);
        }
    }
}
