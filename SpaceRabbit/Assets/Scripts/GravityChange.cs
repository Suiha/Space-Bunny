using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour
{
    void Start() { }

    void OnTriggerEnter2D(Collider2D obj)
    {
        // switch between earth and space gravity
        if (obj.CompareTag("player")) {
            if (PlayerPrefs.GetFloat("gravity") == 1.8f)
            {
                PlayerPrefs.SetFloat("gravity", 1.0f);
            } else
            {
                PlayerPrefs.SetFloat("gravity", 1.8f);
            }
            
        }
    }
}
