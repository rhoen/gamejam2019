using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { private set; get; }

    void Awake() {
        Instance = this;
    }

    public void Lose() {
        Debug.Log("GAME OVER");
        //play lose music, restart scene
    }


    public void Win() {
        //play win music
    }

}