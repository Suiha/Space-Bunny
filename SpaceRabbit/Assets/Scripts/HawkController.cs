using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkController : MonoBehaviour
{
    Rigidbody2D hawk;
    public float speed;
    private int direction = 1;

    private Vector2 screenBounds;
    private float objectWidth;

    // Start is called before the first frame update
    void Start()
    {
        hawk = GetComponent<Rigidbody2D>();
        hawk.velocity = new Vector2(speed, 0);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        objectWidth = hawk.GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // prevent hawk from flying off screen
        Vector3 newPos = hawk.position;
        newPos.x = Mathf.Clamp(newPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        hawk.position = newPos;

        // if hawk hits bounds, turn around
        if (hawk.position.x <= -screenBounds.x + objectWidth || hawk.position.x >= screenBounds.x - objectWidth)
        {
            direction *= -1;
            hawk.velocity = new Vector2(direction * speed, 0);
        }

    }

    // hawk turns when it hits a platform
    void OnCollisionEnter2D(Collision2D obj)
    {
        //transform.localScale = new Vector3(-1, 1, 1);
        
        if (obj.gameObject.tag == "platform")
        {
            direction *= -1;
            hawk.velocity = new Vector2(direction * speed, 0);
        }
    }
}
