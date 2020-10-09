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
        gameManager.slingShot = this.gameObject;
    }

    private void OnDestroy()
    {
        gameManager.slingShot = null;
    }

    private void OnDetachFromhand(Hand hand)
    {
        Debug.Log("Detach!!");
        Destroy(gameObject);
    }
}
