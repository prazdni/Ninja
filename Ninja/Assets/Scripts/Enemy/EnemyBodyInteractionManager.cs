using UnityEngine;

public class EnemyBodyInteractionManager : MonoBehaviour, IBodyPartPicker
{
    [SerializeField] Transform _parent;
    BodyPart _bodyPart;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_bodyPart == null)
        {
            if (other.CompareTag("BodyPart"))
            {
                var bodyPart = other.GetComponent<BodyPart>();
                if (bodyPart.IsEnemyInteractive())
                    Pick(bodyPart);
            }
        }
    }

    public void Unpick()
    {
        if (_bodyPart != null)
            Unpick(_bodyPart);
    }

    public void Pick(BodyPart bodyPart)
    {
        bodyPart.transform.SetParent(_parent);
        bodyPart.SetLocker(this);
        _bodyPart = bodyPart;
    }

    public void Unpick(BodyPart bodyPart)
    {
        bodyPart.SetState(BodyPart.State.Lost);
        bodyPart.SetLocker(null);
        bodyPart.transform.SetParent(null);
    }
}
