﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixerSnapshot StartGameSnapshot;
    public AudioMixerSnapshot FirstItemSnapshot;
    public AudioMixerSnapshot AlmostWinSnapShot;
    public AudioMixerSnapshot EndGameSnapshot;
    public AudioSource WinSource;
    public AudioSource WinStinger;
    public AudioSource LoseSource;
    public AudioSource LoseImpact;
    public AudioSource WinLaugh;
    public List<AudioSource> LoseSources;


    public static AudioManager Instance { private set; get; }

    void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGameSnapshot.TransitionTo(0);
    }

    public void FirstItem()
    {
        FirstItemSnapshot.TransitionTo(2f);
    }

    public void AlmostWin()
    {
        AlmostWinSnapShot.TransitionTo(2f);
    }

    public void Win()
    {
        EndGameSnapshot.TransitionTo(0.5f);
        WinStinger.Play();
        WinSource.PlayDelayed(1.0f);
        WinLaugh.PlayDelayed(1.0f);
    }

    public void Lose()
    {
        EndGameSnapshot.TransitionTo(0.5f);
        LoseSource.Play();
        LoseImpact.Play();
        int loseIndex = (int)Random.Range(0, LoseSources.Count);
        LoseSources[loseIndex].PlayDelayed(1.0f);
    }
}
