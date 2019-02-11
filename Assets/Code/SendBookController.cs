using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBookController : PickUpDroppableItem {
    const float TRIGGER_THRESHOLD = .1f;

    const float ACCELERATE_AFTER_TIME_ALIVE = 3f;
    public float Speed = 2.5f;
    
    int? trackPlayerId = null;
    GameObject mTarget;
    Vector3 mVelocity = Vector3.zero;

    float mActiveTime = 0f;

    public void SendToPlayer(int playerId) {
        PlayerController playerToSend = null;
        if (playerId == 1) {
            playerToSend = GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerController>();
        } else {
            playerToSend = GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerController>();
        }
        mTarget = playerToSend.gameObject;
        trackPlayerId = playerId;
    }

    void FixedUpdate()
    {
        if (trackPlayerId == null) { return; }
        mActiveTime += Time.deltaTime;
        if (Vector3.Distance(mTarget.transform.position, transform.position) < TRIGGER_THRESHOLD)
        {
            PlayerController otherPlayer = mTarget.GetComponent<PlayerController>();
            if (otherPlayer == null) {
                return;
            }
            if (otherPlayer.PlayerId == trackPlayerId) {
                otherPlayer.DropThenPickUpBook();
                Destroy(gameObject);
            }
        }
    }

    void Update() {
        if (trackPlayerId == null) { return; }
        float accel = Mathf.Pow((mActiveTime - ACCELERATE_AFTER_TIME_ALIVE) / ACCELERATE_AFTER_TIME_ALIVE, 2) + 1;
        mVelocity = (mTarget.transform.position - transform.position).normalized * Speed * accel;
        transform.position += mVelocity * Time.deltaTime;
        BookController.Instance.transform.position = transform.position;
    }
}
