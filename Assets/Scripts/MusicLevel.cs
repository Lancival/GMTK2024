using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLevel : MonoBehaviour
{
    public AudioSource[] audioSourceArray;
    public AudioClip[] audioClipArray;
    double nextStartTime;
    int nextClip = 0;
    int toggle = 0;

    void Start()
    {
        nextStartTime = AudioSettings.dspTime + 0.2;
    }

    void Update () 
    {
        // half a second before the actual event time
        if(AudioSettings.dspTime > nextStartTime - 0.5) {
        AudioClip clipToPlay = audioClipArray[nextClip];
        // Loads the next Clip to play and schedules when it will start
        audioSourceArray[toggle].clip = clipToPlay;
        audioSourceArray[toggle].PlayScheduled(nextStartTime);

        nextStartTime += 108.00;
        // Switches the toggle to use the other Audio Source next
        toggle = 1 - toggle;
        // Increase the clip index number, reset if it runs out of clips
        nextClip = nextClip < audioClipArray.Length - 1 ? nextClip + 1 : 0;
        }
    }
}
