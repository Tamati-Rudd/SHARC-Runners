using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/*
** EnemyAI Script
** This script carries out the behaviour of the enemy:
** Moving forward
** Moving towards player
** Turning around
** Shooting at player
*/

public class EnemyAI : MonoBehaviour
{
  
    public float moveSpeed; //Movement speed
    public Transform sightStart, sightEnd; //Enemy line of site for obstacles
    private bool obstacleCollision; //Detect collision with an obstacle or environment
    public bool needsCollision; 
    public float jumpForce;
    private float fireRate; //Fire rate of Enemy
    public float startingFireRate; //Starting fire rate
    public GameObject projectile; //For the bullet
    public Animator animator;
    public Rigidbody2D rb;
    

    public void Start()
    {
        fireRate = startingFireRate;
        jumpForce = Random.Range(250, 350);
        
    }

    private void Update()
    {     
       Move();
       ShootPlayer();
    }

    //Method for moving constantly forward 
    private void Move()
    {

        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0) * moveSpeed;
        InvokeRepeating("Jump", 2f, 3f);
        Flip();
    }

    //Method for turning around if enemy is near a wall
    private void Flip()
    {
        obstacleCollision = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Ground"));

        if(obstacleCollision == needsCollision)
        {
          this.transform.localScale = new Vector3(transform.localScale.x * -1,
          transform.localScale.y,
          transform.localScale.z);
          //animator.SetBool("isTurning", true);
        }
    }

    //Method for Jumping Enemy
    /*private void Jump()
    { 
      rb.AddForce(new Vector2(0, jumpForce));
    }*/

    //Method for shooting at player
    private void ShootPlayer()
    {
      if(fireRate <= 0)
     {   
      animator.SetBool("isAttacking", true);
      animator.Play("Enemy_Attacking");
      Instantiate(projectile, transform.position, Quaternion.identity);
      fireRate = startingFireRate;
    }
    else
    {
      fireRate -= Time.deltaTime;
    }
    animator.SetBool("isAttacking", false);
    }

}

