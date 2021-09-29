using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class records how many players have finished in order to assign placements
public class PlacementManager : MonoBehaviour
{
    public int playersFinished = 0;
    public PhotonView PV;

    //This method registers that a player finished and returns their placement as an integer
    public int registerFinish()
    {
        PV.RPC("incrementPlayersFinished", RpcTarget.AllBuffered);
        return playersFinished;
    }

    [PunRPC]
    public void incrementPlayersFinished()
    {
        playersFinished++;
    }
}
