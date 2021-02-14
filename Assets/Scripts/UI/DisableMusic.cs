//Last edited: 05/12/20
//Author: Aidan McHugh
//SID: 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMusic : MonoBehaviour
{
    //Get the music source
    public AudioSource bgmSource;

    /// <summary>
    /// Call upon enabling the attached GameObject
    /// </summary>
    private void OnEnable()
    {
        //Stop the looping music.
        bgmSource.Stop();
    }
}
