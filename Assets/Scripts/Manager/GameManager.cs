//Last Edited - 07/11/20
//Authors - Aidan McHugh, Mokhirbek Salimboev

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool playTutorial = true;

    //singleton
    #region
    private static GameManager _instance;//instance to set
    /// <summary>
    /// get instance 
    /// </summary>
    public static GameManager Instance
    {
        get
        {   
            return _instance;
        }
    }
    /// <summary>
    /// set instance
    /// </summary>
    private void Awake()
    {
        //check if it is not null or equal to other destroy
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        //if not set it and do not destroy on load of screen
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    //coin
    [HideInInspector]
    public int coin = 0;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Data playerData = SaveSystem.Load();
            coin = playerData.coin;
        }
        else
        {
            SaveSystem.Save(this);
        }
    }
    //call it when player loses
    public void GameOver(int coinCount)
    {
        Debug.Log(coinCount);
        //activate game over window
        if(coin > SaveSystem.Load().coin)
        {
            //If high enough score, save it
            coin = coinCount;
            SaveSystem.Save(this);
        }
        Time.timeScale = 0;//stop time
    }
}
