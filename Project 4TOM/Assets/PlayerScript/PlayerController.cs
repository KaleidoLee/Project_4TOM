using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float baseRunSpeed;
    public float runSpeedMultiplierInPercent;
    public float baseHealthPoints; // Initial health points
    public float currentHealthPoints;
    public int baseLives;
    public int currentLives;
    public float healthPointsDecayPerSecond;

    private int move02;

    private Rigidbody2D rb;
    private BoxCollider2D myFeet;
    private bool isGround;

    private Animator anim;

    //parameters for multiplayer
    PhotonView view;
    public int GamePosition = 0;
    public int PlayerNumber; // for detection and bomb transfer

    // parameters for bomb mechanics
    public bool isBombOnPlayer;
    public bool bombCanPass;
    public float bombPassCooldownTimeInSeconds;
    public float bombPassCooldownTimer;
    public GameObject bombImage;
    // Start is called before the first frame update
    void Start()
    {
        //multiplayer stuff (to get the view point form each player)
        view = GetComponent<PhotonView>();


        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        bombImage.SetActive(false);
        currentHealthPoints = baseHealthPoints;
        currentLives = baseLives;
    } 

    // Update is called once per frame
    void Update()
    {
        //multiplayer stuff (if the player is mine)
        if (view.IsMine)
        {
            Run();
            Facedirection();
            Jump();
            CheckGround();
            SwitchAnimation();
            BombChecker();
        }


        //Run();
        //Facedirection();
        //Jump();
        //CheckGround();
        //SwitchAnimation();
        //BombChecker();
    }

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"));
    }


    void Run()
    {
        float move01 = Input.GetAxis("Horizontal");
        if ((!Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D) ||
            (Input.GetKey(KeyCode.A) & Input.GetKey(KeyCode.D))))
            move02 = 0;
        else if (Input.GetKey(KeyCode.A))
            move02 = -1;
        else if (Input.GetKey(KeyCode.D))
            move02 = 1;
        else
            move02 = 0;

        if(isBombOnPlayer == true) // change speed when player has or does not have bomb
        {
            runSpeed = baseRunSpeed * (100 + runSpeedMultiplierInPercent) / 100; // increased run speed by how many % when player has bomb
        }
        else
        {
            runSpeed = baseRunSpeed; // return to base run speed when player does not have bomb
        }
        Vector2 playerVel = new Vector2(move02 * runSpeed, rb.velocity.y);
        rb.velocity = playerVel;
        bool PlayerRunSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("Run", PlayerRunSpeed);
    }


    void Facedirection()
    {
        bool PlayerRunSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (PlayerRunSpeed)
        {
            if (rb.velocity.x < 0.0f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (rb.velocity.x > 0.0f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

        }
    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {

                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                rb.velocity = Vector2.up * jumpVel;
                anim.SetBool("Jump", true);
            }
        }
    }

    void SwitchAnimation()
    {
        anim.SetBool("Idle", false);
        if (anim.GetBool("Jump"))
        {
            if (rb.velocity.y < 0.0f)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            anim.SetBool("Fall", false);
            anim.SetBool("Idle", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // testing obtaining the bomb
        if (collision.gameObject.CompareTag("Bomb"))
        {
            isBombOnPlayer = true;
        }

        if (collision.gameObject.CompareTag("Player") && bombCanPass == true) // check if player collided with another player and if player can pass the bomb to others
        {
            PlayerController collidedPlayer = collision.gameObject.GetComponent<PlayerController>(); // obtaining details of collided player
            if (collidedPlayer != null && collidedPlayer.isBombOnPlayer == false) // if collided player does not have bomb
            {
                collidedPlayer.isBombOnPlayer = true;
                isBombOnPlayer = false;
            }
        }
    }

    void BombChecker() // check to see if the bomb is on the player
    {
        if (isBombOnPlayer == true) // if the bomb is on the player
        {
            bombImage.SetActive(true);
            bombPassCooldownTimer = bombPassCooldownTimer - Time.deltaTime;
            if (bombPassCooldownTimer <= 0.0f)
            {
                bombCanPass = true;
                bombPassCooldownTimer = 0;
            }

            currentHealthPoints = currentHealthPoints - (healthPointsDecayPerSecond * Time.deltaTime);// reduce health points when player has bomb
            if (currentHealthPoints <= 0.0f)
            {
                isBombOnPlayer = false; // bomb no longer on player since it exploded and transferred to other player
                currentLives--; // reduce 1 life

                if (currentLives <= 0)
                {
                    // lose game
                }
                else
                {
                    currentHealthPoints = baseHealthPoints; // respawn with health again 
                    // set respawn location (don't use instantiate cause we need to save the player lives)
                }

                // randomise/transfer bomb to other player (done with another script)
                
            }
        }
        else // if the bomb is not on the player
        {
            bombPassCooldownTimer = bombPassCooldownTimeInSeconds;
            bombImage.SetActive(false);
            isBombOnPlayer = false;
            bombCanPass = false;
        }
    }
}
