using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


//NOTE: For this scene to function correctly, the next built scene after the game scene MUST be the game ended scene

public class FinishPoint : MonoBehaviour
{
    PhotonView winnerPV;
    public string winnerName;
   

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        { //If the colliding object has the Player tag
            //Record the winner's name
            winnerPV = collision.GetComponent<PhotonView>();
            winnerName = winnerPV.Owner.NickName;

            //Load the post game screen for all players  
            winnerPV.RPC("EndRaceRPC", RpcTarget.AllBuffered, winnerName);

        }
    }
}
