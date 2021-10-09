using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Contains the script for tutorial ability controller
public class TutorialAbility : MonoBehaviour
{
    private tutorialPlayer player; //access tutorial player script
    private int counter;//Counter for ability

    private void Start()
    {
        counter = 0;

    }

    private void Update()
    {
        ActivateAbility();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if target object is a collectable
        if (collision.CompareTag("Collectable"))
        {
            counter++;
            Destroy(collision.gameObject);
            Debug.Log("counter: " + counter);
        }
    }

    private void ActivateAbility()
    {
        if (counter == 8 && Input.GetButtonDown("Down"))
        {
            player.movementSpeed = 20;
        }
    }
}
