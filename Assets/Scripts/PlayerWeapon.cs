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
   public PlayerController controller;
   public Animator anim;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        controller = PV.GetComponent<PlayerController>();
    }

    void Update()
   {
    
   }

   void Shoot()
   {
        //only fire for the local player
        if (PV.IsMine)
        {
            //Only shoot if the local player isn't disabled
            if (!(controller.isDisabled))
                PV.RPC("ShootRPC", RpcTarget.All);
        }
        
   }

    [PunRPC]
    void ShootRPC()
    {
        anim.SetTrigger("isShooting");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void ShootButton()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
}

