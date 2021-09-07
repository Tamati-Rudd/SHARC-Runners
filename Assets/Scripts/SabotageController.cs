using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class controls sabotages - negative effects that are applied to all other players when a player collects a sabotage pickup
public class SabotageController : MonoBehaviour
{
    PlayerController[] controllers;
    StasisTrap stasisSabotage;

    // Start is called before the first frame update
    void Start()
    {
        controllers = new PlayerController[20]; //Array size 20 as our maximum CCU is 20?
        stasisSabotage = new StasisTrap();
    }


    //This method adds a new PlayerController to the array of controllers
    //The array is returned for unit testing purposes
    public PlayerController[] addPlayerController(PlayerController newController)
    {
        //TO DO: Add the newController to the controllers array
        return controllers;
    }

    
    //This method randomly selects a sabotage and calls that sabotages applySabotage method
    //[PunRPC] may or may not need to be an RPC, unsure yet
    public int sabotage(PlayerController sourcePlayer)
    {
        int selectedSabotage = 0;
        //TO DO: Select and apply a sabotage
        return selectedSabotage;
    }
}
