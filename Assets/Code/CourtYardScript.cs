﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtYardScript : MonoBehaviour
{
    private AudioManager mAudioManager;
    
    public bool mHasWon;
    private bool mIsPlayingWinAnimation;

    List<GameObject> mSpecialItemsInRoom = new List<GameObject>();
    List<GameObject> mPlayersInRoom = new List<GameObject>();

    PentagramSlotController[] mPentagramSlots = null;

    void Start() {
        mAudioManager = AudioManager.Instance;
        mPentagramSlots = GetComponentsInChildren<PentagramSlotController>();
    }
  
    void Update()
    {
        if(mSpecialItemsInRoom.Count == 1)
        {
            mAudioManager.FirstItem();
        }

        if (mSpecialItemsInRoom.Count == 3)
        {
            mAudioManager.AlmostWin();
        }
        maybeWin();
    }

    void addSpecialItemToPentagram(BasicItem item) {
        // animate to pentagram slot
        // disable item, and change pentragram animation color
    }

    void OnTriggerEnter(Collider other)
    {
        BasicItem item = other.gameObject.GetComponent<BasicItem>();
        if (item && item.specialItem)
        {
            if (!mSpecialItemsInRoom.Contains(other.gameObject)) {
                mSpecialItemsInRoom.Add(other.gameObject);
            }
        }

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
        if ((mSpecialItemsInRoom.Count == 5 && areBothPlayersPresent()) || mHasWon)
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
            if (player.GetComponent<PlayerController>().IsHoldingItem())
            {
                notHolding = false;
            }
        }
        return notHolding && mPlayersInRoom.Count == 2;
    }


}
