using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    GameObject character1;
    GameObject character2;

    GameObject chaseCharacter;
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
        character1 = GameObject.FindGameObjectWithTag("player1");
        character2 = GameObject.FindGameObjectWithTag("player2");

        chaseCharacter = character1;

        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    public void setTargetPlayerId(int id) {
        if (id == 1) {
            chaseCharacter = character1;
        } else {
            chaseCharacter = character2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeTillnextPlayerSeek += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (chaseCharacter == character1)
            {
                chaseCharacter = character2;
            }
            else
            {
                chaseCharacter = character1;
            }
        }

        MoveNear();
        MoveAwayFromEnemies();

    }

    void MoveNear()
    {

        Vector3 characterPosition = chaseCharacter.transform.position;
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
        if(other.gameObject.tag == "player1" || other.gameObject.tag == "player2")
        {
            GameStateManager.Instance.Lose();
        }
    }
}
