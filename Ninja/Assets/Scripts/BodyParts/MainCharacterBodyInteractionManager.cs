using UnityEngine;

public class MainCharacterBodyInteractionManager : MonoBehaviour, IBodyPartPicker
{
    [SerializeField] Transform _parent;

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
        bodyPart.transform.SetParent(_parent);
    }

    public void Unpick(BodyPart bodyPart)
    {
        bodyPart.transform.SetParent(null);
    }
}
