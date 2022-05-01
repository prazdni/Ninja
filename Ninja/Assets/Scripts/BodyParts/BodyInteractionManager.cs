using UnityEngine;

public class BodyInteractionManager : MonoBehaviour, IBodyPartPicker
{
    public bool isInInteraction => _isInInteraction;

    [SerializeField] Transform _parent;
    bool _isInInteraction;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BodyPart"))
        {
            var bodyPart = other.GetComponent<BodyPart>();
            if (bodyPart.IsPlayerInteractive())
                bodyPart.SetLocker(this);
        }
    }

    public void Pick(BodyPart bodyPart)
    {
        bodyPart.transform.SetPositionAndRotation(_parent.position + _parent.up, _parent.rotation);
        bodyPart.transform.SetParent(_parent);
        _isInInteraction = true;
    }

    public void Unpick(BodyPart bodyPart)
    {
        bodyPart.transform.SetParent(null);
        _isInInteraction = false;
    }
}
