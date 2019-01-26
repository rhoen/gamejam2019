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
    float closestDistance = 100f;
    float characterDistance;

    float x;
    float y;

    public float speed1 = .03f;
    public float speed2 = .01f;

    float timer = 0f;
    Vector3 newPosition;


    // Start is called before the first frame update
    void Start()
    {
        character1 = GameObject.Find("character1");
        character2 = GameObject.Find("character2");

        chaseCharacter = character1;

        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
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

        //MoveDirect();
        MoveNear();
        MoveAwayFromEnemies();

    }

    private void MoveDirect()
    {
        characterDistance = Vector3.Distance(transform.position, chaseCharacter.transform.position);

        if (characterDistance > 4)
        {
            speed1 = 0.05f;
        }
        else
        {
            speed1 = 0.03f;
        }
        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, speed1);
    }


    void MoveNear()
    {

        Vector3 characterPosition = chaseCharacter.transform.position;
        if (timer > 1f)
        {
            Vector2 randV2 = Random.insideUnitCircle;

            newPosition = new Vector3(characterPosition.x + randV2.x * 2, characterPosition.y + randV2.y * 2, transform.position.z);

            timer = 0f;
        }

        transform.position = Vector3.MoveTowards(transform.position, newPosition, 0.03f);
    }



    void MoveAwayFromEnemies()
    {
        closestDistance = 100f;
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < closestDistance && Vector3.Distance(transform.position, enemy.transform.position) != 0f)
            {
                closestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                closestEnemy = enemy;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, -1 * speed2);
    }

}
