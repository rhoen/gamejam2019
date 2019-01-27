using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonManager : MonoBehaviour
{

    public GameObject[] enemies; 

    public float SpawnInterval = 10;
    float mElapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        mElapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        mElapsedTime += Time.deltaTime;
        if(mElapsedTime > SpawnInterval)
        {
            SpawnDemon(PickRandomEnemy(), Vector3.zero);
            mElapsedTime = 0;
        }
    }

    GameObject PickRandomEnemy() {
        int randomNum = Random.Range(0, (int)(enemies.Length-.01f));
        int randIndex = (int)Mathf.Floor(randomNum);
        return enemies[randIndex];
    }

    void SpawnDemon(GameObject demon, Vector3 spawnPosition)
    {
        Instantiate(demon, spawnPosition, Quaternion.identity);
    }
}
