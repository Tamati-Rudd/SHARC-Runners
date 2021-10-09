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

            if (speedTimer >= 12)
            {
                pController.ResetSpeed();
                speedTimer = 0;
                startTimer = false;
            }
        }
    }

    //Change the Player Speed
    public void ActivateSpeed(bool t, bool testing)
    {
        //Unit Testing purposes
        //If the player has activated there ability
        if (t && testing)
            AbilityTest();

        else if(t)
        {
            pController.PickAbility(1, false);//speed the playerup

            //ActivateSpeed the timer
            startTimer = t;
        }
    }

    //Unit Testing
    public void AbilityTest()
    {
        pController.PickAbility(1, true);//speed the playerup
        unitTesting2 =  true;
    }
}
