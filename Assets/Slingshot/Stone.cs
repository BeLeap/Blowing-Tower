using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Stone : MonoBehaviour
{
    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private Vector3 originalPositon;
    private Quaternion originalRotation;

    private Hand.AttachmentFlags attachmentFlags =
        Hand.defaultAttachmentFlags
        & (~Hand.AttachmentFlags.SnapOnAttach)
        & (~Hand.AttachmentFlags.DetachOthers)
        & (~Hand.AttachmentFlags.VelocityMovement);

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            originalPositon = transform.position;
            originalRotation = transform.rotation;

            hand.HoverLock(interactable);
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
        else if(isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);

            transform.position = originalPositon;
            transform.rotation = originalRotation;
        }
    }
}
