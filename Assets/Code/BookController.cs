using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : PickUpDroppableItem {

    public static BookController Instance { get; private set;}

    Light mLight;

    [Range(0,10)]
    public float MaxIntensity;
    [Range(0,10)]
    public float MinIntensity;

    [Range(0,10)]
    public float MaxRange;
    [Range(0,10)]
    public float MinRange;

    private bool isCurrentlyMoving = false;

    void Awake() {
        BookController.Instance = this;
        mLight = GetComponentInChildren<Light>();
    }

    void Update() {
        if (isCurrentlyMoving) {
            if (mLight.intensity < MaxIntensity) {
                mLight.intensity *= 1.02f;
            }
            if (mLight.range < MaxRange) {
                mLight.range *= 1.02f;
            }
        } else {
            if (mLight.intensity > MinIntensity) {
                mLight.intensity *= .99f;
            }
            if (mLight.range > MinRange) {
                mLight.range *= .99f;
            }
        }
    }

    public void SendToPlayer(int playerId) {
        GetComponent<SendBookController>().SendToPlayer(playerId);
    }

    public override void PickUp() {
        isCurrentlyMoving = false;
    }

    public override void Drop() {
        isCurrentlyMoving = true;
    }
    
}