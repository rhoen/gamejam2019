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
        AudioManager.Instance.Lose();
    }


    public void Win() {
        AudioManager.Instance.Win();
    }

}