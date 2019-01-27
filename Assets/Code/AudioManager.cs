using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixerSnapshot StartGameSnapshot;
    public AudioMixerSnapshot FirstItemSnapshot;
    public AudioMixerSnapshot AlmostWinSnapShot;
    public AudioMixerSnapshot WinSnapshot;

    public static AudioManager Instance { private set; get; }
    // Start is called before the first frame update
    void Start()
    {
        StartGameSnapshot.TransitionTo(0);
    }

    void FirstItem()
    {
        FirstItemSnapshot.TransitionTo(0);
    }

    public void AlmostWin()
    {
        AlmostWinSnapShot.TransitionTo(0);
    }

    public void Win()
    {
        WinSnapshot.TransitionTo(0);
    }
}
