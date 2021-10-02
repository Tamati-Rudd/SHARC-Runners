using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAbility : MonoBehaviour
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
                pController.ResetSpeed();
                speedTimer = 0;
                startTimer = false;
            }
        }
        
    }

    //Change the Player Speed
    public void ActivateSpeed(bool t)
    {
        pController.SpeedAbility();//speed the playerup

        //ActivateSpeed the timer
        startTimer = t;
    }
}
