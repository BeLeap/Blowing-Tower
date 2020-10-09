using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Stone : MonoBehaviour
{
    private Interactable interactable;
    private GameManager gameManager;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        Debug.Log("Hi");
        GameObject slingShot = gameManager.slingShot;

        if (slingShot)
        {
            GameObject socket = slingShot.transform.GetChild(1).gameObject;
            float distance = Vector3.Distance(socket.transform.position, gameObject.transform.position);
            if (distance < 10.0f)
            {
                Debug.DrawLine(gameObject.transform.position, socket.transform.position, Color.red);
            }
        }
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
