using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class describes and runs a Blindness Trap sabotage - which impairs a players vision of the game map for a duration
public class BlindnessTrap : MonoBehaviour
{
    //This method applies the sabotage to all players EXCEPT the source
    public void ApplySabotage(PlayerController source, PlayerController[] targets)
    {
        foreach (PlayerController target in targets)
        {
            if (target != null)
            {
                if (target != source)
                {
                    //Activate the sabotage, blinding the player
                    target.PV.RPC("ActivateSabotage", RpcTarget.All, 1);
                }
            }
        }
    }
}
