using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public float stoppingDistance; //Distance where enemy stops
    public float aggro; //Distance where enemy is triggered to move towards player
    public float retreatDistance; //Distance where enemy will retreat if player reaches this distance
    public Transform backLineOfSite, frontLineOfSite; //Distance for seeing players
    public Transform player; //Player Position
    public Transform sightStart, sightEnd; //Enemy line of site for obstacles
    private bool obstacleCollision; //Detect collision with an obstacle or environment
    private bool frontPlayerFound, backPlayerFound; //Detects collisions with players from back to front
    public bool needsCollision; 
    private float fireRate; //Fire rate of Enemy
    public float startingFireRate; //Starting fire rate
    public GameObject projectile; //For the bullet
    public Animator animator;

    void Start()
    {
        fireRate = startingFireRate;
    }

    void Update()
    {   
       frontPlayerFound = Physics2D.Linecast(sightStart.position, frontLineOfSite.position, 1 << LayerMask.NameToLayer ("Player"));
       backPlayerFound = Physics2D.Linecast(sightStart.position, backLineOfSite.position, 1 << LayerMask.NameToLayer ("Player"));
       
       if(frontPlayerFound)
       {
           animator.SetBool("isFollowing", true); 
           TargetPlayer();
       }
       else if(backPlayerFound)
       {
           animator.SetBool("isFollowing", true); 
            this.transform.localScale = new Vector3(transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);
            //this.transform.localScale = new Vector3((transform.localScale.x == 1) ? -1 : 1, 1, 1);
            TargetPlayer();
       }      
       else
       { 
          animator.SetBool("isFollowing", false); 
           Move();
       }
    }

    //Method for moving constantly forward 
    private void Move()
    {

        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0) * moveSpeed;
        Flip();
    }

    //Method for turning around if enemy is near a wall
    private void Flip()
    {
        obstacleCollision = Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Ground"));

        if (obstacleCollision)
        {
            Debug.Log("Flip me");
        }
        else
        {
            Debug.Log("no need to flip");
        }
        
        if(obstacleCollision == needsCollision)
        {
          this.transform.localScale = new Vector3(transform.localScale.x * -1,
          transform.localScale.y,
          transform.localScale.z);
          //animator.SetBool("isTurning", true);
        }
    }
    
    //Method for targeting and moving towards player
    private void TargetPlayer()
    {   
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);;
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance && distanceFromPlayer < aggro)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;

        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position,player.position, -moveSpeed * Time.deltaTime);
        }

        ShootPlayer();
    }

    //Method for shooting at player
    private void ShootPlayer()
    {
        if(fireRate <= 0)
        {   
            //animator.SetBool("isAttacking", true);
            //animator.Play("Enemy_Attacking");
         
            Instantiate(projectile, transform.position, Quaternion.identity);
            fireRate = startingFireRate;
        }
        else
        {
            fireRate -= Time.deltaTime;
        }
        //animator.SetBool("isAttacking", false);
    }

}

