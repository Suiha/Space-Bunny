using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    Rigidbody2D bunny;
    SpriteRenderer spriteRenderer;
    Animator anim;

    // materials
    Material bunnyMaterial;
    [SerializeField] Material flashMaterial;
    [SerializeField] Material healMaterial;
    [SerializeField] Material buffMaterial;

    // bunny traits
    public int maxHealth = 10;
    public float jumpPower, speed;

    // grounded check
    private bool bGrounded;

    // Start is called before the first frame update
    void Start()
    {
        bunny = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bunnyMaterial = spriteRenderer.material;

        PlayerPrefs.SetInt("bunnyMaxHealth", maxHealth);
        PlayerPrefs.SetInt("bunnyHealth", maxHealth);

        PlayerPrefs.SetInt("carrotBuff", 0);
    }

    // FixedUpdate is called before update
    void FixedUpdate()
    {
        // movement
        if ((Input.GetKey("w") || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && bGrounded)
        {
            // carrot buff increases jump power
            if (PlayerPrefs.GetInt("carrotBuff") != 0)
            {
                bunny.velocity = new Vector2(bunny.velocity.x, jumpPower * PlayerPrefs.GetInt("carrotBuff"));
                PlayerPrefs.SetInt("carrotBuff", 0);
            } else
            {
                bunny.velocity = new Vector2(bunny.velocity.x, jumpPower);
            }
            GetComponent<AudioSource>().Play();
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
        } else
        {
            anim.SetBool("moving", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // no x movement, stop moving animation
        if (bunny.velocity.x == 0)
        {
            anim.SetBool("moving", false);
        }

        // effects and precedence
        // check for buff effect
        if (PlayerPrefs.GetInt("bunnyBuff") == 1)
        {
            StartCoroutine(BuffEffect());
        }
        // check for flash effect
        else if (PlayerPrefs.GetInt("bunnyHit") == 1)
        {
            StartCoroutine(FlashEffect());
        }
        // check for heal effect
        else if (PlayerPrefs.GetInt("bunnyHeal") == 1)
        {
            StartCoroutine(HealEffect());
        }
    }

    // collision effect depends on object type
    void OnCollisionEnter2D(Collision2D obj)
    {
        // bunny can only jump if in contact with a platform
        if (obj.gameObject.tag == "platform")
        {
            float platWidth = obj.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
            float platHeight = obj.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            // check if bunny is above platform and not on the sides
            if (bunny.position.y > obj.gameObject.transform.position.y + platHeight && 
                (bunny.position.x > obj.gameObject.transform.position.x - platWidth && bunny.position.x < obj.gameObject.transform.position.x + platWidth))
            {
                bGrounded = true;
                anim.SetBool("jumping", false);
            }
        }
    }

    void OnCollisionExit2D(Collision2D obj)
    {
        // no collision = rabbit in air
        bGrounded = false;
    }

    private IEnumerator BuffEffect()
    {
        // swap to flash effect
        spriteRenderer.material = buffMaterial;

        // wait before executing next part of function
        yield return new WaitForSeconds(0.15f);

        // swap to original material
        spriteRenderer.material = bunnyMaterial;

        // stop routine
        PlayerPrefs.SetInt("bunnyBuff", 0);
    }

    private IEnumerator FlashEffect()
    {
        // swap to flash effect
        spriteRenderer.material = flashMaterial;

        // wait before executing next part of function
        yield return new WaitForSeconds(0.15f);

        // swap to original material
        spriteRenderer.material = bunnyMaterial;

        // stop routine
        PlayerPrefs.SetInt("bunnyHit", 0);
    }

    private IEnumerator HealEffect()
    {
        // swap to heal effect
        spriteRenderer.material = healMaterial;

        // wait before executing next part of function
        yield return new WaitForSeconds(0.15f);

        // swap to original material
        spriteRenderer.material = bunnyMaterial;

        // stop routine
        PlayerPrefs.SetInt("bunnyHeal", 0);
    }
}
