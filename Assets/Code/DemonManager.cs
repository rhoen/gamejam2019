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

    public void CreateEnemyTowardPlayer(int targetPlayerId) {
        GameObject demon = SpawnDemon(PickRandomEnemy(), Vector3.zero);
        demon.GetComponent<DemonController>().setTargetPlayerId(targetPlayerId);
        activeEnemies.Add(demon.GetComponent<DemonController>());
    }

    public void setEnemiesTowardPlayer(int targetPlayerId) {
        foreach(DemonController demon in activeEnemies) {
            demon.setTargetPlayerId(targetPlayerId);
        }
    }
    GameObject PickRandomEnemy() {
        return enemies[Random.Range(0,enemies.Length)];
    }

    GameObject SpawnDemon(GameObject demon, Vector3 spawnPosition)
    {
        return Instantiate(demon, spawnPosition, Quaternion.identity);
    }
}
