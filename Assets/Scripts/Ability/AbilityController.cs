using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public Collectable crystal;
    public bool valid;
    public SpeedAbility speed;
    public JetpackAbility jetpack;

    public void Start()
    {
        valid = false;
    }

    //Activtes the players ability
    public void runAbility(int a, bool testing)
    {
        valid = crystal.SetSpeed(true);//check if the player has collected 8 crystals

        if (valid)
        {
            switch (a)
            {
                //when a is 1 the ability is speed
                case 0:
                    if (!testing)
                    {
                        speed.activateSpeed(true, false);
                    }
                    //unit Testing
                    else if (testing)
                    {
                        speed.activateSpeed(true, true);
                    }   
                    break;

                //when a is 2 the ability is jetpack
                case 1:
                    jetpack.activateJetpack(true);
                    break;

                default:
                    Console.WriteLine("No Number is found");
                    break;
            }          
        }
    }
    
}
