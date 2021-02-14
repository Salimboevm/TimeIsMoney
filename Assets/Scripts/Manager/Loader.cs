//Last edited: 07/11/20
//Author: Aidan McHugh
//SID: 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public Animator transitioner;

    /// <summary>
    /// Call at start of level.
    /// </summary>
    public void Start()
    {
        //If the timescale is not reset, set the timescale to 1.
        if (Time.timeScale <= 0f) Time.timeScale = 1f;
    }

    /// <summary>
    /// Call the scene loading enum.
    /// </summary>
    /// <param name="sceneNum"></param>
    public void LoadScene(int sceneNum)
    {
        //Call the enum.
        StartCoroutine(LSEnum(sceneNum));
    }

    /// <summary>
    /// Load the desired scene.
    /// </summary>
    /// <param name="sceneNum">The integer of the scene in question (see build).</param>
    /// <returns>Technically nothing but waiting.</returns>
    IEnumerator LSEnum(int sceneNum)
    {
        if (Time.timeScale == 0) SceneManager.LoadScene(sceneNum);
        else
        {
            transitioner.SetTrigger("end");
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(sceneNum);
        }
    }
}
