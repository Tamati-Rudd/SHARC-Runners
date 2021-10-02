using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class manages player collision with sabotage crates
public class Sabotagable : MonoBehaviour
{
    public PhotonView PV;
    private PlayerController playerController;

    //Get the Photon View of the Player this script is attached to
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        playerController = PV.GetComponent<PlayerController>();
    }

    //Handle player collision with a Sabotage crate
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sabotage" && !(playerController.raceFinished))
        {
            //Remove the sabotage crate
            int viewID = collision.GetComponent<PhotonView>().ViewID; //Get crates viewID
            PV.RPC("DestroySabotageCrate", RpcTarget.MasterClient, viewID);

            //Signal the SabotageController to apply a Sabotage
            SabotageController sabController = GameObject.FindGameObjectWithTag("SabotageController").GetComponent<SabotageController>();
            PlayerController sourceController = GetComponent<PlayerController>();
            sabController.sabotage(sourceController, 0);
        }
    }

    //Destroy a sabotage crate
    [PunRPC]
    public void DestroySabotageCrate(int viewID)
    {
        while (PhotonView.Find(viewID) != null)
        {
            PhotonNetwork.Destroy(PhotonView.Find(viewID));
        }
    }
}
