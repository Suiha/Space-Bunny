using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour
{
    Rigidbody2D bunny;
    private Animator anim;

    public int maxHealth = 10;
    private int currentHealth;
    public float jumpPower, speed;

    private Vector2 screenBounds;
    private float objectWidth;

    private GameObject platform;
    private bool bGrounded;

    // Start is called before the first frame update
    void Start()
    {
        bunny = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        PlayerPrefs.SetInt("bunnyHealth", currentHealth);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        objectWidth = bunny.GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }


    void FixedUpdate()
    {
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
            if (bGrounded) anim.SetBool("moving", true);
        }
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            bunny.velocity = new Vector2(speed, bunny.velocity.y);
            if (bGrounded) anim.SetBool("moving", true);
        }

        // stop moving animation
        if (bunny.velocity.x == 0)
        {
            anim.SetBool("moving", false);
        }


        // keep track of health from player prefs
        currentHealth = PlayerPrefs.GetInt("bunnyHealth");
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        // bunny can only jump if in contact with a platform
        if (obj.gameObject.tag == "platform")
        {
            platform = obj.gameObject;
            float platWidth = platform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float platHeight = platform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            // check if bunny is above platform and not on the sides
            if (bunny.position.y > platform.transform.position.y + platHeight && (bunny.position.x > platform.transform.position.x - platWidth && bunny.position.x < platform.transform.position.x + platWidth))
            {
                bGrounded = true;
                anim.SetBool("jumping", false);
            }
        }
        // bunny takes damage from enemies
        else if (obj.gameObject.tag == "enemy")
        {
            TakeDamage(1);
        } 
        // bunny heals from health item
        else if (obj.gameObject.tag == "health")
        {
            Heal(1);
        }
    }

    void OnCollisionExit2D(Collision2D obj)
    {
        // no collision = rabbit in air
        bGrounded = false;
    }

    void Heal(int h)
    {
        if (currentHealth < maxHealth)
        {
            PlayerPrefs.SetInt("bunnyHealth", currentHealth + h);
        }
    }

    void TakeDamage(int dmg)
    {
        PlayerPrefs.SetInt("bunnyHealth", currentHealth - dmg);
        if (PlayerPrefs.GetInt("bunnyHealth") <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
