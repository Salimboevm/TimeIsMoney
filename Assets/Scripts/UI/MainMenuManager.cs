//Last Edited - 10/11/20
//Author - Aidan McHugh, Mokhirbek Salimboev

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    //Two parts of the Main Menu scene's UI
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject scoresMenu;
    [Header("High Score Parts")]
    //Reference to high score sections
    public TextMeshProUGUI highScores;
    public TextMeshProUGUI totalScores;

    int tempZero = 0;//to initialise default highscore

    /// <summary>
    /// Open the Main Menu section of the main menu scene
    /// </summary>
    public void OpenMainMenu()
    {
        //Enable the main menu and disable the scores menu
        mainMenu.SetActive(true);
        scoresMenu.SetActive(false);
    }

    /// <summary>
    /// Open the Scores section of the main menu scene
    /// </summary>
    public void OpenScoresMenu()
    {
        //DEBUG - THIS IS WHERE THE HIGH SCORES WILL BE UPDATED.
        //Get a reference to the public high scores of the scenemanager
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            highScores.text = SaveSystem.Load().coin.ToString();
        }
        else highScores.text = tempZero.ToString();
        //Disable the main menu and enable the scores menu


        mainMenu.SetActive(false);
        scoresMenu.SetActive(true);
    }

   
}
