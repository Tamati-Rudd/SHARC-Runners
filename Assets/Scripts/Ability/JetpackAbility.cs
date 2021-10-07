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

            if (speedTimer >= 9)
            {
                pController.PickAbility(4, false);
                speedTimer = 0;
                startTimer = false;
            }
        }
    }

    //Change the Player Speed
    public void ActivateJetpack(bool t)
    {
        pController.PickAbility(2, false);//Put a jetpack on the player
        
        startTimer = t;//ActivateSpeed the timer
    }
}
