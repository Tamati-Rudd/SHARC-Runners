using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : MonoBehaviour
{
    public Collectable collectable;
    public int disableAbility;
    public ProjectileObject projectilePrefab;
    public Transform LaunchOffset;
    public PlayerController pController;
    public ProjectileObject pObject;
    public void Start()
    {
        disableAbility = 0;
        pController = GetComponent<PlayerController>();
        pObject = GetComponent<ProjectileObject>();
    }
    
    //Change the Player Speed
    public void ActivateProjectile()
    {
        //the player can only throw one projectile
        if (disableAbility == 0)
        {
            Instantiate(projectilePrefab, LaunchOffset.position, LaunchOffset.rotation);
            disableAbility++;
        }
        //if the player presses r again then the player will teleport 
        else
        {
            Teleport();//Teleport to new location
            //pObject.DestroyObject();//Destroy the object
            collectable.UpdateCoins();//Reset the ability meter
            disableAbility = 0;
        }
    }

    //Teleport
    public void Teleport()
    {
        //Get the gameobject location
        Vector2 location = GameObject.FindGameObjectWithTag("ProjectileAbility").transform.position;
        
        pController.transform.position = location;//Teleport the player to the projectile
    }
}
