using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Stone : MonoBehaviour
{
    public float distanceToHang = 0.2f;
    public float maxDistance = 5.0f;

    private Interactable interactable;
    private GameManager gameManager;

    private Rigidbody rb;

    public GameObject linePrefab;
    private List<GameObject> lines;

    private bool isReadyToShoot;
    private bool isGrabbed;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
        lines = new List<GameObject>();
        isReadyToShoot = false;
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject myLine = Instantiate(linePrefab, start, Quaternion.identity);
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lines.Add(myLine);
    }

    void DrawTwoLine(Vector3 start, Vector3 left, Vector3 right)
    {
        DrawLine(start, left);
        DrawLine(start, right);
    }

    void UpdateLine(GameObject line, Vector3 start, Vector3 end)
    {
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    void UpdateTwoLine(Vector3 start, Vector3 left, Vector3 right)
    {
        UpdateLine(lines[0], start, left);
        UpdateLine(lines[1], start, right);
    }

    void DestroyLine()
    {
        foreach (GameObject line in lines)
            Destroy(line);

        lines = new List<GameObject>();
    }


    private void Update()
    {
        GameObject slingShot = gameManager.slingShot;

        if (slingShot)
        {
            GameObject socket = slingShot.transform.GetChild(0).gameObject;
            GameObject left = slingShot.transform.GetChild(1).gameObject;
            GameObject right = slingShot.transform.GetChild(2).gameObject;

            float distance = Vector3.Distance(socket.transform.position, gameObject.transform.position);

            if (isGrabbed == true)
            {
                if (distance < distanceToHang)
                    isReadyToShoot = true;

                if (isReadyToShoot == true)
                {
                    if (lines.Count >= 2)
                        UpdateTwoLine(this.gameObject.transform.position, left.transform.position, right.transform.position);
                    else
                        DrawTwoLine(socket.transform.position, left.transform.position, right.transform.position);

                    if (distance > maxDistance)
                        Shoot();
                }
            }

            else
            {
                if (isReadyToShoot == true && distance > maxDistance)
                {
                    isReadyToShoot = false;
                    Shoot();
                }
            }
        }

        else
            DestroyLine();
    }

    void Shoot()
    {
        GameObject slingShot = gameManager.slingShot;

        if (slingShot)
        {
            GameObject socket = slingShot.transform.GetChild(0).gameObject;

            float distance = Vector3.Distance(socket.transform.position, gameObject.transform.position);

            Vector3 relativePos = socket.transform.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.forward);
            Vector3 force = rotation.eulerAngles;
            force = force * distance;
            rb.AddForce(force);

            DestroyLine();
        }

        isReadyToShoot = false;
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
            isGrabbed = true;
            originalPositon = transform.position;
            originalRotation = transform.rotation;

            hand.HoverLock(interactable);
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
        else if(isGrabEnding)
        {
            if (isGrabbed == true)
                Shoot();
            isGrabbed = false;
        }
    }
}
