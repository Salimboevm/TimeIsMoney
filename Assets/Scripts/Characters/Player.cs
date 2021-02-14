//Author: Mokhirbek Salimboev
//Student ID: 1919019

using System.Collections;
using UnityEngine;
using TMPro;

public class Player : Move
{
    
    public CapsuleCollider playerCollider; // player collider to check ground
    public LayerMask layer; // to check layermask to walk and jump
    
    [Header("Audio")]
    //Audiovisual variables
    public AudioClip jumpClip;
    
    //temp coin
    private int tmpCoin = 0;

    [Header("UI")]
    [SerializeField]
    private UIManager uiManager;
    public GameObject gameOver;

    public ParticleSystem coinSys;
    float particleTimer = 0f;
    public float maxParticleTime = 0.25f;
    bool countingParticleTimer;

    bool jump = false;
    float jumpTime = 2.5f;

    public AfterImage ai;

    private void Update()
    {
        if (IsGround())
        {
            //sitting player
            if (IsGround() && Input.GetKey(KeyCode.S))
            {
                playerBody.transform.localScale = new Vector3(playerBody.transform.localScale.x, 0.6f, playerBody.transform.localScale.z);//change player`s y scale
                playerBody.transform.localPosition = new Vector3(transform.position.x, 1f, transform.position.z);//change player`s y position
            }
            //resizing and standing player
            else if (IsGround() && Input.GetKeyUp(KeyCode.S))
            {
                playerBody.transform.localScale = new Vector3(playerBody.transform.localScale.x, 1f, playerBody.transform.localScale.z);//reset 
            }

            //movement to 2 sides   
            if ((Input.GetKeyDown(KeyCode.A)) || Input.GetKeyDown(KeyCode.D))
            {
                ChangeLine();
            }

        }
        else if(transform.position.y < 1.25f) GetComponent<Rigidbody>().AddForce(Vector3.down * 100);

        if(gameObject.transform.position.x > 2)
            gameObject.transform.position = new Vector3(2f, transform.position.y, transform.position.z);
        else if(gameObject.transform.rotation.x < -2)
            gameObject.transform.position = new Vector3(-2f, transform.position.y, transform.position.z);
    }
    /// <summary>
    /// function for player movement to sides
    /// </summary>
    void ChangeLine()
    {
        //Disable afterImage.
        ai.gameObject.SetActive(false);
        //check for keycode`s
        //make player move to 2 sides
        if (Input.GetKeyDown(KeyCode.A))//checking for left side
        {
            if (IsGround())
            {
                if (gameObject.transform.position.x > -2)//if player is inside the line
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - 2f, gameObject.transform.position.y, gameObject.transform.position.z);//change line to new one
                    ai.transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                    ai.gameObject.SetActive(true);
                }
                else return;
            }
            else //if not
                return;//do not do any action
        }
        if (Input.GetKeyDown(KeyCode.D))//checking for right side
        {
            if (IsGround())
            {
                if (gameObject.transform.position.x < 2)//if player is inside the line
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + 2f, gameObject.transform.position.y, gameObject.transform.position.z);//change line to new one
                    ai.transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
                    ai.gameObject.SetActive(true);
                }
                else return;
            }
            else//if not
                return;//do not do any action
        }
    }

    /// <summary>
    /// Call a game over from another function.
    /// </summary>
    public void GameOver()
    {
        //Create a game over.
        if (Time.timeScale == 1f) Time.timeScale = 0f;
        gameOver.SetActive(true);
        GameManager.Instance.GameOver(tmpCoin);
    }

    private void FixedUpdate()
    {
        
        //player jump with adding force
        if (Input.GetKey(KeyCode.W) && !jump)//check for jumping
        {
            if (IsGround())
            {
                StartCoroutine(Jumping());//call a Jumping function
              
                //Play audio
                aSource.Stop();
                aSource.PlayOneShot(jumpClip);
            }
        }
        else
        {
            if (IsGround())//check for ground
                StartCoroutine(Movement(new Vector3(0, 0, (playerBody.velocity.z + moveTime) / inverseMovement * Time.fixedDeltaTime)));//move player
        }
        if(transform.position.y < 0f)
        {
            gameOver.SetActive(true);
            GameManager.Instance.GameOver(tmpCoin);
        }

        //Particles
        if (countingParticleTimer)
        {
            //If the particle system is running, increase the timer.
            particleTimer += Time.deltaTime;
            if(particleTimer >= maxParticleTime)
            {
                //If the timer reaches its limit, disable the timer system.
                coinSys.gameObject.SetActive(false);
                countingParticleTimer = false;
            }
        }
    }
    /// <summary>
    /// call this function when player pressed jump key
    /// </summary>
    /// <returns></returns>
    IEnumerator Jumping()
    {   
        float originalHeight = transform.position.y;//original y position of player
        float maxHeight = originalHeight + jumpTime;//maximum jump height
        playerBody.useGravity = false;//turn off gravity
        
        jump = true;//change value of jump
        yield return null;
        
        while(transform.position.y < maxHeight)//increase y position of player
        {
            transform.position += transform.up * Time.deltaTime * -Physics.gravity.y *(moveTime * 0.016f); //jumpSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        while (transform.position.y > originalHeight)//decrease y position
        {
            transform.position -= transform.up * Time.deltaTime * -Physics.gravity.y * (moveTime * 0.012f);
            yield return null;
        }
        playerBody.useGravity = true;//turn on gravity
        
        jump = false;//change value of jump
        yield return null;//exit coroutine
    }
     /// <summary>
     /// check ground 
     /// return raycast
     /// </summary>
     /// <returns></returns>
    private bool IsGround()
    {
        //check player collider and return raycast
        return Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y +0.3f, layer);
    }
    /// <summary>
    /// checks what is entering to player
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //coin layer check
        if(other.gameObject.layer == 10)
        {
            if (!other.GetComponent<Coin>().lerping)
            {
                //coin layer increase gm coins
                GameManager.Instance.coin++;

                //increase temp coin
                tmpCoin++;
                //update coins and time
                uiManager.CoinsUpdate(tmpCoin);
                //Turn on particle system.
                coinSys.gameObject.SetActive(true);
                if (!coinSys.isPlaying) coinSys.Play();
                particleTimer = 0f;
                countingParticleTimer = true;
            }
            //destroy coin
            other.GetComponent<Coin>().StartLerp();
        }
        //game over layer check
        if (other.gameObject.layer == 9)
        {
            //call game over function from gm
            GameManager.Instance.GameOver(tmpCoin);
            gameOver.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            //call game over function from gm
            GameManager.Instance.GameOver(tmpCoin);
            gameOver.SetActive(true);
        }
    }
}
