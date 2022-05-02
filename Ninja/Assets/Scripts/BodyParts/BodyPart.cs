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
        InsideGrave,
        OutsideGrave,
        Lost
    }

    public Part part => _part;
    public State state;

    [SerializeField] Part _part;
    Collider2D _collider2D;
    IBodyPartPicker _locker;
    Grave _grave;

    void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    public void SetLocker(IBodyPartPicker locker)
    {
        _locker = locker;

        if (_locker == null)
            RefreshState();
    }

    public void SetGrave(Grave grave)
    {
        _grave = grave;
    }

    public bool IsPlayerInteractive()
    {
        return state == State.OutsideGrave && _locker == null;
    }

    public bool IsEnemyInteractive()
    {
        return state != State.Lost && _locker == null;
    }

    public bool IsBodyPartInGrave()
    {
        return (transform.position - _grave.transform.position).magnitude < 1.5f;
    }

    void RefreshState()
    {
        var contacts = new Collider2D[10];
        Physics2D.GetContacts(_collider2D, contacts);
        bool isBodyPartInGrave = IsBodyPartInGrave();
        if (isBodyPartInGrave)
        {
            SetState(State.InsideGrave);
            return;
        }

        bool isBodyPartLost = false;
        for (int i = 0; i < contacts.Length; i++)
        {
            Collider2D contact = contacts[i];
            if (contact != null)
            {
                if (contact.CompareTag("Lost"))
                    isBodyPartLost = true;
            }
        }

        SetState(isBodyPartLost ? State.Lost : State.OutsideGrave);
    }

    void SetState(State newState)
    {
        switch (newState)
        {
            case State.InsideGrave:
                _grave.ReturnToGrave(this);
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
