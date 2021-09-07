using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sabotagable : MonoBehaviour
{
    public PhotonView PV; //The photon view of the crate

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sabotage")
        {
            Debug.Log("Sabotage pickup collected!");

            int viewID = collision.GetComponent<PhotonView>().ViewID;
            PV.RPC("DestroySabotageCrate", RpcTarget.AllBuffered, viewID);
            //TO DO: Call sabotage method
        }
    }

    [PunRPC]
    public void DestroySabotageCrate(int viewID)
    {
        PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);
    }
}
