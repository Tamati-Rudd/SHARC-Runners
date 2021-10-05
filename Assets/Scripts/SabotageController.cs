using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

//This class controls Sabotages - negative effects that are applied to all other players when a player collects a Sabotage pickup
public class SabotageController : MonoBehaviour
{
    public static SabotageController SabotageInstance;
    public PhotonView PV;
    private PlayerController[] controllers = new PlayerController[20]; //Array size 20 as our maximum CCU is 20
    private int numControllers = 0;
    System.Random rand = new System.Random();
    public StasisTrap stasisSabotage;
    public BlindnessTrap blindnessSabotage;
    public Vector2 SpawnPoint, SpawnPoint1, SpawnPoint2, SpawnPoint3, SpawnPoint4, SpawnPoint5, SpawnPoint6;

    //Ensure there is only one sabotage controller instance in the game
    void Awake()
    {
        if (SabotageInstance)
        {
            Destroy(gameObject);
            return;
        }
        SabotageInstance = this;
    }

    //Determine Sabotage crate spawn positions and spawn them
    private void Start()
    {
        SpawnPoint.x = (float)66.82;
        SpawnPoint.y = (float)8.89;

        SpawnPoint1.x = (float)69.58;
        SpawnPoint1.y = (float)-2.87;

        SpawnPoint2.x = (float)82.35;
        SpawnPoint2.y = (float)21.38;

        SpawnPoint3.x = (float)32.21;
        SpawnPoint3.y = (float)26.25;

        SpawnPoint4.x = (float)94.66;
        SpawnPoint4.y = (float)41.31;

        SpawnPoint5.x = (float)92.8;
        SpawnPoint5.y = (float)66.1;

        SpawnPoint6.x = (float)159.2;
        SpawnPoint6.y = (float)12.4;

        if (PV.Owner.IsMasterClient)
        {
            CreateSabotage(SpawnPoint);
            CreateSabotage(SpawnPoint1);
            CreateSabotage(SpawnPoint2);
            CreateSabotage(SpawnPoint3);
            CreateSabotage(SpawnPoint4);
            CreateSabotage(SpawnPoint5);
            CreateSabotage(SpawnPoint6);
        }
    }

    //Spawn a Sabotage Crate
    void CreateSabotage(Vector2 SpawnPoint)
    {
        GameObject SabotageClone = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "SabotageCrate"), SpawnPoint, Quaternion.identity);
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
        }
        
        return controllers;
    }

    //This method removes a PlayerController from the array of controllers. Run when a player finishes the race
    public void removePlayerController(PlayerController toRemove)
    {
        int index = System.Array.IndexOf(controllers, toRemove);
        
        if (index != null)
        {
            controllers[index] = null;
            numControllers--;
        }
    }

    //This method randomly selects a sabotage and calls that sabotages applySabotage method
    //The selectedSabotage is returned for unit testing purposes 
    public int sabotage(PlayerController sourcePlayer, int unitTesting)
    {
        int selectedSabotage = rand.Next(1, 3); //Integer Range: 1 to 2. Currently allows stasis, blindness traps

        //Run the selected Sabotage
        if (selectedSabotage == 1 && unitTesting == 0) 
            stasisSabotage.applySabotage(sourcePlayer, controllers);
        if (selectedSabotage == 2 && unitTesting == 0)
            blindnessSabotage.applySabotage(sourcePlayer, controllers);

        return selectedSabotage;
    }
}
