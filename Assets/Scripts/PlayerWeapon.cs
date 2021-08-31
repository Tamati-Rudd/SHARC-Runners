using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/**
** PlayerWeapon Script
** This carries out the shooting behaviour of the player.
**/
public class PlayerWeapon : MonoBehaviourPunCallbacks
{
   public Transform firePoint;
   public GameObject bulletPrefab;
    public PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
   {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
   }

   void Shoot()
   {
        //only fire for the local player
        if (PV.IsMine)
        {
            PV.RPC("ShootRPC", RpcTarget.All);
        }
        
   }

    [PunRPC]
    void ShootRPC()
    {        
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}

