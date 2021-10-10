using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class manages player collision with sabotage crates and controls how long a player is sabotaged for
public class Sabotagable : MonoBehaviour
{
    public PhotonView PV;
    private PlayerController playerController;

    //Get the Photon View of the Player this script is attached to
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        playerController = PV.GetComponent<PlayerController>();
    }

    //This method checks for and controls the duration of sabotages applied to the player
    //Returns a boolean indicating whether the player's update method should be allowed to continue
    public bool CheckSabotages(float timeSinceLastUpdate)
    {
        bool allowUpdate = true;
        int lightToUse = -1;

        if (playerController.sabotageTimers[1] > 0) //Check for blindness trap sabotage
        {
            playerController.sabotageTimers[1] -= timeSinceLastUpdate;
            if (playerController.sabotageTimers[1] <= 0)
                DeactivateSabotage(1);
            else
                lightToUse = 1;
        }

        if (playerController.sabotageTimers[2] > 0) //Check for missile trap sabotage
        {
            playerController.sabotageTimers[2] -= timeSinceLastUpdate;
            if (playerController.sabotageTimers[2] <= 0)
                DeactivateSabotage(2);
            else
            {
                playerController.missile.MoveMissile();
                lightToUse = 2;
            }        
        }

        if (playerController.sabotageTimers[0] > 0) //Check for stasis trap sabotage
        {
            playerController.sabotageTimers[0] -= timeSinceLastUpdate;
            if (playerController.sabotageTimers[0] <= 0)
                DeactivateSabotage(0);
            else
            {
                lightToUse = 0;
                allowUpdate = false;
            }
        }

        playerController.PV.RPC("SetSabotageIndicator", RpcTarget.All, lightToUse);
        return allowUpdate;
    }

    //This method deactivates at least one sabotage for a player
    public void DeactivateSabotage(int toDeactivate)
    {
        switch (toDeactivate)
        {
            case 0: //Stasis Trap
                playerController.EnablePlayer();
                playerController.sabotageTimers[0] = 0;
                break;
            case 1: //Blindness Trap
                playerController.mapLight.pointLightOuterRadius = 200;
                playerController.sabotageTimers[1] = 0;
                break;
            case 2: //Missile Trap
                playerController.missile.Explode();
                playerController.missile = null;
                playerController.missileObject = null;
                playerController.sabotageTimers[2] = 0;
                break;
            default: //A Player Finished the Race - Deactivate all Active Sabotages
                playerController.EnablePlayer();
                playerController.mapLight.pointLightOuterRadius = 200;
                if (playerController.missile != null)
                    playerController.missile.Explode();
                playerController.sabotageTimers[0] = 0;
                playerController.sabotageTimers[1] = 0;
                playerController.sabotageTimers[2] = 0;
                playerController.PV.RPC("SetSabotageIndicator", RpcTarget.All, -1);
                break;
        }
    }

    //Handle player collision with a Sabotage crate
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sabotage" && !(playerController.raceFinished))
        {
            //Remove the sabotage crate
            int viewID = collision.GetComponent<PhotonView>().ViewID; //Get crates viewID
            PV.RPC("DestroySabotageCrate", RpcTarget.MasterClient, viewID);

            //Signal the SabotageController to apply a Sabotage
            SabotageController sabController = GameObject.FindGameObjectWithTag("SabotageController").GetComponent<SabotageController>();
            PlayerController sourceController = GetComponent<PlayerController>();
            sabController.Sabotage(sourceController, 0);
        }
    }

    //Destroy a sabotage crate
    [PunRPC]
    public void DestroySabotageCrate(int viewID)
    {
        while (PhotonView.Find(viewID) != null)
        {
            PhotonNetwork.Destroy(PhotonView.Find(viewID));
        }
    }
}
