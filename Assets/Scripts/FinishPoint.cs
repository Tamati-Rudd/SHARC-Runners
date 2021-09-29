using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;

//NOTE: For the Finish Point to function correctly, the next built scene after the GameScene MUST be the PostGame scene

//This script manages the functionality of the game's Finish Point
public class FinishPoint : MonoBehaviour
{
    public Stopwatch timer;
    public PlacementManager placements;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        placements = GameObject.FindGameObjectWithTag("PlacementManager").GetComponent<PlacementManager>();
    }

    //Handle collision with the Finish Point
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the colliding object has the Player tag (prevent anything else from triggering the finish point)
        if (collision.tag == "Player" && timer != null)
        {
            //Get player data
            timer.StopStopwatch();
            string time = timer.getTime();
            int playerPlacement = placements.registerFinish();
            PhotonView playerPV = collision.GetComponent<PhotonView>();
            string playerName = playerPV.Owner.NickName;

            //Record player data
            GameObject Record = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FinishRecord"), Vector2.zero, Quaternion.identity);
            FinishRecord fr = Record.GetComponent<FinishRecord>();
            PhotonView recordPV = Record.GetComponent<PhotonView>();
            recordPV.RPC("clearIfNotMine", RpcTarget.AllBuffered); //Delete the record for player who aren't the one who finished
            fr.setName(playerName);
            fr.setTime(time);
            fr.setPlacement(playerPlacement);

            //End the race
            //winnerPV.RPC("EndRaceRPC", RpcTarget.AllBuffered, winnerName, time);

            //Destroy the finished player's object, and move them (and only them!) to the PostGame screen
            //PhotonNetwork.Destroy(PhotonView.Find(playerPV.ViewID));
            SceneManager.LoadScene("PostGame");
        }
    }
}
