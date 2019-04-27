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

    void Start() {
        Shuffle(enemiesToSpawnRandomly);
    }

    public GameObject firstEnemyToSpawn;
    public GameObject[] enemiesToSpawnRandomly;

    public void SpawnEnemyAtPosition(Vector3 position) {
        if (activeEnemies.Count >= enemiesToSpawnRandomly.Length + 1) {
            return;
        }
        GameObject enemy = null;
        if (activeEnemies.Count == 0) {
            enemy = SpawnDemon(firstEnemyToSpawn, position);
        } else {
            enemy = SpawnDemon(enemiesToSpawnRandomly[activeEnemies.Count - 1], position);
        }
        activeEnemies.Add(enemy.GetComponent<DemonController>());
    }

    void Shuffle(GameObject[] objects)
	{
		// Loops through array
		for (int i = objects.Length-1; i > 0; i--)
		{
			// Randomize a number between 0 and i (so that the range decreases each time)
			int rnd = Random.Range(0,i);
			
			// Save the value of the current i, otherwise it'll overright when we swap the values
			GameObject temp = objects[i];
			
			// Swap the new and old values
			objects[i] = objects[rnd];
			objects[rnd] = temp;
		}
	}

    GameObject SpawnDemon(GameObject demon, Vector3 spawnPosition)
    {
        return Instantiate(demon, spawnPosition, Quaternion.identity);
    }
}
