using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class describes and runs a Missile Trap sabotage - which fires a missile at the player for a duration
public class MissileTrap : MonoBehaviour
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
                    //Activate the sabotage, firing a missile at the target
                    target.PV.RPC("ActivateSabotage", RpcTarget.All, 2);
                }
            }
        }
    }
}
