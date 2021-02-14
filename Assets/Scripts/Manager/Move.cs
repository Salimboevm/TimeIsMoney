//Author: Mokhirbek Salimboev
//Student ID: 1919019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{
    //move time to character movement 
    public float moveTime = 0.1f;
    //move through character rigidbody
    protected Rigidbody playerBody;
    protected float jumpForce = 0;
    //makes more afficient calculations
    protected float inverseMovement;
    //Audio source
    public AudioSource aSource = null;

    protected virtual void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        inverseMovement = 1f / moveTime;
        if(GetComponent<AudioSource>() != null)
        {
            //If there is an audiosource, set it.
            aSource = GetComponent<AudioSource>();
        }
    }
    /// <summary>
    /// move player with velocity 
    /// and increase it by time
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Movement(Vector3 side)
    {
        while (true)//endless loop 
        {
            playerBody.velocity = side;//move player with adding velocity
            moveTime += 0.00005f * Time.deltaTime;//increase movement speed
            yield return null;//return zero to not crash and exit from loop
        }
    }
    

}
