using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

//This class records how many players have finished in order to assign placements
public class PlacementManager : MonoBehaviourPun
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

    //This method registers a player completing the race
    [PunRPC]
    public void registerFinish(string playerName, string playerTime)
    {
        playersFinished = playersFinished + 1;
        int playerPlacement = playersFinished;
        placements.Enqueue(new FinishRecord(playerName, playerTime, playerPlacement));

        if (playersFinished == playerCount)
            SceneManager.LoadScene("PostGame");
    }


}
