using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room : MonoBehaviour
{
    public int width;
    public int height;
    // Start is called before the first frame update
    
    void OnValidate()
    {
        // GameObject left = GetComponentsInChildren<GameObject>()[0];
        // GameObject right = GetComponentsInChildren<GameObject>()[1];
        // GameObject top = GetComponentsInChildren<GameObject>()[2];
        // GameObject bottom = GetComponentsInChildren<GameObject>()[3];

        // left.transform.localScale = new Vector3(1, height, 1);
        // right.transform.localScale = new Vector3(1, height, 1);
        // top.transform.localScale = new Vector3(width, 1, 1);
        // bottom.transform.localScale = new Vector3(width, 1, 1);
    }


}
