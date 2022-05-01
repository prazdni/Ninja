using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool isInInteraction => _bodyInteractionManager.isInInteraction;

    [SerializeField] MovementManager _movementManager;
    [SerializeField] BodyInteractionManager _bodyInteractionManager;
}
