using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    public enum TargetType {
        Book,
        Player,
        Courtyard,
    };

    public TargetType DemonTargetType = TargetType.Book;
    GameObject target;
    GameObject[] enemies;

    public Sprite[] MoveSprites;

    public float InitialMoveSpeed = 1f;

    private SpriteRenderer mSprite;
    public float MoveFramerate = 5;

    public float Acceleration = 1f;

    public float awayFromEnemiesSpeed = .01f;
    public float SeekTargetJitterFactor = .5f;
    public float RandomTargetSeekIntervalMax = 3;

    public float RandomTargetSeekIntervalMin = 2;

    float mCurrentTargetSeekInterval = 0;
    float elapsedTimeTillNextTargetSeek = 100;

    float mFrameCounter;

    Vector3 mVelocity = Vector3.zero;

    GameObject closestEnemy;

    GameObject player1 = null;
    GameObject player2 = null;
    void Awake() {
         mSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        ChooseTarget();
    }

    void ChooseTarget() {
        switch(DemonTargetType) {
            case TargetType.Book:
                target = BookController.Instance.gameObject;
                break;
            case TargetType.Player:
                player1 = GameObject.FindGameObjectWithTag("player1");
                player2 = GameObject.FindGameObjectWithTag("player2");
                break;
            case TargetType.Courtyard:
                target = FindObjectsOfType<CourtYardScript>()[0].gameObject;
                break;
            default:
                break;
        }
    }

    GameObject FindClosestPlayer() {
        float distPlayer1 = (transform.position - player1.transform.position).magnitude;
        float distPlayer2 = (transform.position - player2.transform.position).magnitude;
        if (distPlayer1 > distPlayer2) {
            return player2;
        } else {
            return player1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DemonTargetType == TargetType.Player) {
            target = FindClosestPlayer();
        }
        mFrameCounter += MoveFramerate * mVelocity.magnitude * Time.deltaTime;
        if (MoveSprites != null && MoveSprites.Length > 0)
        {
            int frame = ((int)mFrameCounter) % MoveSprites.Length;
            mSprite.sprite = MoveSprites[frame];
        }

        elapsedTimeTillNextTargetSeek += Time.deltaTime;
        MoveTowardsTarget();
        // MoveAwayFromEnemies();
    }

    void MoveTowardsTarget()
    {
        if (elapsedTimeTillNextTargetSeek > mCurrentTargetSeekInterval)
        {
            elapsedTimeTillNextTargetSeek = 0f;
            Vector3 targetPosition = target.transform.position;
            Vector2 radiusDisplacement = Random.insideUnitCircle * SeekTargetJitterFactor;
            Vector3 newTowardsPosition = new Vector3(targetPosition.x + radiusDisplacement.x, targetPosition.y + radiusDisplacement.y, targetPosition.z);

            mCurrentTargetSeekInterval = Random.value * (RandomTargetSeekIntervalMax - RandomTargetSeekIntervalMin) + RandomTargetSeekIntervalMin;
            Vector3 vel = (newTowardsPosition - transform.position);
            vel.z = 0;
            mVelocity = vel.normalized * InitialMoveSpeed;
        }
        mVelocity *= Acceleration;
    
        transform.position += mVelocity * Time.deltaTime;
    }



    void MoveAwayFromEnemies()
    {
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < closestDistance && Vector3.Distance(transform.position, enemy.transform.position) != 0f)
            {
                closestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                closestEnemy = enemy;
            }
        }
        if (closestEnemy == null) {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, -1 * awayFromEnemiesSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player1" || other.gameObject.tag == "player2")
        {
            GameStateManager.Instance.Lose();
        }
    }
}
