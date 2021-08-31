using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed;
    private Rigidbody2D rb;
    public float jumpForce;
    public bool facingRight = true;
    public GameObject bulletpoint;
    public bool isDisabled = true;

    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private Animator anim;
    private SpriteRenderer sr;

    PhotonView PV;
    Camera cam;
    //ability 
    public float speedTimer;
    public bool activateSpeed;
    public Collectable collectableMeter;//Access the collectable script
    private bool hasBulletFlipped = false;

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
        

        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }


        speedTimer = 0;
        activateSpeed = false;

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
        if (Input.GetButtonDown("Jump") && isDisabled == false)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        this.transform.Rotate(0f, 180f, 0);
        //sr.flipX = true;
        cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));

    }
}

