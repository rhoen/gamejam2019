using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{


    public AudioMixerSnapshot FirstItemCollected;
    public static AudioManager Instance { private set; get; }
    // Start is called before the first frame update
    void Start()
    {
        FirstItemCollected.TransitionTo(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
