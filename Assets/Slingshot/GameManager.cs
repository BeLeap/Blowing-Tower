using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        SteamVR.Initialize();
    }

    public GameObject slingshot = null;
}
