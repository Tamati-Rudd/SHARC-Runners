using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackAbility : MonoBehaviour
{
    public float speedTimer;
    public PlayerController pController;
    public Collectable collectable;
    public bool startTimer;

    // Start is called before the first frame update
    public void Start()
    {
        speedTimer = 0;
        startTimer = false;
    }

    public void Update()
    {
        if (startTimer)
        {
            //Start the timer
            speedTimer += Time.deltaTime;

            if (speedTimer >= 5)
            {
                pController.pickAbility(3);
                speedTimer = 0;
                startTimer = false;
            }
        }
    }

    //Change the Player Speed
    public void activateJetpack(bool t)
    {
        pController.pickAbility(2);//Put a jetpack on the player

        //ActivateSpeed the timer
        startTimer = t;
    }
}
