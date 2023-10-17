using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    Rigidbody2D rabbit;
    public float jump, speed;
    Vector2 force = new Vector2(0, 0);
    Vector2 rAcceleration = new Vector2(0, 0);
    bool bGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rabbit = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey("w") || Input.GetKey(KeyCode.Space)) && bGrounded)
        {
            rabbit.velocity = transform.up * jump;
        }
        if (Input.GetKey("a") && rabbit.velocity.x == 0)
        {
            rabbit.velocity += new Vector2(-speed, 0);
        }
        if (Input.GetKey("d") && rabbit.velocity.x == 0)
        {
            rabbit.velocity += new Vector2(speed, 0);
        }
    }

    // on collision = rabbit on ground
    void OnCollisionEnter2D(Collision2D col)
    {
        bGrounded = true;
    }

    // no collision = rabbit in air
    void OnCollisionExit2D(Collision2D col)
    {
        bGrounded = false;
    }
}
