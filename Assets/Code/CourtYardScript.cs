using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtYardScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    // Declare and initialize a new List of GameObjects called currentCollisions.
    List<GameObject> currentSpecialItems = new List<GameObject>();

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("basic item?" + col.gameObject.GetComponent<BasicItem>());

        // Add the GameObject collided with to the list.
        if (col.gameObject.GetComponent<BasicItem>())
        {
            currentSpecialItems.Add(col.gameObject);
            Debug.Log("You found a special item");
            CheckForWin();
        }

        // Print the entire list to the console.
        foreach (GameObject gObject in currentSpecialItems)
        {
            print(gObject.name);
        }
    }

    void CheckForWin()
    {
        if (currentSpecialItems.Count == 2)
        {
            Debug.Log("YOU WIN!");
        }
    }


    void OnTriggerExit(Collider col)
    {

        // Remove the GameObject collided with from the list.
        if (col.gameObject.GetComponent<BasicItem>() != null)
        {
            currentSpecialItems.Remove(col.gameObject);
            Debug.Log("you removed a special item");
        }
    }
}
