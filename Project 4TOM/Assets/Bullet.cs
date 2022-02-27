using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask collisionMask;
    public float speed = 20f;
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] int maxBounceNum = 3;
    [SerializeField] GameObject playerChar;
    [SerializeField] float bulletLifetimeLimit = 5f;

    Vector3 lastVelocity;
    float bulletLifetime = 0f;

    //int bounceNum = 0; 

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * -speed; //speed is given to bullet when initiating bullet in Skill_1_Ricochet script
    }

    private void Update()
    {
        lastVelocity = rb.velocity; //keep updating direction even after bouncing with another platform
        Physics2D.IgnoreLayerCollision(3, 7); //bullet ignore player layer

        if (bulletLifetime <= bulletLifetimeLimit)
        {
            bulletLifetime += Time.deltaTime;
        }
        else //if bullet spends more than bulletLifetimeLimit on screen
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal); //reflect to bounce against platform

            rb.velocity = direction * Mathf.Max(speed, 0f); //change rigidbody direction if contact with another platform

            //bounceNum++;

            //if (bounceNum >= maxBounceNum)
            //{
            //    Destroy(gameObject);
            //}
        }
    }
}
