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
    private bool winning;

    // Declare and initialize a new List of GameObjects called currentCollisions.
    List<GameObject> currentSpecialItems = new List<GameObject>();
    List<GameObject> players = new List<GameObject>();

    void OnTriggerEnter(Collider col)
    {

        // Add the GameObject collided with to the list.
        if (col.gameObject.GetComponent<BasicItem>())
        {
            currentSpecialItems.Add(col.gameObject);
            Debug.Log("You found a special item");
        }

        if (col.gameObject.GetComponent<PlayerController>())
        {
            players.Add(col.gameObject);
        }

        // Print the entire list to the console.
        foreach (GameObject gObject in currentSpecialItems)
        {
            print(gObject.name);
        }
    }

    void CheckForWin()
    {
        if (currentSpecialItems.Count == 2 && PlayersPresent())
        {
            Debug.Log("YOU WIN!");
        }

        if(win)
        {
            if (!winning)
            {
                winning = true;
                StartCoroutine(GameObject.Find("Background").GetComponent<EndGameAnimatorScript>().Animate());
                StartCoroutine(GameObject.Find("DemonForeground").GetComponent<EndGameAnimatorScript>().Animate());
                StartCoroutine(GameObject.Find("RocksForeground").GetComponent<EndGameAnimatorScript>().Animate());
            }

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
