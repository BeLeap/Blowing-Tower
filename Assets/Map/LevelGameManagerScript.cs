using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGameManagerScript : MonoBehaviour
{
    GameObject[] enemies;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");  
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log(enemies.Length);

        if(enemies.Length == 0)
        {
            Destroy(Player);
            SceneManager.LoadScene("LevelSelection", LoadSceneMode.Additive);
        }
    }
}
