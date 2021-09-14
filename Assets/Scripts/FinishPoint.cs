using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

//NOTE: For the Finish Point to function correctly, the next built scene after the GameScene MUST be the PostGame scene

//This script manages the functionality of the game's Finish Point
public class FinishPoint : MonoBehaviour
{
    PhotonView winnerPV;
    public string winnerName;
    public Stopwatch timer;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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

            //Record the winner's name
            winnerPV = collision.GetComponent<PhotonView>();
            winnerName = winnerPV.Owner.NickName;

            //End the race
            winnerPV.RPC("EndRaceRPC", RpcTarget.AllBuffered, winnerName, time);
        }
    }
}
