using System.Collections.Generic;
using UnityEngine;

public class BodyPartsManager : MonoBehaviour
{
    [SerializeField] Grave _grave;

    public List<BodyPart> bodyParts;

    void Awake()
    {
        foreach (BodyPart bodyPart in bodyParts)
            _grave.ReturnToGrave(bodyPart);
    }
}
