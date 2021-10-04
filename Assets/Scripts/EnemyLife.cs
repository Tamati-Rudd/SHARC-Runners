using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
/** 
** EnemyLife Script
** This script contains the life behaviour of the enemy:
** Receiving Damage 
** Death of enemy
** Generating gems
**/
public class EnemyLife : MonoBehaviourPunCallbacks
{
    public int HP = 10; //Health Point of Enemy

    //Materials used for the animation
    private Material matWhite;
    private Material matDefault;
    public PhotonView PV;
    
    SpriteRenderer sr;
    public GameObject gemPrefab;

    public void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
    }

    //Method for enemy detecting collision with player bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When enemy is struck by bullet
        if(collision.CompareTag("PlayerProjectile"))
        {
            HP--; //Health goes down
            //sr.material = matWhite; //Enemy flashes white to indicate hit 
            GenerateGem();

            if(HP <= 0)
            {

                int viewID = this.GetComponent<PhotonView>().ViewID;

                PV.RPC("Kill", RpcTarget.MasterClient, viewID);
            }
        
        }
    }


    //Method for generating gems
    private void GenerateGem()
    {
        PhotonNetwork.InstantiateRoomObject((Path.Combine("PhotonPrefabs", "Square")), transform.position, transform.rotation);

       // Instantiate(gemPrefab, transform.position, gemPrefab.transform.rotation);              
       
    }

    [PunRPC]
    public void Kill(int viewID)
    {

        PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);




    }
}
