using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//This class describes and controls a missile - an object launched at a player when they are targeted by a missile sabotage
[RequireComponent(typeof(Rigidbody2D))] //Indicates that this script requries a Rigidbody2D component to be on its object
public class SabotageMissile : MonoBehaviour
{
    [Header("Missile Information")]
    public float speed = 0.5f;
    public float rotateSpeed = 10f;
    public Rigidbody2D rb;
    public PlayerController target;

    //Set the target player of a missile
    public void SetTarget(PlayerController targetController)
    {
        target = targetController;
    }
    
    //Moves the missile towards the target player
    public void MoveMissile()
    {
        //Determine the direction to point the missile
        Vector2 direction = (Vector2)target.PV.transform.position - rb.position;
        direction.Normalize();

        //Rotate the missile
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;

        //Move the missile towards the target player
        rb.velocity = transform.up * speed;
    }

    //This method calls the target's missile sabotage to end early as a result of collision
    public void EndMissile()
    {
        target.sabotageCheck.DeactivateSabotage(2);
    }

    //Destroy the missile
    public void Explode()
    {
        Destroy(gameObject);
    }
}
