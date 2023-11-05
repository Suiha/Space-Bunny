using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBuff : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("player"))
        {
            // add buff effect here

            // carrot disappears when player "collects" it
            Destroy(this.gameObject);
        }
    }

}
