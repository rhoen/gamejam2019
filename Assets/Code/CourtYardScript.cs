using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtYardScript : MonoBehaviour
{
    private AudioManager mAudioManager;
    
    public bool mHasWon;
    private bool mIsPlayingWinAnimation;

    List<GameObject> mPlayersInRoom = new List<GameObject>();
    PentagramSlotController[] mPentagramSlots = null;

    void Start() {
        mAudioManager = AudioManager.Instance;
        mPentagramSlots = GetComponentsInChildren<PentagramSlotController>();
    }
  
    void Update()
    {
        if(NumSpecialItemsInRoom() == 1)
        {
            mAudioManager.FirstItem();
        }

        if (NumSpecialItemsInRoom() == 3)
        {
            mAudioManager.AlmostWin();
        }
        maybeWin();
    }

    int NumSpecialItemsInRoom() {
        int count = 0;
        foreach (PentagramSlotController slot in mPentagramSlots) {
            if (slot.isFilled) {
                count++;
            }
        }
        return count;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            mPlayersInRoom.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            mPlayersInRoom.Remove(other.gameObject);
        }
    }

    void maybeWin()
    {
        if ((NumSpecialItemsInRoom() == 5 && areBothPlayersPresent()) || mHasWon)
        {
            if (!mIsPlayingWinAnimation)
            {
                mAudioManager.Win();
                mIsPlayingWinAnimation = true;
                StartCoroutine(GameObject.Find("Background").GetComponent<EndGameAnimatorScript>().Animate());
                StartCoroutine(GameObject.Find("DemonForeground").GetComponent<EndGameAnimatorScript>().Animate());
                StartCoroutine(GameObject.Find("RocksForeground").GetComponent<EndGameAnimatorScript>().Animate());
            }
        }
    }

    private bool areBothPlayersPresent()
    {
        bool notHolding = true;
        foreach (GameObject player in mPlayersInRoom)
        {
            if (player.GetComponent<PlayerController>().CurrentlyHeldItem())
            {
                notHolding = false;
            }
        }
        return notHolding && mPlayersInRoom.Count == 2;
    }


}
