using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapToggleScript : MonoBehaviour
{
    bool isMinimapEnable;
    GameObject Minimap;

    // Start is called before the first frame update
    void Start()
    {
        isMinimapEnable = true;
        Minimap = GameObject.Find("Minimap");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isMinimapEnable = !isMinimapEnable;
            Debug.Log("Key pressed");
            Minimap.SetActive(isMinimapEnable);
        }
    }
}
