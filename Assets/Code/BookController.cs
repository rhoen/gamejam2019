using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : PickUpDroppableItem {

    public GameObject SendLightPrefab;
    public static BookController Instance { get; private set;}

    void Awake() {
        BookController.Instance = this;
    }

    public void SendToPlayer(int playerId) {
        GameObject clone = Instantiate(SendLightPrefab, transform.position, transform.rotation);
        clone.GetComponent<SendBookController>().SendToPlayer(playerId);
        DemonManager.Instance.setEnemiesTowardPlayer(playerId);
        DemonManager.Instance.CreateEnemyTowardPlayer(playerId);
    }

    public override void PickUp() {
    }

    public override void Drop() {
    }
    
}