using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class LevelGameManagerScript : MonoBehaviour
{
    public int maxEnemyCount;
    GameObject[] enemies, stones;
    public Text enemyCount;
    public Text gameoverText;

    public float gameoverTime = 2f;

    private IEnumerator GameOverCounter()
    {
        float processingTime = 0.0f;
        while(processingTime < gameoverTime)
        {
            processingTime += Time.deltaTime;

            yield return null;
        }

        SceneManager.LoadScene("LevelSelection", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        stones = GameObject.FindGameObjectsWithTag("Stone");

        string enemyCountString = "EnemyCount " + enemies.Length.ToString() + "/" + maxEnemyCount.ToString();
        enemyCount.text = enemyCountString;

        if (enemies.Length <= 0 || stones.Length <= 0)
        {
            if(enemies.Length <= 0)
            {
                gameoverText.text = "Level Complete!";
            }
            else
            {
                gameoverText.text = "Game Over!";
            }

            StartCoroutine("GameOverCounter");
        }
    }
}
