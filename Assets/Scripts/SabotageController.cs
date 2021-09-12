using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

//This class controls sabotages - negative effects that are applied to all other players when a player collects a sabotage pickup
public class SabotageController : MonoBehaviour
{
    public static SabotageController Instance;
    PhotonView PV;
    PlayerController[] controllers = new PlayerController[20]; //Array size 20 as our maximum CCU is 20
    int numControllers = 0;
    System.Random rand = new System.Random();
    StasisTrap stasisSabotage;
    public Vector2 SpawnPoint, SpawnPoint1, SpawnPoint2, SpawnPoint3;

    private void Start()
    {
        SpawnPoint.x = (float)56.822;
        SpawnPoint.y = (float)-9.087;

        SpawnPoint1.x = (float)55.155;
        SpawnPoint1.y = (float)11.623;

        SpawnPoint2.x = (float)78.119;
        SpawnPoint2.y = (float)35.352;

        SpawnPoint3.x = (float)60.512;
        SpawnPoint3.y = (float)50.6927;


        if (PV.Owner.IsMasterClient)
        {
            CreateSabotage(SpawnPoint);
            CreateSabotage(SpawnPoint1);
            CreateSabotage(SpawnPoint2);
            CreateSabotage(SpawnPoint3);
        }
    }

    void CreateSabotage(Vector2 SpawnPoint)
    {
        GameObject SabotageClone = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "SabotageCrate"), SpawnPoint, Quaternion.identity);

    }

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        Transform stasisChild = transform.Find("StasisTrap");
        stasisSabotage = stasisChild.GetComponent<StasisTrap>();
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

    //This method randomly selects a sabotage and calls that sabotages applySabotage method
    //The selectedSabotage is returned for unit testing purposes 
    public int sabotage(PlayerController sourcePlayer, int unitTesting)
    {
        int selectedSabotage = rand.Next(1, 2); //Range: minimum to maximum-1

        if (selectedSabotage == 1 && unitTesting == 0) //Stasis Trap
        { //When unit testing, do not run a sabotage with this test (there is a separate test for this)
            stasisSabotage.applySabotage(sourcePlayer, controllers);
        }
        //In the future, add more if statements for additonal sabotages

        return selectedSabotage;
    }
}
