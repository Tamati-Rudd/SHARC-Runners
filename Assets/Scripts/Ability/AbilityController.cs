using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public Collectable crystal;
    public bool valid;
    public SpeedAbility speed;
    public void Start()
    {
        valid = false;
    }
    public bool runAbility(int a)
    {
        valid = crystal.SetSpeed();//check if the player has collected 8 crystals

        if (valid)
        {
            switch (a)
            {
                //when a is 1 the ability is speed
                case 1:
                    speed.ActivateSpeed(true);
                    break;

                default:
                    Console.WriteLine("Error");
                    break;

            }          
        }
        return valid;
    }

    
}
