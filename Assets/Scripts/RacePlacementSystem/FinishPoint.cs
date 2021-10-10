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

                //Register finish and transition player to spectate mode
                placementManagerPV.RPC("RegisterFinish", RpcTarget.AllBufferedViaServer, playerName, time);
                playerPV.RPC("Finished", RpcTarget.AllBuffered);

                GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
                Transform canvasTransform = canvas.transform;
                foreach (Transform transform in canvasTransform)
                {
                    if (transform.tag == "FinishedText")
                    {
                        GameObject finishedText = transform.gameObject;
                        finishedText.SetActive(true);
                    }
                    if (transform.tag == "Meter" || transform.tag == "Counter")
                    {
                        GameObject gameObject = transform.gameObject;
                        gameObject.SetActive(false);
                    }
                }
            }   
        }
    }
}
