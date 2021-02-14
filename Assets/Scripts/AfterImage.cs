//Author: Aidan McHugh
//Last Edited: 01/12/20
//SID: 1806867  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    //Necessary variables
    bool isLeft;
    float timer = 0.2f;

    /// <summary>
    /// Call when the gameObject is enabled.
    /// </summary>
    private void OnEnable()
    {
        //Set isLeft based on position in comparison to player
        if (transform.localPosition.x < 0) isLeft = true;
        else isLeft = false;
        //Reset timer
        timer = 0.2f;
    }

    /// <summary>
    /// Call once per frame
    /// </summary>
    void Update()
    {
        //Set position based on isLeft and time left.
        if (isLeft) transform.localPosition = new Vector3(-timer * 10, 0, 0);
        else transform.localPosition = new Vector3(timer * 10, 0, 0);
        //Decrement timer until 0, then disable.
        timer -= Time.deltaTime;
        if (timer <= 0) gameObject.SetActive(false);
    }

}
