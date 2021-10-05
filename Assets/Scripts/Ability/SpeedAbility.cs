using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAbility : MonoBehaviour
{
    public float speedTimer;
    public PlayerController pController;
    public Collectable collectable;
    public bool startTimer;
    public bool unitTesting2;

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

            if (speedTimer >= 9)
            {
                pController.ResetSpeed();
                speedTimer = 0;
                startTimer = false;
            }
        }
    }

    //Change the Player Speed
    public void activateSpeed(bool t, bool testing)
    {
        //Unit Testing purposes
        //If the player has activated there ability
        if (t && testing)
            abilityTest();

        else if(t)
        {
            pController.pickAbility(1, false);//speed the playerup

            //ActivateSpeed the timer
            startTimer = t;
        }
    }

    //Unit Testing
    public void abilityTest()
    {
        pController.pickAbility(1, true);//speed the playerup
        unitTesting2 =  true;
    }
}
