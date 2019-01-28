using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtYardScript : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
        CheckForWin();
    }

    public bool win;
    public AudioManager audioManager;
    private bool winning;

    // Declare and initialize a new List of GameObjects called currentCollisions.
    List<GameObject> currentSpecialItems = new List<GameObject>();
    List<GameObject> players = new List<GameObject>();

    void OnTriggerEnter(Collider col)
    {

        // Add the GameObject collided with to the list.
        if (col.gameObject.GetComponent<BasicItem>())
        {
            if (currentSpecialItems.Contains(col.gameObject)) { return; }
            if (!col.gameObject.GetComponent<BasicItem>().specialItem) { return; }
            currentSpecialItems.Add(col.gameObject);
            col.gameObject.GetComponent<BasicItem>().enabled = false;
            Debug.Log("You found a special item");
            DemonManager.Instance.SpawnEnemy();
        }

        if (col.gameObject.GetComponent<PlayerController>())
        {
            players.Add(col.gameObject);
        }
    }

    void CheckForWin()
    {
        if ((currentSpecialItems.Count == 5 && PlayersPresent()) || win)
        {
            if (!winning)
            {
                audioManager.Win();
                winning = true;
                StartCoroutine(GameObject.Find("Background").GetComponent<EndGameAnimatorScript>().Animate());
                StartCoroutine(GameObject.Find("DemonForeground").GetComponent<EndGameAnimatorScript>().Animate());
                StartCoroutine(GameObject.Find("RocksForeground").GetComponent<EndGameAnimatorScript>().Animate());
            }
        }

        if(currentSpecialItems.Count == 1)
        {
            audioManager.FirstItem();
        }

        if (currentSpecialItems.Count == 3)
        {
            audioManager.AlmostWin();
        }
    }

    private bool PlayersPresent()
    {
        bool notHolding = true;
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerController>().HoldingItem())
            {
                notHolding = false;
            }
        }
        return notHolding && players.Count == 2;
    }


    void OnTriggerExit(Collider col)
    {
        // Remove the GameObject collided with from the list.
        if (col.gameObject.GetComponent<BasicItem>() != null)
        {
            currentSpecialItems.Remove(col.gameObject);
            Debug.Log("you removed a special item");
        }

        if (col.gameObject.GetComponent<PlayerController>())
        {
            players.Remove(col.gameObject);
        }
    }

}
