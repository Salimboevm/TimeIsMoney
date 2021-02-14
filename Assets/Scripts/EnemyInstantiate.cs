using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiate : MonoBehaviour
{
    public GameObject enemy;//enemy prefab
    public Transform lastBlokPosition;//distance between 2 enemy prefabs
    public Transform enemyParent;//parent transform to make hierarchy clear

    public float blockRoadRange;//range of road to insantiate enemy prefab
    private float distanceFromBlock = 20;//distance from player

    private Player player;//player to take distance

    private void Start()
    {
        player = FindObjectOfType<Player>();//finding player from hierarchy
    }
    /// <summary>
    /// function to randomly spawn enemy prefabs
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyRandomer()
    {
        //wait
        yield return new WaitForSeconds(Random.Range(1f,3f) * Time.deltaTime);
        //endless loop
        while (true)
        {
            if (Pooling.Instance.difficulty >= 3)//check difficulty level
            {
                //if high enough
                float player_z = player.transform.position.z;//player`s z position
                if (lastBlokPosition.position.z - player_z < distanceFromBlock)//check distance form player
                {
                    //take spawning position into one variable
                    Vector3 br = new Vector3(Random.Range(-blockRoadRange, blockRoadRange), 1f, lastBlokPosition.position.z + distanceFromBlock + Random.Range(10, 20));
                    lastBlokPosition = Instantiate(enemy, br, Quaternion.Euler(0, 0, 0), enemyParent).transform;//spawn enemy
                }
                yield return null;//exit from loop
            }
            else//if difficulty is less
                yield return null;//exit 
        }
      
    }
    private void OnEnable()
    {
        StartCoroutine(EnemyRandomer());//start EnemyRandomer coroutine
    }
}
