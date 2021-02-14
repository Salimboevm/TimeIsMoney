//Last edited - 09/10/20
//Author - Aidan McHugh
//SID - 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideSound : MonoBehaviour
{
    //Object collider
    Collider trigger;
    AudioSource aSource;
    //Audioclip
    public AudioClip clip;

    /// <summary>
    /// Call at the start of the program
    /// </summary>
    void Start()
    {
        //Get various components attached to the gameObject.
        trigger = GetComponent<Collider>();
        aSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Call when another gameObject collides with this gameObject
    /// </summary>
    /// <param name="other">The collider of the other object</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check other's tag
        if(other.gameObject.tag == "Player")
        {
            //If it's the player, play the sound attached to this object.
            aSource.PlayOneShot(clip);
        }
    }
}
