using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script contains the player controller for the player in tutorial
public class tutorialPlayer : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed;
    public float jumpForce;
    private bool isGrounded;
    public Transform groundCheck;
    public bool facingRight = true;
    public LayerMask whatIsGround;

    [Header("Double Jump & Wall Jump")]
    private bool canDoubleJump;
    public float wallJumpTime = 0.2f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.5f;
    bool isWallSliding = false;
    RaycastHit2D wallCheckhit; //drawing raycast to check for a wall
    float jumpTime;
    bool isOnWall;

    [Header("Gameobject")]
    private Animator anim;
    private SpriteRenderer sr;
    private SpringJoint2D sj;
    Camera cam;
    private Rigidbody2D rb;
    public GameObject bulletpoint;

    private int counter;//Counter for ability

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>();
        sr = GetComponent<SpriteRenderer>();
        sj = GetComponent<SpringJoint2D>();
        counter = 0;
    }

    void Update()
    {
        MovePlayer();

        PlayerJumps();

        WallJump();

        FlipPlayer();

        SetPlayerAnimation();

        ActivateAbility();
    }

    private void PlayerJumps()
    {
        //check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);
        //check if the player can do the double jump
        if (isGrounded)
        {
            canDoubleJump = true;
        }
        if (Input.GetButtonDown("Jump")) //check if the jump button is pressed and player can move
        {
            if (isGrounded || isWallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else
            {
                if (canDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    canDoubleJump = false;
                }

            }
        }
    }
    private void WallJump()
    {
        if (facingRight)
        {
            wallCheckhit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, whatIsGround); //does raycasting to detect the wall
        }
        else
        {
            wallCheckhit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, whatIsGround);
        }


        if (wallCheckhit && !isGrounded && Input.GetAxisRaw("Horizontal") != 0 && isOnWall)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
            isOnWall = false;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
            isOnWall = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }
    }

    private void MovePlayer()
    {
        //Movement left & right
        var moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
    }

    public void Flip()
    {

        facingRight = !facingRight;
        //flip the player localscale by multiplying -1
        this.transform.localScale = new Vector3(transform.localScale.x * -1,
       transform.localScale.y,
       transform.localScale.z);

        bulletpoint.transform.Rotate(0f, 180f, 0); //flip the bullet point when the player flip

    }

    private void FlipPlayer()
    {
        if (rb.velocity.x < 0 && facingRight) //flip player to the right direction
        {
            Flip();
        }
        else if (rb.velocity.x > 0 && !facingRight) //flip player to the left direction
        {
            Flip();
        }
    }
    private void SetPlayerAnimation()
    {
        anim.SetFloat("moveSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal"))); //running animation
        anim.SetBool("isGrounded", isGrounded); //jumping animation
        anim.SetBool("isOnWall", isWallSliding); //wall jump animation
        if (facingRight)
        {
            anim.SetBool("facingRight", facingRight); //wall jump facing right
        }
        else if (!facingRight)
        {
            anim.SetBool("facingLeft", !facingRight); // wall jump facing left
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if target object is a wall
        if (collision.gameObject.tag == "Wall")
        {
            isOnWall = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if target object is a collectable
        if (collision.CompareTag("Collectable"))
        {
            counter++;
            Destroy(collision.gameObject);
            Debug.Log("counter: " + counter);
        }
    }

    private void ActivateAbility()
    {
        if (counter >= 8 && Input.GetButtonDown("Fire2"))
        {
            movementSpeed = 20;
        }
    }
}
