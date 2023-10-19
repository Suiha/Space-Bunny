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

    private Vector2 screenBounds;
    private float objectWidth;

    // Start is called before the first frame update
    void Start()
    {
        rabbit = GetComponent<Rigidbody2D>(); 
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        objectWidth = rabbit.GetComponent<SpriteRenderer>().bounds.size.x / 16;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // prevent rabbit from moving out of horizontal bounds
        Vector3 viewPos = rabbit.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 - objectWidth, screenBounds.x + objectWidth);
        rabbit.position = viewPos;

        // movement keys
        if ((Input.GetKey("w") || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && bGrounded)
        {
            rabbit.velocity = transform.up * jump;
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rabbit.velocity += new Vector2(-speed, 0);
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rabbit.velocity += new Vector2(speed, 0);
        }

        if (rabbit.position.x <= (-Screen.width / 2) || rabbit.position.x >= (Screen.width / 2))
        {
            rabbit.velocity = new Vector2(0, 0);
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
