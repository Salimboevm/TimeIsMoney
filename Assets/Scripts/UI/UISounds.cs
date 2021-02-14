//Last Edited: 07/11/2020
//Author: Aidan McHugh
//SID: 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [Header("Button Sounds")]
    AudioSource aSource;
    public AudioClip hover;
    public AudioClip click;

    /// <summary>
    /// Call at the start of the program.
    /// </summary>
    void Start()
    {
        //Get attached AudioSource
        aSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Play the clicking sound.
    /// </summary>
    /// <param name="f">the pitch of the sound. Usually 1 for mouse down, 0.75f for mouse up.</param>
    public void PlayClick(float f)
    {
        //Stop any sound(s) playing, set the pitch and play the sound.
        aSource.Stop();
        aSource.pitch = f;
        aSource.PlayOneShot(click);
    }

    /// <summary>
    /// Play the hovering sound.
    /// </summary>
    public void PlayHover()
    {
        //Set the pitch and play the sound.
        aSource.pitch = 1f;
        aSource.PlayOneShot(hover);
    }

    
}
