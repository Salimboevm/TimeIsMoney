//Last Edited (DD/MM/YY) - 06/11/20
//Author - Aidan McHugh
//SID - 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    [Header("Difficulty Variables")]
    public int difficulty = 1;
    public float timer = 0f;
    public float difficultyTimeIncrease = 5f;
    
    [Header("Subsection Parts")]
    public Subsection[] tileSelection;
    //Array of white sections that are completely empty, this way there is always a clear route.
    public Subsection[] blankParts;

    [Header("Singleton Management")]
    private static Pooling _instance;
    public static Pooling Instance { get { return _instance; } }
    /// <summary>
    /// Call before the game is run.
    /// </summary>
    private void Awake()
    {
        //Check for other instances
        if (_instance != null && _instance != this)
        {
            //If there is another instance and it isn't this gameObject, delete this instance
            Destroy(this.gameObject);
        }
        else
        {
            //If there are no other PlayerControllers, set the instance reference to this instance.
            _instance = this;
        }
        //Reset timer
        timer = 0;
    }

    /// <summary>
    /// Update every frame.
    /// </summary>
    private void Update()
    {
        //Increase the timer
        timer += Time.deltaTime;
        if (timer >= difficultyTimeIncrease && difficulty < 3)
        {
            //If the timer hits the increase, increase the difficulty and reset the timer.
            difficulty++;
            timer = 0;
        }
    }
}
