using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Stone : MonoBehaviour
{
    public float distanceToHang = 0.2f;
    public float maxDistance = 5.0f;
    public float force = 1.0f;

    private Interactable interactable;
    private GameManager gameManager;

    private Rigidbody rb;

    public GameObject linePrefab;
    private List<GameObject> lines;

    private bool isHang;
    private bool isGrabbed;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.Instance;
        lines = new List<GameObject>();
        isHang = false;
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
        isHang = true;
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
        isHang = false;
    }

    void ManageLine(GameObject slingshot)
    {
        if (slingshot != null)
        {
            if (isGrabbed == true)
            {
                GameObject socket = slingshot.transform.GetChild(0).gameObject;
                GameObject left = slingshot.transform.GetChild(1).gameObject;
                GameObject right = slingshot.transform.GetChild(2).gameObject;

                float distance = Vector3.Distance(this.transform.position, socket.transform.position);

                if (lines.Count >= 2)
                {
                    if (distance > maxDistance) DestroyLine();
                    else UpdateTwoLine(this.transform.position, left.transform.position, right.transform.position);
                }

                else if (distance < distanceToHang)
                {
                    DrawTwoLine(this.transform.position, left.transform.position, right.transform.position);
                }
            }

            else
            {
                DestroyLine();
            }
        }

        else
        {
            DestroyLine();
        }
    }


    private void Update()
    {
        GameObject slingshot = gameManager.slingshot;
        ManageLine(slingshot);
    }

    void Shoot(Vector3 direction, Hand hand)
    {
        hand.DetachObject(gameObject);
        hand.HoverUnlock(interactable);
        rb.AddForce(direction * force);
        DestroyLine();
        isHang = false;
        isGrabbed = false;
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
            isGrabbed = false;
            if (isHang == true)
            {
                GameObject slingshot = gameManager.slingshot;
                if (slingshot)
                {
                    GameObject socket = slingshot.transform.GetChild(0).gameObject;

                    Vector3 lineBetweenSocketAndStone = socket.transform.position - this.transform.position;

                    Shoot(lineBetweenSocketAndStone, hand);
                }
            }
            else
            {
                hand.DetachObject(gameObject);
                hand.HoverUnlock(interactable);
                transform.position = originalPositon;
                transform.rotation = originalRotation;
            }
        }
    }
}
