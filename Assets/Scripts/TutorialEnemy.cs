using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    public float moveSpeed; //Movement speed
    public Transform sightStart, sightEnd; //Enemy line of site for obstacles
    private bool obstacleCollision; //Detect collision with an obstacle or environment
    public bool needsCollision; 
    public Animator animator;
    public Rigidbody2D rb;
    public int HP = 10; //Health Point of Enemy
    public GameObject gemPrefab;

    public void Start()
    {
    }

    private void Update()
    {     
       Move();
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

        if(obstacleCollision == needsCollision)
        {
          this.transform.localScale = new Vector3(transform.localScale.x * -1,
          transform.localScale.y,
          transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When enemy is struck by bullet
        if(collision.CompareTag("PlayerProjectile"))
        {
            HP--; //Health goes down 
            GenerateGem();

            if(HP <= 0)
            {
                Kill();
            }
        
        }
    }


    //Method for generating gems
    private void GenerateGem()
    {
       Instantiate(gemPrefab, transform.position, gemPrefab.transform.rotation);              
       
    }

    public void Kill()
    {
        Destroy(gameObject);
    }


}
