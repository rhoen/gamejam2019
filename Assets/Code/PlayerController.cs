using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float VELOCITY_DECAY = 0.5f;

    public int PlayerId;
    public float MovementSpeed = 5;

    public float HeldObjectOffset = .5f;
    Vector3 mVelocity = Vector3.zero;
    Vector3 mFacingDirection = Vector3.right;

    PickUpDroppableItem mClosestItem = null;
    PickUpDroppableItem mCurrentlyHeldObject = null;

    BookController mBook;
    MementoController mMemento;
    private CharacterController mCharController;

    void Awake() {
         mCharController = GetComponent<CharacterController>();
    }
    void Update() {
        mCharController.Move(mVelocity * Time.deltaTime);
        if (mCurrentlyHeldObject != null) {
            mCurrentlyHeldObject.transform.position = transform.position + (mFacingDirection * HeldObjectOffset);
        }
    }

    void FixedUpdate()
    {
        mVelocity *= VELOCITY_DECAY;
    }

    public void OnAxisInput(float horizontal, float vertical) {
        mVelocity += new Vector3(MovementSpeed * horizontal, MovementSpeed * vertical, 0);
        if (Mathf.Abs(horizontal) > .1f || Mathf.Abs(vertical) > .1f) {
            if (Mathf.Abs(vertical) > Mathf.Abs(horizontal)) {
                mFacingDirection = vertical > 0 ? Vector3.up : Vector3.down;
            } else {
                mFacingDirection = horizontal > 0 ? Vector3.right : Vector3.left;
            }
        }
    }

    public void OnAButton() {
        dropThenPickUpClosestItemIfExists();
    }

    public void OnBButton() {
        // send away
    }

    private void dropThenPickUpBook() {
        // when receiving sent book
    }

    private void dropThenPickUpClosestItemIfExists() {
        PickUpDroppableItem previouslyHeldObject = mCurrentlyHeldObject;
        if (mCurrentlyHeldObject != null) {
            mCurrentlyHeldObject.Drop();
            mCurrentlyHeldObject = null;
        }
        if (mClosestItem != null && mClosestItem != previouslyHeldObject) {
            mClosestItem.PickUp();
            mCurrentlyHeldObject = mClosestItem;
            mClosestItem = null;    
        }
    }

    public void OnTriggerEnter(Collider other) {
        MonoBehaviour[] list = other.gameObject.GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour mb in list)
        {
            if (mb is PickUpDroppableItem)
            {
                mClosestItem = (PickUpDroppableItem)mb;
                break;
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        MonoBehaviour[] list = other.gameObject.GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour mb in list)
        {
            if (mb is PickUpDroppableItem)
            {
                mClosestItem = null;
                break;
            }
        }
    }
}