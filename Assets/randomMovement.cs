using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMovement : MonoBehaviour
{
    float r = 0;
    Vector2 randV2;
    Vector3 randPosition;
    // Start is called before the first frame update
    void Start()
    {
        print("start");
    }

    // Update is called once per frame
    void Update()
    {
        print("update");
        if(r <= 0)
        {
            randV2 = Random.insideUnitCircle;
            randPosition = new Vector3(transform.position.x + 6 * randV2.x, transform.position.y + 6 * randV2.y, transform.position.z);
            print(randPosition);
            print(transform.position);
            r = Random.value * 50;
        }
        transform.position = Vector3.MoveTowards(transform.position, randPosition, 0.03f);
        r--;
    }
}
