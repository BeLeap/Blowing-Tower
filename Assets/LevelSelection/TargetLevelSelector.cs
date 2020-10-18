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
        if (level == -1)
        {
            Application.Quit();
        }

        else
        {
            SceneManager.LoadScene("level_" + level.ToString(), LoadSceneMode.Single);
        }
    }
}
