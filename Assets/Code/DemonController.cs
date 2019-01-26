﻿using System.Collections;
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

    float x;
    float y;

    public float towardsPlayerSpeed = .03f;
    public float awayFromEnemiesSpeed = .01f;

    public float seekPlayerJitterFactor = 2;
    public float nextPlayerSeekInterval = 2f;
    float elapsedTimeTillnextPlayerSeek = 100;
    
    Vector3 newPosition;

    public string player1;
    public string player2;


    // Start is called before the first frame update
    void Start()
    {
        character1 = GameObject.Find(player1);
        character2 = GameObject.Find(player2);

        chaseCharacter = character1;

        enemies = GameObject.FindGameObjectsWithTag("enemy");
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

        //MoveDirect();
        MoveNear();
        MoveAwayFromEnemies();

    }

    private void MoveDirect()
    {
        characterDistance = Vector3.Distance(transform.position, chaseCharacter.transform.position);

        if (characterDistance > 4)
        {
            towardsPlayerSpeed = 0.05f;
        }
        else
        {
            towardsPlayerSpeed = 0.03f;
        }
        transform.position = Vector3.MoveTowards(transform.position, chaseCharacter.transform.position, towardsPlayerSpeed);
    }


    void MoveNear()
    {

        Vector3 characterPosition = chaseCharacter.transform.position;
        if (elapsedTimeTillnextPlayerSeek > nextPlayerSeekInterval)
        {
            Vector2 radiusDisplacement = Random.insideUnitCircle;

            newPosition = new Vector3(characterPosition.x + radiusDisplacement.x * seekPlayerJitterFactor, characterPosition.y + radiusDisplacement.y * seekPlayerJitterFactor, transform.position.z);

            elapsedTimeTillnextPlayerSeek = 0f;
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

}
