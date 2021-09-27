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
    PhotonView playerPV;
    public string playerName;
    public Stopwatch timer;
    PlacementManager placements;

    private void Start()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
        placements = GameObject.FindGameObjectWithTag("PlacementManager").GetComponent<PlacementManager>();
    }

    //Handle collision with the Finish Point
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the colliding object has the Player tag
        if (collision.tag == "Player" && timer != null)
        { 
            //Get the winner's time
            timer.StopStopwatch();
            string time = timer.getTime();

            //Determine the player's placement
            int playerPlacement = placements.registerFinish();

            //Record the winner's name
            playerPV = collision.GetComponent<PhotonView>();
            playerName = playerPV.Owner.NickName;

            GameObject Record = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FinishRecord"), Vector2.zero, Quaternion.identity);
            if (Record == null)
                Debug.Log("Record Doesnt Exist!");
            FinishRecord fr = Record.GetComponent<FinishRecord>();
            if (fr == null)
                Debug.Log("Fr Doesnt Exist!");
            fr.setName(playerName);
            fr.setTime(time);
            fr.setPlacement(playerPlacement);

            //End the race
            //winnerPV.RPC("EndRaceRPC", RpcTarget.AllBuffered, winnerName, time);

            //Destroy the finished player's object, and move them (and only them!) to the PostGame screen
            Destroy(collision); //Needs to be changed to work properly
            SceneManager.LoadScene("PostGame");
        }
    }
}
