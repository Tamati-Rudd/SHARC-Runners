using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
** PlayerBullet Script
** This carries out the player's bullet behaviour
**/
public class PlayerBullet : MonoBehaviour
{
   public float speed = 20f; // speed of bullet
   public Rigidbody2D rb;
    

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    //Collision method for the bullet colliding with other game objects
    private void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        if(hitInfo.gameObject.CompareTag("Ground") || hitInfo.gameObject.CompareTag("Enemy"))
        {
            DestroySelf();
        }
    }
    
    //Destroys itself once bullet touches another game object.
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
