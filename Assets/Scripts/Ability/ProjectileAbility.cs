using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : MonoBehaviour
{
    public Collectable collectable;
    public int disableAbility;
    public ProjectileObject projectilePrefab;
    public Transform LaunchOffset;
 
    public void Start()
    {
        disableAbility = 0;
    }
    public void Update()
    {
        
    }
    //Change the Player Speed
    public void ActivateProjectile()
    {
        //the player can only throw one projectile
        if(disableAbility == 0)
        {
            Instantiate(projectilePrefab, LaunchOffset.position, LaunchOffset.rotation);
            disableAbility++;
        }
        //if the player presses r again then the player will teleport 
        else
        {
            //Teleport to new location
            collectable.UpdateCoins();//Reset the ability meter
        }

    }
}
