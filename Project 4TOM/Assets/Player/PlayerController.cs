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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Facedirection();
        Jump();
        CheckGround();
        SwitchAnimation();
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
}
