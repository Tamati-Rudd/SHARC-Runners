using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

//This class records how many players have finished in order to assign placements
public class PlacementManager : MonoBehaviourPunCallbacks
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
    public void RegisterFinish(string playerName, string playerTime)
    {
        playersFinished = playersFinished + 1;
        int playerPlacement = playersFinished;
        placements.Enqueue(new FinishRecord(playerName, playerTime, playerPlacement));

        if (playersFinished == playerCount)
            SceneManager.LoadScene("PostGame");
    }

    //Handle a player leaving the game mid-game
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        string leftName = otherPlayer.NickName;
        bool leftPlayerFinished = false;

        foreach (FinishRecord fr in placements) {
            string name = fr.GetName();
            if (leftName.Equals(name))
                leftPlayerFinished = true;
        }

        //If the left player hadn't finished, then deduct them from the player count
        if (!leftPlayerFinished)
            playerCount = playerCount - 1;

        //Re-check if the game should be ended
        if (playersFinished == playerCount)
            SceneManager.LoadScene("PostGame");
    }
}
