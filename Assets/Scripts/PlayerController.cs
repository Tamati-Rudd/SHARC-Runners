using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using System;

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
    public Sabotagable sabotageCheck;
    public Light2D sabotageIndicator;
    public float[] sabotageTimers;
    public Light2D mapLight;

    [Header("Ability")]
    public AbilityController aController;
    public float jetSpeed = 15f;
    public bool jumpAbility;
    public int selectedCharacter;
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

        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");

        //Get Player's map light object
        Transform floorsTransform = GameObject.Find("Floors").transform;
        foreach (Transform transform in floorsTransform)
        {
            if (transform.tag == "MapLight")
                mapLight = transform.gameObject.GetComponent<Light2D>();
        }


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

        sabotageTimers = new float[3];
        sabotageTimers[0] = 0;
        sabotageTimers[1] = 0;
        sabotageTimers[2] = 0;
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
        else if (raceStarted && !raceFinished) //Check for sabotages
        {
            float timeSinceLastUpdate = Time.deltaTime;
            bool allowUpdate = sabotageCheck.checkSabotages(timeSinceLastUpdate);
            if (!allowUpdate)
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

    public void PlayerAbility()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            aController.RunAbility(selectedCharacter, false);//check if the player has collected 8 crystals
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
                if (jumpAbility)//enable the jump ability
                {
                    rb.velocity = new Vector2(rb.velocity.x, jetSpeed);
                }
                else if (canDoubleJump)
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
    public void PickAbility(int n, bool testing)
    {
        switch (n)
        {
            case 1://Speed ability activated
                movementSpeed = 20;
                break;

            case 2://Jetpack Ability
                jumpAbility = true;//enable the ability
                break;

            case 3://disable the ability
                jumpAbility = false;
                break;

            default:
                Console.WriteLine("No ability is found");
                break;
        }

         if (!testing)
            collectableMeter.UpdateCoins();//Reset the ability meter
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

    //Re-enables a player - runs when a Stasis Trap sabotage ends
    public void enablePlayer()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        PV.transform.rotation = Quaternion.identity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isDisabled = false;
    }

    //Activate a Sabotage: hinder the player, and set or add to the duration of that sabotage
    [PunRPC]
    void activateSabotage(int sabotageToActivate)
    {
        if (PV.IsMine)
        {
            switch(sabotageToActivate)
            {
                case 0: //Stasis Trap
                    isDisabled = true;
                    sabotageTimers[0] = 3;
                    break;
                case 1: //Blindness Trap
                    mapLight.pointLightOuterRadius = 0;
                    sabotageTimers[1] += 15;
                    break;
                case 2: //TO DO: Projectile Trap
                    break;
                default:
                    break;
            }
        }
    }

    //This RPC sets a player's sabotage indicator, based on a lightToUse parameter determined for each player, each frame
    [PunRPC]
    void setSabotageIndicator(int lightToUse)
    {
        switch (lightToUse)
        {
            case 0: //Stasis Trap - intense red glow
                sabotageIndicator.intensity = 10;
                sabotageIndicator.pointLightOuterRadius = 3f;
                sabotageIndicator.color = Color.red;
                sabotageIndicator.enabled = true;
                break;
            case 1: //Blindness Trap - white spotlight
                sabotageIndicator.intensity = 2;
                sabotageIndicator.pointLightOuterRadius = 3.5f;
                sabotageIndicator.color = Color.white;
                sabotageIndicator.enabled = true;
                break;
            case 2: //TO DO: Projectile Trap - likely orange
                break;
            default:
                sabotageIndicator.enabled = false;
                break;
        }
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
        sabotageCheck.deactivateSabotage(-1);
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