using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public enum BodyPartState
    {
        InsideGrave,
        OutsideGrave,
        Lost
    }

    public BodyPartState state;

    public void SetBodyPartState(string newState) 
    {
        switch (newState)
        {
            case "InsideGrave":
                state = BodyPartState.InsideGrave;
                break;
            case "OutsideGrave":
                state = BodyPartState.OutsideGrave;
                break;
            case "Lost":
                state = BodyPartState.Lost;
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Grave")
        {
            SetBodyPartState("InsideGrave");
        }
        else if (other.gameObject.tag == "OutOfBorder")
        {
            SetBodyPartState("Lost");
        }
        else 
        {
            SetBodyPartState("OutsideGrave");
        }
    }
}
