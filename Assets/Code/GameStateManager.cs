using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { private set; get; }

    void Awake() {
        Instance = this;
    }

    void Start() {
        int playerId = Random.value < 0.5f ? 1 : 2;
        PlayerController player = GameObject.FindGameObjectWithTag("player" + playerId).GetComponent<PlayerController>();
        player.DropThenPickUpBook();
    }

    public void Lose() {
        AudioManager.Instance.Lose();
    }

    public void Win() {
        AudioManager.Instance.Win();
    }

}