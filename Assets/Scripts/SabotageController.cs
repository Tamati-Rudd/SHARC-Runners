using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class controls sabotages - negative effects that are applied to all other players when a player collects a sabotage pickup
public class SabotageController : MonoBehaviour
{
    PhotonView PV;
    PlayerController[] controllers = new PlayerController[20]; //Array size 20 as our maximum CCU is 20
    int numControllers = 0;
    System.Random rand = new System.Random();
    
    StasisTrap stasisSabotage;
    

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        //stasisSabotage = new StasisTrap();
        //if (stasisSabotage != null)
        //Debug.Log("Stasis Sabotage Created!");
    }


    //This method adds a new PlayerController to the array of controllers
    //The array is returned for unit testing purposes
    public PlayerController[] addPlayerController(PlayerController newController)
    {
        //Add the newController to the controllers array (if there is room)
        if (numControllers < 20)
        {
            controllers[numControllers] = newController;
            numControllers++;
            Debug.Log("New Controller Added!");
        }
        
        return controllers;
    }

    
    //This method randomly selects a sabotage and calls that sabotages applySabotage method
    //The selectedSabotage is returned for unit testing purposes
    public int sabotage(PlayerController sourcePlayer)
    {
        int selectedSabotage = rand.Next(1, 2); //Range: minimum to maximum-1

        if (selectedSabotage == 1) //Stasis Trap
        {
            Debug.Log("Apply Stasis Trap Sabotage"); 
            //stasisSabotage.applySabotage(sourcePlayer, controllers);
        }
        return selectedSabotage;
    }
}
