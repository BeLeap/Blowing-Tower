using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Sling : MonoBehaviour
{
    private Hand hand;

    private void OnAttachToHand(Hand attachedHand)
    {
        hand = attachedHand;
    }

    private void OnDetachFromhand(Hand hand)
    {
        Debug.Log("Detach!!");
        Destroy(gameObject);
    }
}
