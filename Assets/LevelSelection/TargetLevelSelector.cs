using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetLevelSelector : MonoBehaviour
{
    public int level;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(player);
        SceneManager.LoadScene("level_" + level.ToString(), LoadSceneMode.Additive);
    }
}
