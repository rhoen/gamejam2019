using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBookController : PickUpDroppableItem {

    public float TrackSpeed = 0.02f;
    public float MinX = float.NegativeInfinity;
    public float MaxX = float.PositiveInfinity;

    private int? trackPlayerId = null;

    private GameObject Target;

    public void SendToPlayer(int playerId) {
        PlayerController playerToSend = null;
        if (playerId == 1) {
            playerToSend = GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerController>();
        } else {
            playerToSend = GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerController>();
        }
        Target = playerToSend.gameObject;
        trackPlayerId = playerId;
    }


    public void OnTriggerEnter(Collider other) {
        if (trackPlayerId == null) { return; }
        PlayerController otherPlayer = other.GetComponent<PlayerController>();
        if (otherPlayer == null) {
            return;
        }

        if (otherPlayer.PlayerId == trackPlayerId) {
            otherPlayer.DropThenPickUpBook();
            Destroy(gameObject);
        }
    }
    void Update() {
        if (trackPlayerId == null) { return; }
        Vector3 pos = transform.position;
        pos.x = Mathf.Max(MinX, pos.x);
        pos.x = Mathf.Min(MaxX, pos.x);
        pos.x = Mathf.Lerp(pos.x, Target.transform.position.x, TrackSpeed);

        pos.y = Mathf.Max(MinX, pos.y);
        pos.y = Mathf.Min(MaxX, pos.y);
        pos.y = Mathf.Lerp(pos.y, Target.transform.position.y, TrackSpeed);
        pos.z = BookController.Instance.transform.position.z;
        BookController.Instance.transform.position = pos;
        pos.z = -0.26f;
        transform.position = pos;
    }
}
