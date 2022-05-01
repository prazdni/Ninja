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
            {
                bodyPart.SetLocker(this);
                bodyPart.transform.SetParent(_parent);
            }
        }
    }
}
