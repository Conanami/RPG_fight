using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio 
{
    private AudioSource audioSource;
    
    public Player_Audio(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
