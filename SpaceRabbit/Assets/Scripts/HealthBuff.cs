using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    public int heal = 1;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("player"))
        {
            // update player prefs
            int health = PlayerPrefs.GetInt("bunnyHealth");
            PlayerPrefs.SetInt("bunnyHealth", health + heal);

            // heart disappears when player "collects" it
            Destroy(this.gameObject);
        }
    }
}
