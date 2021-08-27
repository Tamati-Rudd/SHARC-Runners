using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
** PlayerWeapon Script
** This carries out the shooting behaviour of the player.
**/
public class PlayerWeapon : MonoBehaviour
{
   public Transform firePoint;
   public GameObject bulletPrefab;

   void Update()
   {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
   }

   void Shoot()
   {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
   }
}
