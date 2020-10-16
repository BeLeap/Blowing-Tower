using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource enemySource;
    public AudioSource woodSource;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playEnemySource()
    {
        enemySource.PlayOneShot(enemySource.clip);
    }
    public void playWoodSource()
    {
        woodSource.PlayOneShot(woodSource.clip);
    }
}
