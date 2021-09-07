using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class describes and runs a stasis trap sabotage - which freezes players in place for a few seconds
public class StasisTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //This method applies the sabotage to all players EXCEPT the source
    //Returns a bool for unit testing purposes
    public bool applySabotage(PlayerController source, PlayerController[] targets)
    {
        if (targets[0].isDisabled && !(source.isDisabled))
            return true;
        else
            return false;
    }
}
