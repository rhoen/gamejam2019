using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonManager : MonoBehaviour
{

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer % 10 == 0)
        {

        }
    }

    void SpawnDemon(GameObject demon, Vector3 spawnPosition)
    {
        Instantiate(demon, spawnPosition, Quaternion.identity);
    }
}
