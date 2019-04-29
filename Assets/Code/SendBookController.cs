using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBookController : MonoBehaviour {
    const float TRIGGER_THRESHOLD = .1f;

    const float ACCELERATE_AFTER_TIME_ALIVE = 3f;
    public float Speed = 2.5f;
    
    GameObject mTarget;
    Vector3 mVelocity = Vector3.zero;
    float mActiveTime = 0f;

    public void SendToPlayer(int playerId) {
        PlayerController playerToSend = GameObject.FindGameObjectWithTag("player" + playerId).GetComponent<PlayerController>();
        mTarget = playerToSend.gameObject;
    }

    void FixedUpdate()
    {
        if (mTarget == null) { return; }
        mActiveTime += Time.deltaTime;
        if (Vector3.Distance(mTarget.transform.position, transform.position) < TRIGGER_THRESHOLD)
        {
            PlayerController otherPlayer = mTarget.GetComponent<PlayerController>();
            otherPlayer.DropThenPickUpBook();
            Reset();
        }
    }

    void Reset() {
        mTarget = null;
        mVelocity = Vector3.zero;
        mActiveTime = 0f;
    }

    void Update() {
        if (mTarget == null) { return; }
        float accel = Mathf.Pow((mActiveTime - ACCELERATE_AFTER_TIME_ALIVE) / ACCELERATE_AFTER_TIME_ALIVE, 2) + 1;
        mVelocity = (mTarget.transform.position - transform.position).normalized * Speed * accel;
        transform.position += mVelocity * Time.deltaTime;
        BookController.Instance.transform.position = transform.position;
    }
}
