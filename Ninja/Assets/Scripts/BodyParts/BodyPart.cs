using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public enum Part
    {
        LeftHand,
        RightHand,
        Head,
        Body,
        LeftLeg,
        RightLeg
    }

    public enum State
    {
        Reachable,
        Lost
    }

    public Part part => _part;
    public State state;

    [SerializeField] Part _part;
    IBodyPartPicker _locker;
    Grave _grave;

    public void SetLocker(IBodyPartPicker locker)
    {
        _locker = locker;
    }

    public void SetGrave(Grave grave)
    {
        _grave = grave;
    }

    public bool IsEnemyInteractive()
    {
        return state != State.Lost && _locker == null;
    }

    public void SetState(State newState)
    {
        state = newState;

        switch (newState)
        {
            case State.Reachable:
                _grave.ReturnToGrave(this);
                break;
        }
    }
}
