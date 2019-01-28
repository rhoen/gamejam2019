using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : PickUpDroppableItem {

    public GameObject SendLightPrefab;
    public static BookController Instance { get; private set;}

    Light mLight;

    void Awake() {
        BookController.Instance = this;
        mLight = GetComponentInChildren<Light>();
    }

    public void SendToPlayer(int playerId) {
        GameObject clone = Instantiate(SendLightPrefab, transform.position, transform.rotation);
        clone.GetComponent<SendBookController>().SendToPlayer(playerId);
    }

    public override void PickUp() {
        // lerp
        mLight.range = 3f;
        mLight.intensity = 4f;
    }

    public override void Drop() {
        mLight.range = 1.8f;
        mLight.intensity = 1.6f;
    }
    
}