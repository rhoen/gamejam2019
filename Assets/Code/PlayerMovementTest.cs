using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = .08f;
    KeyCode up;
    KeyCode down;
    KeyCode left;
    KeyCode right;

    void Start()
    {
        if(gameObject.name == "character1")
        {
            print("character 1");
            up = KeyCode.UpArrow;
            down = KeyCode.DownArrow;
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
        }
        else
        {
            up = KeyCode.W;
            down = KeyCode.S;
            left = KeyCode.A;
            right = KeyCode.D;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveChar();
    }

    void moveChar()
    {
        //animator.SetFloat("Speed", 0);
        //direction = 0;
        if (Input.GetKey(up))
        {
            transform.Translate(0, speed, 0);
            //animator.SetFloat("Speed", 1);
            //direction = 1;
        }
        if (Input.GetKey(down))
        {
            transform.Translate(0, (-1) * speed, 0);
            //animator.SetFloat("Speed", 1);
            //direction = 2;
        }
        if (Input.GetKey(left))
        {
            transform.Translate((-1) * speed, 0, 0);
            transform.localScale = (new Vector3(-1, 1, 1));
            //animator.SetFloat("Speed", 1);
            //direction = 3;
        }
        if (Input.GetKey(right))
        {
            transform.Translate(speed, 0, 0);
            transform.localScale = (new Vector3(1, 1, 1));
            //animator.SetFloat("Speed", 1);
            //direction = 4;
        }

    }
}
