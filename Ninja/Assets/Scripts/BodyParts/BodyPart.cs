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

    public bool IsPlayerInteractive()
    {
        return state == State.OutsideGrave && _locker == null;
    }

    public bool IsEnemyInteractive()
    {
        return state != State.Lost && _locker == null;
    }

    void RefreshState()
    {
        var contacts = new Collider2D[10];
        Physics2D.GetContacts(_collider2D, contacts);
        bool isBodyPartInGrave = false;
        bool isBodyPartLost = false;
        for (int i = 0; i < contacts.Length; i++)
        {
            Collider2D contact = contacts[i];
            if (contact != null)
            {
                if (contact.CompareTag("Grave"))
                    isBodyPartInGrave = true;

                if (contact.CompareTag("Lost"))
                    isBodyPartLost = true;
            }
        }

        if (isBodyPartInGrave)
            SetState(State.InsideGrave);
        else if (isBodyPartLost)
            SetState(State.Lost);
        else
            SetState(State.OutsideGrave);
    }

    void SetState(State newState)
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
