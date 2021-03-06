﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    public float collisionMagnitude = 2;
    public float collisionEventLimit = 3;
    public float collisionTime = 0.2f;
    SoundManager soundManager;
    public GameObject particle;

    string name;
    uint collisionEventCount;

    private IEnumerator Count()
    {
        float time = 0.0f;
        while(time < collisionTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        GameObject particleObject = Instantiate(particle);
        particleObject.transform.position = this.gameObject.transform.position;
        particleObject.transform.rotation = this.gameObject.transform.rotation;
        if (name.Contains("obstacle"))
            soundManager.playWoodSource();
        else if (name.Contains("Enemy"))
            soundManager.playEnemySource();
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        name = this.gameObject.name;
        collisionEventCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collisionEventCount);
        if (collision.relativeVelocity.magnitude > collisionMagnitude){
            Debug.Log("CollisionCount++");
            collisionEventCount++;

            if(collisionEventCount == collisionEventLimit)
            {
                StartCoroutine(Count());
            }   
        }
    }
}
