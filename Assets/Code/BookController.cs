using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : PickUpDroppableItem {

    public GameObject SendLightPrefab;
    public static BookController Instance { get; private set;}

    void onAwake() {
        BookController.Instance = this;
    }

    public void sendToOtherPlayer() {
        Instantiate(SendLightPrefab, transform.position, transform.rotation);
    }

    public override void PickUp() {
        Debug.Log("picked up!");
    }

    public override void Drop() {
        Debug.Log("dropped!");
    }
    
}