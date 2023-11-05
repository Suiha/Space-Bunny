using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    // heart disappears when player "collects" it
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("player"))
        {
            Destroy(this.gameObject);
        }
    }
}
