using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

//Runs when the first player collides with the finish point
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
