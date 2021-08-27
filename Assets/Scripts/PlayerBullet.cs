using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
** PlayerBullet Script
** This carries out the player's bullet behaviour
**/
public class PlayerBullet : MonoBehaviour
{
   public float speed = 20f;
   public Rigidbody2D rb;
    

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        if(hitInfo.gameObject.CompareTag("Environment") || hitInfo.gameObject.CompareTag("Enemy"))
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
