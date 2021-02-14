//Author: Mokhirbek Salimboev
//Student ID: 1919019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player player;
 
    //last position of player
    private Vector3 lastPlayerPos;
    //camera moving distance from player
    private float distanceToMove;

    void Start()
    {
        player = FindObjectOfType<Player>();
        lastPlayerPos = player.transform.position;
    }

    void Update()
    {
        distanceToMove = player.transform.position.z - lastPlayerPos.z;//camera moving distance

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distanceToMove);//move camera

        lastPlayerPos = player.transform.position;//update last player position
    }
}
