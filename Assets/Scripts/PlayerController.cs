using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;


public class PlayerController : MonoBehaviour, IPunObservable
{
    public PhotonView PV;
    public float SmoothingDelay = 0.5f;
    bool observed = false;

    [Header("Player Movement")]
    public float movementSpeed;
    public float jumpForce;
    private bool isGrounded;
    public Transform groundCheck;
    public bool facingRight = true;
    public LayerMask whatIsGround;
    public bool isDisabled = true;
    public TMP_Text username;
    public Transform respawnPoint;

    [Header("Race Status")]
    public bool raceStarted = false;
    public bool raceFinished = false;

    [Header("Sabotage")]
    public Light2D sabotageIndicator;
    public float disableTimer = 0;

    [Header("Ability")]
    public AbilityController aController;
    public Collectable collectableMeter;//Access the collectable script
    private bool hasBulletFlipped = false;

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

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        SabotageController sabController = GameObject.FindGameObjectWithTag("SabotageController").GetComponent<SabotageController>();
        sabController.addPlayerController(this);

        //For observing the player's movement and sending it across the photon network
        foreach (Component observedComponent in this.PV.ObservedComponents)
        {
            if (observedComponent == this)
            {
                observed = true;
                break;
            }
        }

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

        //use default movement speed
        ResetSpeed();

        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (!PV.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
            return;
        }
        else if (isDisabled && raceStarted)
        {
            disableTimer += Time.deltaTime;
            if (disableTimer >= 3) //enable the player and allow the rest of update to happen
            {
                PV.RPC("EnablePlayerRPC", RpcTarget.All);
                rb.constraints = RigidbodyConstraints2D.None;
                PV.transform.rotation = Quaternion.identity;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                disableTimer = 0;
            }
            else //return, disallowing the update
            {
                rb.constraints = RigidbodyConstraints2D.None;
                PV.transform.rotation = Quaternion.identity;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                return;
            }

        }

        MovePlayer();

        PlayerJumps();

        FlipPlayer();

        WallJump();

        SetPlayerAnimation();

        if (!raceFinished)
            PlayerAbility();

    }

    private void PlayerAbility()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            aController.runAbility(2);//check if the player has collected 8 crystals
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

    private void FlipPlayer()
    {
        if (rb.velocity.x < 0 && facingRight && isDisabled == false) //flip player to the right direction
        {
            Flip();
        }
        else if (rb.velocity.x > 0 && !facingRight && isDisabled == false) //flip player to the left direction
        {
            Flip();
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

    private void PlayerJumps()
    {
        //check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);
        //check if the player can do the double jump
        if (isGrounded)
        {
            canDoubleJump = true;
        }
        if (Input.GetButtonDown("Jump") && isDisabled == false) //check if the jump button is pressed and player can move
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

    private void MovePlayer()
    {
        if (isDisabled == false)
        {
            //Movement left & right
            var moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
        }
    }

    //Default movement speed
    public void ResetSpeed()
    {
        movementSpeed = 13.5f;
    }

    //Change the speed of the character
    public void pickAbility(int n)
    {
        if (n == 1)
        {
            collectableMeter.UpdateCoins();
            movementSpeed = 20;
            
        }
        else if (n == 2)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isGrounded = false;
            canDoubleJump = true;
            collectableMeter.UpdateCoins();
        }
        else if (n == 3)
        {
            isGrounded = true;
        }

        //return true;//Unit Testing
    }

        public void Flip()
    {

        facingRight = !facingRight;
        //flip the player localscale by multiplying -1
        this.transform.localScale = new Vector3(transform.localScale.x * -1, 
       transform.localScale.y,
       transform.localScale.z);

        PV.RPC("FlipRPC", RpcTarget.All);

        bulletpoint.transform.Rotate(0f, 180f, 0); //flip the bullet point when the player flip

    }

    //Disables the player
    [PunRPC]
    void DisablePlayerRPC()
    {
        isDisabled = true;
        sabotageIndicator.enabled = true;
    }

    //Re-enables the player
    [PunRPC]
    void EnablePlayerRPC()
    {
        isDisabled = false;
        sabotageIndicator.enabled = false;
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

    //Runs when the player finishes the race, entering them into a spectator state
    //In a spectator state, a player can still move on the map, but is invisible to other players and cannot interact with certain features (e.g. sabotage/crystal pickup, ability, shooting)
    [PunRPC]
    void Finished()
    {
        PV.transform.position = new Vector3(PV.transform.position.x, PV.transform.position.y, -100);
        SabotageController sabController = GameObject.FindGameObjectWithTag("SabotageController").GetComponent<SabotageController>();
        sabController.removePlayerController(this);
        raceFinished = true;
        movementSpeed = 20;
        jumpForce = 30;
    }

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            
        }
        else
        {
            //Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }

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
            if (!(raceFinished)) //Move player back to the respawn point
                PV.transform.position = respawnPoint.transform.position;
            else //Move player back to respawn point while spectating. Done to prevent disruption of the race by spectators
                PV.transform.position = new Vector3(respawnPoint.transform.position.x, respawnPoint.transform.position.y, -100);
        }
    }
}