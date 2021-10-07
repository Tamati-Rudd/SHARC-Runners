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
    public NodeShiftingAbility nShift;
    public Collectable collectableMeter;//Access the collectable script

    private void Awake()
    {
        nShift = GetComponent<NodeShiftingAbility>();
        collectableMeter = GetComponent<Collectable>();
    }

    public void Start()
    {
        valid = false;
    }

    //Activtes the players ability
    public void RunAbility(int a, bool testing)
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
                        speed.ActivateSpeed(true, false);
                    }
                    //unit Testing
                    else if (testing)
                    {
                        speed.ActivateSpeed(true, true);
                    }   
                    break;

                //when a is 1 the ability is jetpack
                case 1:
                    if (!testing)
                    {
                        jetpack.ActivateJetpack(true);
                    }
                    break;

                case 2:
                    nShift.teleport();
                    collectableMeter.UpdateCoins();
                    break;
                default:
                    Console.WriteLine("No Number is found");
                    break;
            }          
        }
    }
    
}
