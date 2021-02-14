//Last Edited - 07/10/20
//Author - Aidan McHugh
//SID - 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    
    /// <summary>
    /// Load a scene in the build.
    /// </summary>
    /// <param name="sceneNum">the integer associated with the scene.</param>
    public void LoadScene(int sceneNum)
    {
        //Load the desired scene
        SceneManager.LoadScene(sceneNum);
    }
}
