using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
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
        closestDistance = 100f;
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < closestDistance && Vector3.Distance(transform.position, enemy.transform.position) != 0f)
            {
                closestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                closestEnemy = enemy;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(chaseCharacter == character1)
            {
                chaseCharacter = character2;
            }
            else
            {
                chaseCharacter = character1;
            }
        }
        characterDistance = Vector3.Distance(transform.position, chaseCharacter.transform.position);

        //if (closestDistance < 4)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, -1 * speed / 2);
        //}



        //if (Vector3.Distance(transform.position, chaseCharacter.transform.position) > 7)
        //{
        //    speed1 = 0.06f;
        //    speed2 = 0.0025f;
        //} 
        //else if (Vector3.Distance(transform.position, chaseCharacter.transform.position) > 5)
        //{
        //    speed1 = 0.04f;
        //    speed2 = 0.005f;
        //}
        //else
        //{
        //    speed1 = 0.03f;
        //    speed2 = 0.015f;
        //}
        if(characterDistance > 4)
        {
            speed1 = 0.05f;
        }
        else
        {
            speed1 = 0.03f;
        }
        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, speed1);
        //if(closestDistance < characterDistance)
        //{
            transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, -1 * speed2);
        //}













        //switch (gameObject.name)
        //{
        //    case "enemy1":
        //        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, speed);
        //        break;
        //    case "enemy2":
        //        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, speed);
        //        break;
        //    case "enemy3":
        //        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, speed);
        //        break;
        //    case "enemy4":
        //        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, speed);
        //        break;
        //    case "enemy5":
        //        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, speed);
        //        break;
        //    case "enemy6":
        //        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, speed);
        //        break;
        //    case "enemy7":
        //        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, speed);
        //        break;
        //}
    }

}
