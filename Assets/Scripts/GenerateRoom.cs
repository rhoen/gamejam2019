using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour
{
    int MIN_LENGTH = 3;
    int MAX_LENGTH = 10;
    public GameObject wallPrefab;
    private List<GameObject> walls = new List<GameObject>();

    private void Awake()
    {
        MakeRoom();
    }

    void MakeRoom()
    {

        for (int i = 0; i < 4; i++)
        {
            int width = Random.Range(MIN_LENGTH, MAX_LENGTH);
            int length = Random.Range(MIN_LENGTH, MAX_LENGTH);
            GameObject wall = Instantiate(wallPrefab);
            BoxCollider2D boxCollider = wall.GetComponent<BoxCollider2D>();
            boxCollider.size.Set(width, length);
            SpriteRenderer s_renderer = wall.GetComponent<SpriteRenderer>();
            s_renderer.size = new Vector2(width, length);
            //SetWallPosition(wall);
            walls.Add(wall);
        }
    }

    //void SetWallPosition(GameObject wall)
    //{
    //    GameObject prevWall = walls[walls.Count - 1];

    //}
}
