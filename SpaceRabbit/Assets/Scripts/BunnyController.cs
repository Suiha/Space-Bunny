using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    Rigidbody2D bunny;
    private Animator anim;

    public float jumpPower, speed;
    Vector2 force = new Vector2(0, 0);
    Vector2 rAcceleration = new Vector2(0, 0);

    private Vector2 screenBounds;
    private float objectWidth;

    private bool bGrounded;

    // Start is called before the first frame update
    void Start()
    {
        bunny = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        objectWidth = bunny.GetComponent<SpriteRenderer>().bounds.size.x / 16;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // prevent rabbit from moving out of horizontal bounds
        Vector3 viewPos = bunny.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 - objectWidth, screenBounds.x + objectWidth);
        bunny.position = viewPos;

        // jumping
        if ((Input.GetKey("w") || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && bGrounded)
        {
            bunny.velocity = new Vector2 (bunny.velocity.x, (transform.up * jumpPower).y);
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            bunny.velocity = new Vector2(-speed, bunny.velocity.y);
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            bunny.velocity = new Vector2(speed, bunny.velocity.y);
        }

        // keep bunny within screen bounds
        if (bunny.position.x <= (-Screen.width / 2) || bunny.position.x >= (Screen.width / 2))
        {
            bunny.velocity = new Vector2(0, 0);
        }
    }

    // on collision = rabbit on ground
    void OnCollisionEnter2D(Collision2D col)
    {
        bGrounded = true;
        anim.SetBool("jumping", false);
    }

    // no collision = rabbit in air
    void OnCollisionExit2D(Collision2D col)
    {
        bGrounded = false;
        anim.SetBool("jumping", true);
    }
}
