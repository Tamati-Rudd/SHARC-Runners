using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class controls sabotages - negative effects that are applied to all other players when a player collects a sabotage pickup
public class SabotageController : MonoBehaviour
{
    PlayerController[] controllers;

    // Start is called before the first frame update
    void Start()
    {
        //Get the controllers
    }

    
    //[PunRPC] may or may not need to be an RPC, unsure yet
    void applySabotage(PhotonView sourcePlayer)
    {
        //TO DO: Apply freeze sabotages
    }
}
