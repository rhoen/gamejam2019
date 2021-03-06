using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { private set; get; }

    public bool CanRestart = false;
    public GameObject GameOverScreenPrefab;
    public GameObject WillowWinPrefab;
    public GameObject WhispWinPrefab;

    private Camera GameOverCamera;
    void Awake() {
        Instance = this;
    }

    void Start() {
        int playerId = Random.value < 0.5f ? 1 : 2;
        PlayerController player = GameObject.FindGameObjectWithTag("player" + playerId).GetComponent<PlayerController>();
        player.DropThenPickUpBook();
        GameOverCamera = GetComponentInChildren<Camera>();
        SelectRandomItems();
    }

    void SelectRandomItems() {
        BasicItem[] allBasicItems = FindObjectsOfType<BasicItem>();
        Shuffle(allBasicItems);
        for (int i = 0; i < 5; i++) {
            allBasicItems[i].specialItem = true;
        }
    }

    void Shuffle(MonoBehaviour[] objects)
	{
		// Loops through array
		for (int i = objects.Length-1; i > 0; i--)
		{
			// Randomize a number between 0 and i (so that the range decreases each time)
			int rnd = Random.Range(0,i);
			
			// Save the value of the current i, otherwise it'll overright when we swap the values
			MonoBehaviour temp = objects[i];
			
			// Swap the new and old values
			objects[i] = objects[rnd];
			objects[rnd] = temp;
		}
	}

    public void Lose() {
        if (CanRestart) {
            return;
        }
        AudioManager.Instance.Lose();
        Instantiate(GameOverScreenPrefab, new Vector3(0, 0, 10), Quaternion.identity);
        GameOverCamera.enabled = true;
        CanRestart = true;
    }

    public void Win() {
        AudioManager.Instance.Win();
        Vector3 willowPosition = GameObject.FindGameObjectWithTag("player1").transform.position;
        Vector3 whispPosition = GameObject.FindGameObjectWithTag("player2").transform.position;
        GameObject.FindGameObjectWithTag("player1").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("player1").GetComponentInChildren<SpriteRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("player2").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("player2").GetComponentInChildren<SpriteRenderer>().enabled = false;
        BookController.Instance.GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(WillowWinPrefab, willowPosition, Quaternion.identity);
        Instantiate(WhispWinPrefab, whispPosition, Quaternion.identity);
        StartCoroutine(IgnoreInputTillRestartForSeconds(3));
    }

    IEnumerator IgnoreInputTillRestartForSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        CanRestart = true;
    }

    public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}