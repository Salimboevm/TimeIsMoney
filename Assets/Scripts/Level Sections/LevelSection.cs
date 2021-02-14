//Last Edited (DD/MM/YY) - 03/11/20
//Author - Aidan McHugh
//SID - 1806867

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour
{
    //A Vector3 of various positions the prefab sections can spawn at.
    public Vector3[] tilePos;
    //The pool of prefab sections to choose from.
    public Pooling pool;
    //Blank piece in case of emergency
    public GameObject emergency;

    //Simple identifier for different level sections.
    //A publicly settable int for quick reference - this way, the level section can be deduced through code.
    public int sectionNumber;
    //Code reference to Start/End Colliders
    public StartEnd[] triggers;
    //Create an assignment of new tiles.
    public GameObject[] newTiles = new GameObject[3];
    //Coins
    public Coin[] coins = new Coin[7];

    /// <summary>
    /// Call as soon as awake
    /// </summary>
    private void Awake()
    {
        //Create an array for the start and end triggers in the level section. 0 is the start, 1 is the end.
        triggers = GetComponentsInChildren<StartEnd>();
    }

    /// <summary>
    /// Call when the object is enabled.
    /// </summary>
    private void OnEnable()
    {
        //Reset newTiles
        newTiles = new GameObject[3];

        //Manually set the tilePos positions.
        tilePos[0] = new Vector3(transform.localPosition.x - 2, transform.localPosition.y, transform.localPosition.z);
        tilePos[1] = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);
        tilePos[2] = new Vector3(transform.localPosition.x + 2, transform.localPosition.y, transform.localPosition.z);
        //Do a for loop of blankParts.
        for(int i = 0; i < pool.blankParts.Length; i++)
        {
            //Go through each blank part to find an inactive one
            if (!pool.blankParts[i].gameObject.activeSelf)
            {
                //If a part is inactive, set this one to enabled and assign it to [0].
                pool.blankParts[i].gameObject.SetActive(true);
                newTiles[0] = pool.blankParts[i].gameObject;
                break;
            }
        }
        //Do the same for regular parts.
        for(int i = 1; i < 3; i++)
        {
            //Set [1] and [2] by randomly selecting a piece.
            bool set = false;
            int strikes = 0;
            do
            {
                //Check for a subsection in a random position.
                int rand = Random.Range(0,pool.tileSelection.Length - 1);
                //Go through the tileSelection array.
                if (pool.difficulty >= pool.tileSelection[rand].difficulty && !pool.tileSelection[rand].gameObject.activeSelf)
                {
                    //If there is an inactive object, set it to true and assign it to [i]. THIS WILL CHANGE.
                    pool.tileSelection[rand].gameObject.SetActive(true);
                    newTiles[i] = pool.tileSelection[rand].gameObject;
                    set = true;
                    break;
                }
                //If it is taken up, add to strikes. 
                else strikes++;
                if(strikes == 9)
                {
                    //If enough strikes have occurred, simply instantiate the level section.
                    GameObject newObj = Instantiate(emergency, transform.position, Quaternion.identity);
                    //Set it to be in pooling - NOTE: DOES NOT ADD TO POOLING ARRAY.
                    newObj.transform.parent = pool.gameObject.transform;
                    //Enable the object and break.
                    newObj.SetActive(true);
                    newTiles[i] = newObj;
                    set = true;
                    break;
                }

            } while (!set);
        }

        //Randomise the newTiles.
        for(int i = 2; i > -1; i--)
        {
            //Do an inverse for loop so that each section is randomised.
            //Create a random integer ranging from 0 to i.
            int rnd = Random.Range(0,i);
            //Create a random bool to decide if switching should take place.
            bool switchr = Random.value < 0.5f;
            if (switchr)
            {
                //If true, switch position [i] and [rnd] in tilePos.
                Vector3 temp = tilePos[rnd];
                tilePos[rnd] = tilePos[i];
                tilePos[i] = temp;
            }
            //Reorganise the position
            newTiles[i].transform.position = tilePos[i];
        }

        //Set where the coins will spawn.
        bool coinSet = false;
        do
        {
            //Choose a free subsection.
            int rnd = Random.Range(0, 2);
            if(newTiles[rnd].GetComponent<Collider>() != null)
            {
                //If the collider exists, check if there is a block to be jumped over.
                bool blockThere = false;
                if(newTiles[rnd].transform.childCount > 0)
                {
                    //If there is a child attached to the subsection, check its y position
                    if(newTiles[rnd].transform.GetChild(0).transform.position.y == 1f)
                    {
                        //If it is 1.5f and can be jumped over, set blockThere to true
                        blockThere = true;
                    }
                }

                //Do a for loop.
                for(int i = 0; i < coins.Length; i++)
                {
                    //Activate each coin and place it on the free subsection, creating an assumed vector3 along the subsection.
                    coins[i].gameObject.SetActive(true);
                    Vector3 chosenV3 = new Vector3(newTiles[rnd].transform.localPosition.x, 1, (newTiles[rnd].transform.position.z + (i * 2) - 7));
                    if (blockThere)
                    {
                        //If there is a jumpable block on the subsection, get a float of the distance between coin i and the block.
                        float estimatedZ = Mathf.Abs((newTiles[rnd].transform.position.z + (i * 2) - 7) - newTiles[rnd].transform.GetChild(0).transform.position.z);
                        if (estimatedZ <= 2.5f)
                        {
                            //If the float is within range, use cos() to create a jump arc.
                            if(estimatedZ > 0) estimatedZ /= 2.5f;
                            chosenV3 = new Vector3(newTiles[rnd].transform.localPosition.x, 1 + (2.5f * Mathf.Cos(estimatedZ)), (newTiles[rnd].transform.position.z + (i * 2) - 7));
                        }
                    }
                    //Set the chosen vector3 position.
                    coins[i].transform.position = chosenV3;
                }
                //Set coinSet to true and break out.
                coinSet = true;
                break;
            }
        } while (!coinSet);
    }

    /// <summary>
    /// Perform when the level section is disabled.
    /// </summary>
    private void OnDestroy()
    {
        //Go through newTiles
        foreach(GameObject go in newTiles)
        {
            //Disable each gameObject in newTiles.
            go.SetActive(false);
        }
        foreach(Coin c in coins)
        {
            //Do the same for coins
            c.gameObject.SetActive(false);
        }
    }

}
