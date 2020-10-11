using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    public float collisionMagnitude = 2;
    public float collisionEventLimit = 3;
    public SoundManager soundManager;
    public GameObject particle;
    

    uint collisionEventCount;

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(particle);
        soundManager.playSound();
        Destroy(gameObject);
    }
   

    // Start is called before the first frame update
    void Start()
    {
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
                Instantiate(particle);
                soundManager.playSound();
                Destroy(gameObject);
                // StartCoroutine(Timer());
            }   
        }
    }
}
