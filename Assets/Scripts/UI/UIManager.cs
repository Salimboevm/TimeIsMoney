//Last edited - 07/11/20
//Author - Aidan McHugh
//SID - 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text timerUI;
    public GameObject pause;
    public Player player;

    public float timer = 20f;

    public Text scoreTextUI;

    /// <summary>
    /// Call once per frame
    /// </summary>
    void Update()
    {
        //Manage the decreasing timer
        timer -= Time.deltaTime;

        if (timer < 5f)
        {
            //Less than 5 seconds are remaining, set the colour to an increasing red.
            timerUI.color = Color.Lerp(Color.red, Color.white, timer / 5);
        }
        else
        {
            //There is still time, set to the colour white.
            timerUI.color = Color.white;
        }
        //If the timer is = 0, game over
        if (timer <= 0) player.GameOver();
        else
        {
            timerUI.text = timer.ToString("#.00") + "s";
        }

        //Escape key input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //If the player presses escape, pause the game.
            Pause();
        }
    }

    /// <summary>
    /// Call to pause the game.
    /// </summary>
    void Pause()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
    }

    /// <summary>
    /// Call to resume the game.
    /// </summary>
    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
    }
    public void CoinsUpdate(int coin)
    {
        //update the UI.
        timer += 0.5f;
        scoreTextUI.text = "SCORE: " + coin;
    }
}
