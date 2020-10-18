using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBallInfinite : MonoBehaviour
{
    GameObject[] stones;

    private Vector3 stonePosition;
    public GameObject stonePrefab;
    public GameObject table;
    // Start is called before the first frame update
    void Start()
    {
        stones = GameObject.FindGameObjectsWithTag("Stone");
        stonePosition = stones[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        stones = GameObject.FindGameObjectsWithTag("Stone");
        Debug.Log(stones.Length.ToString());
        if (stones.Length <= 3)
        {
            GameObject newStone = GameObject.Instantiate(stonePrefab);
            newStone.tag = "Stone";
            newStone.transform.SetParent(table.transform);
            newStone.transform.position = stonePosition;
        }
    }
}
