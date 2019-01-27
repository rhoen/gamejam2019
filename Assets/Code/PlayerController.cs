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

    private SpriteRenderer mPlayerSprite;
    private SpriteRenderer mHandSprite;

    private State mCurrentState;

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
            if (mCurrentlyHeldObject != null) {
                TransitionState(State.RestingWithItem);
            }
            TransitionState(State.Resting);
        }

        mCharController.Move(mVelocity * Time.deltaTime);
        if (mCurrentlyHeldObject != null) {
            mCurrentlyHeldObject.transform.position = transform.position + (mFacingDirection * HeldObjectOffset);
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
            if (handSpriteArray != null) {
                mHandSprite.sprite = handSpriteArray[frame];
            } else {
                mHandSprite.sprite = null;
            }
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
            if (mCurrentlyHeldObject != null) {
                TransitionState(State.MovingWithItem);
            } else {
                TransitionState(State.Moving);
            }
        }
        mPlayerSprite.flipX = mFacingDirection != Vector3.right;
        mHandSprite.flipX = mFacingDirection != Vector3.right;
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
    void TransitionState(State state)
    {
        mCurrentState = state;
    }
}