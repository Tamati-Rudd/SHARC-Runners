using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
** EnemyBullet Script
** This carries out the enemies bullet behaviour
**/
public class EnemyBullet : MonoBehaviour
{
    private float bulletSpeed; 
    private Rigidbody2D rb; 
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletSpeed = 20f;

    }

    public void Update()
    {   
        rb.AddForce(transform.right * bulletSpeed);
    }

    //Method for detecting collision with player and or environment
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            DestroyProjectile();
        }
        else
        {
            Invoke("DestroyProjectile", 2f);
        }
    }

    //Method for destroying the enemies bullet
    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
