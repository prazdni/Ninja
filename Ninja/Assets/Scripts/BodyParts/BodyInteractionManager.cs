using UnityEngine;

public class BodyInteractionManager : MonoBehaviour, IBodyPartPicker
{
    public bool isInInteraction => _bodyPart != null;

    [SerializeField] Transform _parent;
    BodyPart _bodyPart;

    void Update()
    {
        if (_bodyPart == null)
            return;

        if (_bodyPart.IsBodyPartInGrave())
        {
            Unpick(_bodyPart);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_bodyPart != null)
            return;

        if (other.CompareTag("BodyPart"))
        {
            var bodyPart = other.GetComponent<BodyPart>();
            if (bodyPart.IsPlayerInteractive())
                Pick(bodyPart);
        }
    }

    public void Pick(BodyPart bodyPart)
    {
        bodyPart.transform.SetPositionAndRotation(_parent.position + _parent.up, _parent.rotation);
        bodyPart.transform.SetParent(_parent);
        bodyPart.SetLocker(this);
        _bodyPart = bodyPart;
    }

    public void Unpick(BodyPart bodyPart)
    {
        bodyPart.transform.SetParent(null);
        bodyPart.SetLocker(null);
        _bodyPart = null;
    }
}
