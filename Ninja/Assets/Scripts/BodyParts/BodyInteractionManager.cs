using UnityEngine;

public class BodyInteractionManager : MonoBehaviour, IBodyPartPicker
{
    public bool isInInteraction => _bodyPart != null;

    [SerializeField] Grave _grave;
    [SerializeField] Transform _parent;
    BodyPart _bodyPart;

    void Update()
    {
        if (_bodyPart == null)
            return;

        if ((transform.position - _grave.transform.position).magnitude < 1.5f)
        {
            _grave.ReturnToGrave(_bodyPart);
            Unpick(_bodyPart);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
