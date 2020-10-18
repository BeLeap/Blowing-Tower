using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetLevelSelector : MonoBehaviour
{
    public int level;
    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("level_" + level.ToString(), LoadSceneMode.Single);
    }
}
