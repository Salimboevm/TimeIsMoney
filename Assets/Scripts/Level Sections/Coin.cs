//Last edited - 01/12/20
//Author - Aidan McHugh
//SID - 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Animator
    public Animator anim;
    //Lerp variables
    public bool lerping = false;
    public Vector3 aPoint = new Vector3(0,0,0);
    public Vector3 bPoint = new Vector3(0,0,0);
    GameObject lerpPos;
    private float startTime;
    private float journeyLength;
    //Audio
    AudioSource aSource;
    public AudioClip hitClip;

    /// <summary>
    /// Call at start of the program.
    /// </summary>
    private void Start()
    {
        //Set point A, find the desired lerp position and set it as point B
        aPoint = transform.position;
        lerpPos = GameObject.FindGameObjectWithTag("ScoreUI");
        bPoint = lerpPos.transform.position;
        aSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Update every frame of play.
    /// </summary>
    private void Update()
    {
        //Check if the coin is currently lerping.
        if (lerping)
        {
            //If it is, continue by updating point B and lerp it.
            bPoint = lerpPos.transform.position;
            float distCovered = (Time.time - startTime) * 20;
            float journeyFraction = distCovered / journeyLength;
            transform.position = Vector3.Lerp(aPoint, bPoint, journeyFraction);
            //If it is near to point B, then destroy this object.
            if (Mathf.Approximately(gameObject.transform.position.x, lerpPos.transform.position.x))
            {
                lerping = false;
                gameObject.SetActive(false);
            }
        }
        else if (Vector3.Distance(GameObject.FindGameObjectWithTag("MainCamera").transform.position, transform.position) < 25 && !anim.enabled) anim.enabled = true;
    }

    /// <summary>
    /// Call when the gameobject is enabled.
    /// </summary>
    private void OnEnable()
    {
        //Disable the animator to save framerate.
        anim.enabled = false;
    }

    /// <summary>
    /// Command this object to start lerping to point B.
    /// </summary>
    public void StartLerp()
    {
        //Allow this object to lerp
        lerping = true;
        anim.enabled = false;
        //Set up the lerp process.
        startTime = Time.time;
        journeyLength = Vector3.Distance(aPoint,bPoint);
        //Play a sound
        aSource.Stop();
        aSource.pitch = Random.Range(0.75f, 1.25f);
        aSource.PlayOneShot(hitClip);
    }
}
