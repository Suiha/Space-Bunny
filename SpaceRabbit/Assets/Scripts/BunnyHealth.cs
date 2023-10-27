using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BunnyHealth : MonoBehaviour
{
    private TMP_Text healthCounter;
    string health = "Health: ";

    public GameObject bunny;
    private BunnyController bunnyController;

    //public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        healthCounter = GetComponent<TMP_Text>();

        bunnyController = bunny.GetComponent<BunnyController>();
        healthCounter.text = health + bunnyController.maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthCounter.text = health + bunnyController.currentHealth;
    }

}
