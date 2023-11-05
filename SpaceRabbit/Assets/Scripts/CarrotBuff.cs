using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBuff : MonoBehaviour
{
    public int buffEffect = 20;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("player"))
        {
            // add buff effect here
            PlayerPrefs.SetInt("carrotBuff", buffEffect);

            // carrot disappears when player "collects" it
            Destroy(this.gameObject);
        }
    }

}
