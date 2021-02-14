//Last Edited: 10/11/20
//Author: Aidan McHugh
//SID: 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialEnum : MonoBehaviour
{
    Animator anim;

    /// <summary>
    /// Call on first frame
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();
        //Play the cutscene based on if it's been seen before.
        if (GameManager.Instance.playTutorial)
        {
            //If the tutorial has not been seen before, play it
            GameManager.Instance.playTutorial = false;
            StartCoroutine(tutAnim());
        }
        else
        {
            //Else load up the main scene.
            anim.enabled = false;
            anim.gameObject.SetActive(false);
            SceneManager.LoadScene(1);
        }

    }

    /// <summary>
    /// A coroutine to run while the tutorial animation is running.
    /// </summary>
    /// <returns>Technically nothing.</returns>
    IEnumerator tutAnim()
    {
        //Wait for five seconds, then load the game.
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
