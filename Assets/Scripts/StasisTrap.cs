using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//This class describes and runs a stasis trap sabotage - which freezes players in place for a few seconds
public class StasisTrap : MonoBehaviour
{
    //PunRPCs cannot be run in the Unity Test Framework,
    //So must use this to instead test with code lines that would mimic the disable RPC for the target
    public bool unitTesting = false; 

    //This method applies the sabotage to all players EXCEPT the source
    //Returns a bool for unit testing purposes
    public bool applySabotage(PlayerController source, PlayerController[] targets)
    {
        bool success = false;

        //Apply the stasis trap to the targets
        foreach (PlayerController target in targets) {
            if (target != null)
            {
                if (target != source && !(unitTesting))
                {
                    //Run the sabotage by disabling the target
                    target.PV.RPC("DisablePlayerRPC", RpcTarget.All);
                }
                else if (target != source && unitTesting) 
                {
                    Debug.Log("Unit Testing Stasis Trap");
                    //Mimic disable RPC functionality for unit testing purposes
                    target.isDisabled = true;

                    Debug.Log("target disabled: " + target.isDisabled);
                    Debug.Log("source disabled: " + source.isDisabled);
                    //Determine whether the sabotage was successful (for unit testing)
                    if (target.isDisabled && !(source.isDisabled))
                        success = true;
                }
            }      
        }
        return success;
    }
}
