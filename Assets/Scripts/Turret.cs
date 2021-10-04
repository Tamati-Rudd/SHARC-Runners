using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//This contains the script for the enemy turret's fire rate
public class EnemyTurret : MonoBehaviour
{
    public Transform firepoint; //firing point of turret
    public GameObject enemyBullet; //Object space for the enemy bullet prefab
    private float timeBetween; //time between shots
    public PhotonView PV; //access multiplayer server
    public float  startTimeBetween; //starting time between shots

    private void Start()
    {
        timeBetween = startTimeBetween;
    }

    private void Update()
    {
       TurretShoot();
    }

    //Method for Turret Shooting
    public void TurretShoot()
    {
         if(timeBetween <= 0)
        {
            PV.RPC("TurretShootRPC", RpcTarget.All);
            timeBetween = startTimeBetween;
        }
        else 
        {
            timeBetween -= Time.deltaTime;
        }
    }

    //Method for Turret Shooting in multiplayer
    [PunRPC]
    public void TurretShootRPC()
    {
        Instantiate(enemyBullet, firepoint.position, firepoint.rotation);
    }
}
