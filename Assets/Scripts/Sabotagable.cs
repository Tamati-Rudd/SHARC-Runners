using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class manages player collision with sabotage crates
public class Sabotagable : MonoBehaviour
{
    public PhotonView PV; 

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sabotage")
        {
            Debug.Log("Sabotage pickup collected!");

            //Remove the sabotage crate
            int viewID = collision.GetComponent<PhotonView>().ViewID; //Get crates viewID
            PV.RPC("DestroySabotageCrate", RpcTarget.AllBuffered, viewID);

            //Select and apply the sabotage
            SabotageController sabController = GameObject.FindGameObjectWithTag("SabotageController").GetComponent<SabotageController>();
            PlayerController sourceController = GetComponent<PlayerController>();
            sabController.sabotage(sourceController);
        }
    }

    //Destroy a sabotage crate
    [PunRPC]
    public void DestroySabotageCrate(int viewID)
    {
        PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);
    }
}
