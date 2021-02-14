//Author: Mokhirbek Salimboev
//Student ID: 1919019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Move
{
    //Camera
    GameObject camRef;

    /// <summary>
    /// Call when enabled.
    /// </summary>
    private void OnEnable()
    {
        //Find Camera
        camRef = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void FixedUpdate()
    {
        //enemy moving
        if(Pooling.Instance.difficulty >= 3)
            StartCoroutine(Movement(new Vector3(0, 0, -((playerBody.velocity.z + moveTime) / inverseMovement * Time.fixedDeltaTime))));
        //If behind the camera, destroy
        if (transform.position.z < camRef.transform.position.z) Destroy(gameObject);
    }
}
