using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    Rigidbody2D rabbit;
    public float jumpSpeed, maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rabbit = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // movement keys
        if (Input.GetKey("w"))
        {
            rabbit.velocity = transform.up * jumpSpeed;
        }
        if (Input.GetKey("a"))
        {
            rabbit.velocity = transform.right * -maxSpeed;
        }
        if (Input.GetKey("d"))
        {
            rabbit.velocity = transform.right * maxSpeed;
        }
    }
}
