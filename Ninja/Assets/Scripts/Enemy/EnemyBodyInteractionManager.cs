using UnityEngine;

public class EnemyBodyInteractionManager : MonoBehaviour, IBodyPartPicker
{
    [SerializeField] Transform _parent;

    public void Pick(BodyPart bodyPart)
    {
        bodyPart.transform.SetParent(_parent);
        bodyPart.SetLocker(this);
    }

    public void Unpick(BodyPart bodyPart)
    {
        if (bodyPart != null)
        {
            bodyPart.SetLocker(null);
            bodyPart.transform.SetParent(null);
        }
    }
}
