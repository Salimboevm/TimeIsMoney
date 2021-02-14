//Last edited - 17/11/20
//Author - Aidan McHugh
//SID - 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTransparency : MonoBehaviour
{
    //Required variables
    MeshRenderer mr;
    Color col;
    GameObject player;
    bool transparenter = false;

    /// <summary>
    /// Call at the start of the program.
    /// </summary>
    void Start()
    {
        //Get the meshRenderer, the color of the hazard's material, and the player.
        mr = GetComponent<MeshRenderer>();
        col = mr.material.color;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Call when enabled.
    /// </summary>
    private void OnEnable()
    {
        //Reset the transparency.
        if(mr != null) mr.material.color = col;
    }

    /// <summary>
    /// Call once every frame
    /// </summary>
    void Update()
    {
        //NOTE - INCOMPLETE
        //Create a float comparing the z positions
        float dist = transform.position.z - player.transform.position.z;
        //If sufficiently transparent turn this off
        if (mr.material.color.a <= 0.2f)
        {
            transparenter = false;
        }
        //If within distance, start making hazard transparent.
        else if (dist <= 4) transparenter = true;

        if (transparenter)
        {
            //If able to make the hazard transparent, do so and apply it to the MeshRenderer.
            Color newCol = new Color(col.r, col.g, col.b, (transform.position.z - player.transform.position.z) / 4);
            mr.material.color = newCol;
        }
    }
}
