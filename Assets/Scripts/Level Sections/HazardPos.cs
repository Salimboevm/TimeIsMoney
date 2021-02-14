//Last edited (DD/MM/YY) - 27/10/20
//Author - Aidan McHugh
//SID - 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardPos : MonoBehaviour
{
    //y position of the object, differentiates between jumping over and ducking under.
    public float yPos;

    /// <summary>
    /// Call when the hazard object is enabled.
    /// </summary>
    private void OnEnable()
    {
        //Get the parent object and move the hazard to a random position on it.
        transform.position = new Vector3(transform.parent.localPosition.x, yPos, transform.parent.localPosition.z + (Random.Range(-6,6)));
    }
}
