using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Sling : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.slingshot = this.gameObject;
    }

    private void OnDestroy()
    {
        gameManager.slingshot = null;
    }

    private void OnDetachFromhand(Hand hand)
    {
        Debug.Log("Detach!!");
        Destroy(gameObject);
    }
}
