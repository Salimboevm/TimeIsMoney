using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //Level variables
    public Transform levelToSpawn;
    public Transform startLevel;
    //Pool object
    public Pooling pool;
    //Player
    public GameObject player;
    //Level variables
    public GameObject[] levels;
    public GameObject currentLevel;
    //Level spawnpoint location
    public Vector3 levelSpawn;
    


    private void Awake()
    {
        //Finds the place to spawn the level
        levelSpawn = startLevel.Find("SpawnPos").position;
        //Gets access to the pool of level parts so the level spawned can use them
        levelToSpawn.GetComponent<LevelSection>().pool = pool;
        //Spawns three levels ahead of the player so they can't see the end
        SpawnLevel();
        SpawnLevel();
        SpawnLevel();
    }


    private void Update()
    {
        //Gets access to all levels in scene
        levels = GameObject.FindGameObjectsWithTag("Level");
    }


    private void SpawnLevel()
    {
        //Runs Transform SpawnLevel function
        Transform lastLevelTransform = SpawnLevel(levelSpawn);
        //Sets current level spawnpoint location
        levelSpawn = lastLevelTransform.Find("SpawnPos").position;
    }


    private Transform SpawnLevel(Vector3 spawnPosition)
    {
        //Spawns the level at the spawnpoint location
        Transform levelTransform = Instantiate(levelToSpawn, spawnPosition, Quaternion.identity);
        return levelTransform;
    }


    private void OnTriggerEnter(Collider other)
    {
        //Detects if the trigger is a start/end point collider
        if(other.gameObject.tag == "LevelCollider")
        {
            //Checks if the collider is an end point
            if (other.GetComponent<StartEnd>().isEnd == true)
            {
                //Runs SetCurrentLevel function, 
                //giving it the parent of the collider,
                //which is the level
                SetCurrentLevel(other.transform.parent.gameObject);
                //Runs SpawnLevel function
                SpawnLevel();
            }
            //Checks if the collider is a start point
            if (other.GetComponent<StartEnd>().isEnd != true)
            {
                //Runs the LevelDestroy function
                LevelDestroy();
            }
        }
    }


    private void SetCurrentLevel(GameObject level)
    {
        //Sets the currentLevel variable to the level given by the collider
        currentLevel = level;
    }


    private void LevelDestroy()
    {
        //Checks to see if the first level in the levels array is not the current level
        if (levels[0] != currentLevel)
        {
            //Destroys the first level in the array, 
            //which should be behind the player as it was added first
            Destroy(levels[0]);
        }
    }
}
