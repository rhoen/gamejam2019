using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    GameObject target;
    GameObject[] enemies;

    GameObject closestEnemy;
    float characterDistance;

    public float towardsPlayerSpeed = .02f;
    public float awayFromEnemiesSpeed = .01f;

    public float seekPlayerJitterFactor = 2;
    public float nextPlayerSeekInterval = 2f;
    float elapsedTimeTillnextPlayerSeek = 100;

    public float maxMovementTime = 5;
    
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        target = BookController.Instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeTillnextPlayerSeek += Time.deltaTime;
        MoveNear();
        MoveAwayFromEnemies();
    }

    void MoveNear()
    {
        Vector3 characterPosition = target.transform.position;
        if (elapsedTimeTillnextPlayerSeek > nextPlayerSeekInterval)
        {
            Vector2 radiusDisplacement = Random.insideUnitCircle;

            newPosition = new Vector3(characterPosition.x + radiusDisplacement.x * seekPlayerJitterFactor, characterPosition.y + radiusDisplacement.y * seekPlayerJitterFactor, transform.position.z);

            elapsedTimeTillnextPlayerSeek = 0f;
            nextPlayerSeekInterval = Random.value * maxMovementTime;
        }

        transform.position = Vector3.MoveTowards(transform.position, newPosition, towardsPlayerSpeed);
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
