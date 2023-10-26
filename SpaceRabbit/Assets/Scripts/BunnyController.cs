using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour
{
    Rigidbody2D bunny;
    private Animator anim;

    public int maxHealth = 3;
    public int currentHealth;
    public float jumpPower, speed;

    private Vector2 screenBounds;
    private float objectWidth;

    private bool bGrounded;

    // Start is called before the first frame update
    void Start()
    {
        bunny = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        objectWidth = bunny.GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // prevent rabbit from moving out of horizontal bounds
        Vector3 newPos = bunny.position;
        newPos.x = Mathf.Clamp(newPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        bunny.position = newPos;

        // jumping
        if ((Input.GetKey("w") || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && bGrounded)
        {
            bunny.velocity = new Vector2(bunny.velocity.x, jumpPower);
            anim.SetBool("jumping", true);
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            bunny.velocity = new Vector2(-speed, bunny.velocity.y);
            if (!anim.GetBool("jumping")) anim.SetBool("moving", true);
        }
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            bunny.velocity = new Vector2(speed, bunny.velocity.y);
            if (!anim.GetBool("jumping")) anim.SetBool("moving", true);
        }

        // stop moving animation
        if (bunny.velocity.x == 0)
        {
            anim.SetBool("moving", false);
        }

    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        // bunny can only jump if in contact with a platform
        if (obj.gameObject.tag == "platform")
        {
            bGrounded = true;
            anim.SetBool("jumping", false);
        } else if (obj.gameObject.tag == "enemy")
        {
            TakeDamage(1);
        }
    }

    void OnCollisionExit2D(Collision2D obj)
    {
        // no collision = rabbit in air
        bGrounded = false;
    }

    void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
