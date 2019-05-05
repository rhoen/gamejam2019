using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    const float REST_THRESHOLD = 0.25f;
    public float VELOCITY_DECAY = 0.90f;
    public Sprite[] RestSprites;
    public Sprite[] RestingWithItemSprites;
    
    public Sprite[] MoveSprites;
    public Sprite[] MovingWithItemSprites;
    public float MoveFramerate = 5;

    public float RestFramerate = 3;

    float mFrameCounter;
    public int PlayerId = 1;
    public float MovementSpeed = 5;

    private float HeldItemOffset = .2f;
    Vector3 mVelocity = Vector3.zero;
    Vector3 mFacingDirection = Vector3.right;

    PickUpDroppableItem mClosestItem = null;
    PickUpDroppableItem mCurrentlyHeldItem = null;

    BookController mBook;
    MementoController mMemento;
    private CharacterController mCharController;

    private SpriteRenderer mPlayerSprite;
    private SpriteRenderer mHandSprite;

    private State mCurrentState;

    private Vector3 mOffsetForCurrentlyHeldItem = Vector3.zero;
    private float mSpriteDiagonalOfHeldItem = 0f;

    public float HeldItemWeightMultiplier = 3;

    public enum State
    {
        Resting,
        RestingWithItem,
        Moving,
        MovingWithItem
    }

    void Awake() {
         mCharController = GetComponent<CharacterController>();
         mPlayerSprite = GetComponent<SpriteRenderer>();
         mHandSprite = GetComponentsInChildren<SpriteRenderer>()[1];
    }
    void Update() {
        if (mVelocity.magnitude < REST_THRESHOLD)
        {
            if (mCurrentlyHeldItem != null) {
                TransitionState(State.RestingWithItem);
            } else {
                TransitionState(State.Resting);
            }
        }

        mCharController.Move(mVelocity * Time.deltaTime);
        if (mCurrentlyHeldItem != null) {
            mCurrentlyHeldItem.transform.position = transform.position + (mFacingDirection * HeldItemOffset) - mOffsetForCurrentlyHeldItem;
        }

        Sprite[] spriteArray = null;
        Sprite[] handSpriteArray = null;
        switch (mCurrentState)
        {
            case State.MovingWithItem:
                spriteArray = MoveSprites;
                handSpriteArray = MovingWithItemSprites;
                mFrameCounter += MoveFramerate * (mVelocity.magnitude / MovementSpeed) * Time.deltaTime;
                break;

            case State.Moving:
                spriteArray = MoveSprites;
                handSpriteArray = null;
                mFrameCounter += MoveFramerate * (mVelocity.magnitude / MovementSpeed) * Time.deltaTime;
                break;

            case State.RestingWithItem:
                spriteArray = RestSprites;
                handSpriteArray = RestingWithItemSprites;
                mFrameCounter += RestFramerate * Time.deltaTime;
                break;

            case State.Resting:
                spriteArray = RestSprites;
                handSpriteArray = null;
                mFrameCounter += RestFramerate * Time.deltaTime;
                break;
        }
        
        if (spriteArray != null && spriteArray.Length > 0)
        {
            int frame = ((int)mFrameCounter) % spriteArray.Length;
            mPlayerSprite.sprite = spriteArray[frame];
        }

        if (handSpriteArray != null && handSpriteArray.Length > 0) {
            int frame = ((int)mFrameCounter) % handSpriteArray.Length;
            mHandSprite.sprite = handSpriteArray[frame];
        } else {
            mHandSprite.sprite = null;
        }
    }

    void FixedUpdate()
    {
        mVelocity *= VELOCITY_DECAY;
    }

    public PickUpDroppableItem CurrentlyHeldItem()
    {
        return mCurrentlyHeldItem;
    }

    public void OnAxisInput(float horizontal, float vertical) {
        float speedWithWeight = MovementSpeed / Mathf.Max(1, mSpriteDiagonalOfHeldItem * HeldItemWeightMultiplier);
        mVelocity += new Vector3(speedWithWeight * horizontal, speedWithWeight * vertical, 0);
        if (Mathf.Abs(horizontal) > .1f || Mathf.Abs(vertical) > .1f) {
            if (Mathf.Abs(horizontal) > .1f) {
                mFacingDirection = horizontal > 0 ? Vector3.right : Vector3.left;
            }
            if (mCurrentlyHeldItem != null) {
                TransitionState(State.MovingWithItem);
            } else {
                TransitionState(State.Moving);
            }
        }
        mPlayerSprite.flipX = mFacingDirection != Vector3.right;
        mHandSprite.flipX = mFacingDirection != Vector3.right;
    }

    public void OnAButton() {
        PickUpDroppableItem previouslyHeldItem = DropCurrentItem();
        if (mClosestItem != previouslyHeldItem) {
            pickupClosestItem();
        }
        if (previouslyHeldItem is BookController) {
            sendBookToOtherPlayer();
        }
    }

    public void Die() {
        
    }

    public void OnBButton() {
        // nothing yet? maybe not neeeded
    }

    // Called by SendBookController/GameStateManager 
    public void DropThenPickUpBook() {
        DropCurrentItem();
        pickupBook();
    }

    private void sendBookToOtherPlayer() {
        int otherPlayerId = 1;
        if (PlayerId == 1) {
            otherPlayerId = 2;
        }
        BookController.Instance.SendToPlayer(otherPlayerId);
    }

    private void pickupBook() {
        mClosestItem = BookController.Instance;
        pickupClosestItem();
    }

    private void pickupClosestItem() {
        if (mClosestItem == null) {
            return;
        }
        mClosestItem.PickUp();
        mCurrentlyHeldItem = mClosestItem;
        mClosestItem = null;
        if (mCurrentlyHeldItem.GetComponent<BoxCollider>() != null) {
            mOffsetForCurrentlyHeldItem = new Vector3(0, mCurrentlyHeldItem.GetComponent<BoxCollider>().center.y, 0);
        }
        if (mCurrentlyHeldItem.GetComponent<SpriteRenderer>()) {
            mSpriteDiagonalOfHeldItem = (mCurrentlyHeldItem.GetComponent<SpriteRenderer>().sprite.bounds.min - mCurrentlyHeldItem.GetComponent<SpriteRenderer>().sprite.bounds.max).magnitude;
        }
    }

    public PickUpDroppableItem DropCurrentItem() {
        if (mCurrentlyHeldItem == null) {
            return null;
        }
        PickUpDroppableItem previouslyHeldItem = mCurrentlyHeldItem;
        mCurrentlyHeldItem.Drop();
        mCurrentlyHeldItem = null;
        mOffsetForCurrentlyHeldItem = Vector3.zero;
        mSpriteDiagonalOfHeldItem = 0f;
        return previouslyHeldItem;
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
    void TransitionState(State state)
    {
        mCurrentState = state;
    }
}