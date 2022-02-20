using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;

    private int move02;

    private Rigidbody2D rb;
    private BoxCollider2D myFeet;
    private bool isGround;

    private Animator anim;

    // parameters for bomb mechanics
    public bool isBombOnPlayer;
    public bool bombCanPass;
    public float bombPassCooldownTimer;
    public float bombTimerCooldown;
    public GameObject bombImage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        bombImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Facedirection();
        Jump();
        CheckGround();
        SwitchAnimation();
        BombChecker();
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

        if (collision.gameObject.CompareTag("Player") && bombCanPass == true) // check if player can pass the bomb to others
        {
            PlayerController collidedPlayer = collision.gameObject.GetComponent<PlayerController>();
            if (collidedPlayer != null && collidedPlayer.isBombOnPlayer == false)
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
            // runSpeed = // set run speed after obtaining bomb 
            bombTimerCooldown = bombTimerCooldown - Time.deltaTime;
            if (bombTimerCooldown <= 0.0f)
            {
                bombCanPass = true;
            }
        }
        else // if the bomb is not on the player
        {
            bombTimerCooldown = 1.0f;
            bombImage.SetActive(false);
            isBombOnPlayer = false;
            bombCanPass = false;
        }
    }
}
