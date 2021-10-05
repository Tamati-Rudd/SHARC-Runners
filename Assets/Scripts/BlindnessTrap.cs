using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class describes and runs a Blindness Trap sabotage - which impairs a players vision of the game map for a duration
public class BlindnessTrap : MonoBehaviour
{
    public void applySabotage(PlayerController source, PlayerController[] targets)
    {
        foreach (PlayerController target in targets)
        {
            if (target != null)
            {
                if (target != source)
                {
                    //Blind the target and enable their spotlight
                    target.PV.RPC("BlindPlayerRPC", RpcTarget.All);
                }
            }
        }
    }

}
