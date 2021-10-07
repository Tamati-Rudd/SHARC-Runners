using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
** PlayerWeapon Script
** This carries out the shooting behaviour of the player.
**/
public class TutorialShoot : MonoBehaviour
{
    
    public Transform firePoint; //firepoint poisition
    public GameObject bulletPrefab; //for bullet prefab
    public PlayerController controller; //access the player controller
    public Animator anim; //animation for bullet

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
         Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
