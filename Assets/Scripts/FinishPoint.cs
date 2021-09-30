using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;

//This script manages the functionality of the game's Finish Point
public class FinishPoint : MonoBehaviour
{
    public Stopwatch timer;
    public PhotonView placementManagerPV;

    private void Start()
    {
        placementManagerPV = GameObject.FindGameObjectWithTag("PlacementManager").GetComponent<PhotonView>();
    }

    //Finish point collision
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Ensure colliding object was a player
        if (collision.tag == "Player" && timer != null)
        {
            PhotonView playerPV = collision.GetComponent<PhotonView>();
            PlayerController playerController = playerPV.GetComponent<PlayerController>();

            //Ensure the player hasn't already finished
            if (!(playerController.raceFinished))
            {
                //Get player data
                timer.StopStopwatch();
                string time = timer.getTime();

                string playerName = playerPV.Owner.NickName;

                placementManagerPV.RPC("registerFinish", RpcTarget.AllBufferedViaServer, playerName, time);
                Debug.Log("Should put player into spectator mode!");
                playerPV.RPC("Finished", RpcTarget.AllBuffered);
            }

            //Record player data
            //GameObject Record = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FinishRecord"), Vector2.zero, Quaternion.identity);
            //FinishRecord fr = Record.GetComponent<FinishRecord>();
            //PhotonView recordPV = Record.GetComponent<PhotonView>();
            //recordPV.RPC("clearIfNotMine", RpcTarget.AllBuffered); //Delete the record for player who aren't the one who finished
            //fr.setName(playerName);
            //fr.setTime(time);
            //fr.setPlacement(playerPlacement);

            //End the race
            //winnerPV.RPC("EndRaceRPC", RpcTarget.AllBuffered, winnerName, time);

            //Destroy the finished player's object, and move them (and only them!) to the PostGame screen
            //PhotonNetwork.Destroy(PhotonView.Find(playerPV.ViewID));
            //SceneManager.LoadScene("PostGame");
        }
    }
}
