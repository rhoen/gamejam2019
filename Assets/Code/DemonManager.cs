using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonManager : MonoBehaviour
{

    public static DemonManager Instance {private set; get;}

    private List<DemonController> activeEnemies = new List<DemonController>();

    void Awake() {
        Instance = this;
    }

    public GameObject[] enemies;

    public void SpawnEnemy() {
        GameObject demon = SpawnDemon(PickRandomEnemy(), Vector3.zero);
        activeEnemies.Add(demon.GetComponent<DemonController>());
    }

    GameObject PickRandomEnemy() {
        return enemies[Random.Range(0,enemies.Length)];
    }

    GameObject SpawnDemon(GameObject demon, Vector3 spawnPosition)
    {
        return Instantiate(demon, spawnPosition, Quaternion.identity);
    }
}
