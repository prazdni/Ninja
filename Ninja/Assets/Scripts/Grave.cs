using UnityEngine;

public class Grave : MonoBehaviour
{
    [SerializeField] Transform _rightHandSpawnPoint;
    [SerializeField] Transform _leftHandSpawnPoint;
    [SerializeField] Transform _headSpawnPoint;
    [SerializeField] Transform _bodySpawnPoint;
    [SerializeField] Transform _leftLegSpawnPoint;
    [SerializeField] Transform _rightLegSpawnPoint;

    public void ReturnToGrave(BodyPart bodyPart)
    {
        switch (bodyPart.part)
        {
            case BodyPart.Part.LeftHand:
                bodyPart.transform.SetPositionAndRotation(_leftHandSpawnPoint.position, _leftHandSpawnPoint.rotation);
                break;
            case BodyPart.Part.RightHand:
                bodyPart.transform.SetPositionAndRotation(_rightHandSpawnPoint.position, _rightHandSpawnPoint.rotation);
                break;
            case BodyPart.Part.Head:
                bodyPart.transform.SetPositionAndRotation(_headSpawnPoint.position, _headSpawnPoint.rotation);
                break;
            case BodyPart.Part.Body:
                bodyPart.transform.SetPositionAndRotation(_bodySpawnPoint.position, _bodySpawnPoint.rotation);
                break;
            case BodyPart.Part.LeftLeg:
                bodyPart.transform.SetPositionAndRotation(_leftLegSpawnPoint.position, _leftLegSpawnPoint.rotation);
                break;
            case BodyPart.Part.RightLeg:
                bodyPart.transform.SetPositionAndRotation(_rightLegSpawnPoint.position, _rightLegSpawnPoint.rotation);
                break;
        }
    }
}
