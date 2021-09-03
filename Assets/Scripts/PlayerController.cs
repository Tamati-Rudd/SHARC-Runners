using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IPunObservable
{

    public float movementSpeed;
    private Rigidbody2D rb;
    public float jumpForce;
    public bool facingRight = true;
    public GameObject bulletpoint;
    public bool isDisabled = true;
    public TMP_Text username;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Transform respawnPoint;

    private Animator anim;
    private SpriteRenderer sr;
    private SpringJoint2D sj;

    PhotonView PV;
    Camera cam;
    //ability 
    public float speedTimer;
    public bool activateSpeed;
    public Collectable collectableMeter;//Access the collectable script
    private bool hasBulletFlipped = false;

    private bool canDoubleJump;
    public float wallJumpTime = 0.2f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.5f;
    bool isWallSliding = false;
    RaycastHit2D wallCheckhit;
    float jumpTime;
    bool isOnWall;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = GetComponentInChildren<Camera>();
        sr = GetComponent<SpriteRenderer>();
        sj = GetComponent<SpringJoint2D>();
        
        //destroy other player's rigidbody
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(sj);
            Destroy(rb);
        }


        speedTimer = 0;
        activateSpeed = false;
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    }


    // Update is called once per frame
    void Update()
    {

        if (!PV.IsMine)
        {
            return;
        }

        //move spawned character

        //Movement left & right
        
        if (isDisabled == false)
        {
            var moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
        }
       

        //check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);


        //jumping
        //Check if player can do double jump
        if (isGrounded)
        {
            canDoubleJump = true;
        }
        if (Input.GetButtonDown("Jump") && isDisabled == false)
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

        //flip player facing direction
        if (rb.velocity.x < 0 && facingRight && isDisabled == false)
        {

            Flip();

           // sr.flipX = true;
          //  facingRight = false;
            
        }
        else if (rb.velocity.x > 0 && !facingRight && isDisabled == false)
        {
             Flip();
          // sr.flipX = false;
          //  facingRight = true;

        }

        //Wall Jump

        if (facingRight)
        {
            wallCheckhit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, whatIsGround);
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

        //check for animation 
        anim.SetFloat("moveSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));//rb.velocity.x)//);
        anim.SetBool("isGrounded", isGrounded);

        //Press the ability button
        if (Input.GetButtonDown("Fire2"))
        {
            activateSpeed = collectableMeter.SetSpeed();//Call set speed to activate boost
            if (activateSpeed)
                SpeedAbility();
        }
        //If true start the time limit of the ability
        if (activateSpeed)
        {
            speedTimer += Time.deltaTime;
            //this will reset the speed once speedTimer is 3
            if (speedTimer >= 3)
            {
                movementSpeed = 9;
                speedTimer = 0;
                activateSpeed = false;
            }
        }


    }

    
    //Change the speed of the character
    public void SpeedAbility()
    {
        collectableMeter.UpdateCoins();
        movementSpeed = 20;
    }
    public void Flip()
    {

        facingRight = !facingRight;

        this.transform.localScale = new Vector3(transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z);

        // username.transform.localScale = new Vector3(transform.localScale.x * -1,
        //  transform.localScale.y,
        //transform.localScale.z);

        //username.transform.Rotate(0f, 180f, 0);

        PV.RPC("FlipRPC", RpcTarget.All);

        bulletpoint.transform.Rotate(0f, 180f, 0);

        //username.transform.Rotate(0f, 180f, 0);
        //sr.flipX = true;
        //cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));

    }

    [PunRPC]
    void FlipRPC()
    {
        if (!PV.IsMine)
        {
            //username.transform.Rotate(0f, 180f, 0);
            username.transform.localScale = new Vector3(transform.localScale.x * -1,
              transform.localScale.y,
           transform.localScale.z);
        }

    }

    ////Runs whenever the player has died (e.g. on collision with enemy)
    //[PunRPC]
    //void killPlayerRPC(Transform respawnPoint)
    //{
    //    PV.transform.position = respawnPoint.transform.position;
    //}

    //Runs when a race is ended, saving the winner and loading all players into the PostGame scene
    [PunRPC]
    void EndRaceRPC(string winnerName)
    {
        WinnerRecord win = GameObject.FindGameObjectWithTag("WinRecord").GetComponent<WinnerRecord>();
        win.updateWinnerName(winnerName);
        SceneManager.LoadScene("PostGame");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    //Check for player Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if target object is a wall
        if (collision.gameObject.tag == "Wall")
        {
            isOnWall = true;
        }
        //Check if the target object is an enemy
        else if (collision.gameObject.tag == "Enemy") 
        {
            //Move player back to the respawn point
            PV.transform.position = respawnPoint.transform.position;
        }
    }
}

