using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask collisionMask;
    [SerializeField] float speed = 20f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int maxBounceNum = 3;
    [SerializeField] GameObject playerChar;

    Vector3 lastVelocity;

    int bounceNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * -speed;
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
        Physics2D.IgnoreLayerCollision(3, 7);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
            bounceNum++;
            
            if (bounceNum >= maxBounceNum)
            {
                Destroy(gameObject);
            }
        }
    }
}
