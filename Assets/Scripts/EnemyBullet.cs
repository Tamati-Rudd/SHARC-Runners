using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
** EnemyBullet Script
** This carries out the enemies bullet behaviour
**/
public class EnemyBullet : MonoBehaviour
{
    public float speed; 
    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2( player.position.x, player.position.y);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    //Method for detecting collision with player and or environment
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Environment"))
        {
            DestroyProjectile();
        }
    }

    //Method for destroying the enemies bullet
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
