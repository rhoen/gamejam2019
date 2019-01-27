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

    private void OnTriggerEnter(Collider other)
    {
        
    }

    // Declare and initialize a new List of GameObjects called currentCollisions.
    List<GameObject> currentSpecialItems = new List<GameObject>();

    void OnTriggerEnter(Collision col)
    {

        // Add the GameObject collided with to the list.
        if (col.gameObject.GetType() == typeof(BasicItem))
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

    void OnTriggerExit(Collision col)
    {

        // Remove the GameObject collided with from the list.
        if (col.gameObject.GetType() == typeof(BasicItem))
        {
            currentSpecialItems.Remove(col.gameObject);
            Debug.Log("you removed a special item");
        }
    }
}
