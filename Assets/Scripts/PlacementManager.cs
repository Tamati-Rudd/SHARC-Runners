using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//This class records how many players have finished in order to assign placements
public class PlacementManager : MonoBehaviour
{
    public static PlacementManager PlacementInstance;
    public int playerCount;
    public int playersFinished = 0;
    public Queue<FinishRecord> placements;
    public PhotonView PV;

    //Ensure there is only one PlacementManager, and setup variables
    private void Awake()
    {
        if (PlacementInstance)
        {
            Destroy(gameObject);
            return;
        }

        PlacementInstance = this;
        placements = new Queue<FinishRecord>();
        Player[] players = PhotonNetwork.PlayerList;
        playerCount = players.Length;
    }

    //This method registers that a player finished and returns their placement as an integer
    public int registerFinish()
    {
        PV.RPC("incrementPlayersFinished", RpcTarget.AllBuffered);
        return playersFinished;
    }

    //public void registerFinish(string playerName, string time)
    //{
    //    //placements.Enqueue(new FinishRecord());
    //}

    [PunRPC]
    public void incrementPlayersFinished()
    {
        playersFinished++;
    }  
}
