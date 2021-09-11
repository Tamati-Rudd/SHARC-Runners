using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Script for attack button
public class AttackButton : MonoBehaviourPunCallbacks
{
    public PlayerWeapon weapon; //access methods of PlayerWeapon Script
    public PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        weapon = PV.GetComponent<PlayerWeapon>();
    }

    void Update()
    {
        
    }

    //Detect Button being pressed
    public void ShootButton()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            weapon.Shoot();
        }
    }
}
