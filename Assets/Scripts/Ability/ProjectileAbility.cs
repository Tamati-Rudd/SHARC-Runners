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
        Instantiate(projectilePrefab, LaunchOffset.position, LaunchOffset.rotation);
       

    }
}
