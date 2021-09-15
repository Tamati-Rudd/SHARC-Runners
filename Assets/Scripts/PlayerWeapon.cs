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
    public Transform firePoint; //firepoint poisition
    public GameObject bulletPrefab; //for bullet prefab 
    public PhotonView PV; //access multiplayer server
    public PlayerController controller; //access the player controller
    public Animator anim; //animation for bullet

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        controller = PV.GetComponent<PlayerController>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }
    
    //method for player being able to shoot
    public void Shoot()
    {
        //only fire for the local player
        if (PV.IsMine)
        {
            //Only shoot if the local player isn't disabled
            if (!(controller.isDisabled))
                PV.RPC("ShootRPC", RpcTarget.All);
        }
    }

    //method for server so players can shoot on multiplayer
    [PunRPC]
    public void ShootRPC()
    {
        anim.SetTrigger("isShooting");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}