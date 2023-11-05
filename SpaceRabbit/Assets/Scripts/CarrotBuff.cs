using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBuff : MonoBehaviour
{
    // carrot disappears when player "collects" it
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("player"))
        {
            Destroy(this.gameObject);
        }
    }

}
