using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyInteractionManager : MonoBehaviour, IBodyPartPicker
{
    [SerializeField] Transform _parent;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BodyPart"))
        {
            var bodyPart = other.GetComponent<BodyPart>();
            if (bodyPart.IsEnemyInteractive())
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