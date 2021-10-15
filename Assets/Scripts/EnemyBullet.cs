using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
** EnemyBullet Script
** This carries out the enemies bullet behaviour
**/
public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed; //The Speed of the enemy bullet
    Rigidbody2D rb;

    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       rb.velocity = -transform.up * bulletSpeed;
       Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
