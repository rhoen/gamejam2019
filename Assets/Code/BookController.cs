using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : PickUpDroppableItem {

    public static BookController Instance { get; private set;}

    void onAwake() {
        BookController.Instance = this;
    }

    public override void PickUp() {
        Debug.Log("picked up!");
        transform.localScale = new Vector3(.5f, .5f, 1f);
    }

    public override void Drop() {
        Debug.Log("dropped!");
        transform.localScale = new Vector3(1, 1, 1);
    }
    
}