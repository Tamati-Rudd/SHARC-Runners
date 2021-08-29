using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour, IPunObservable
{
    public float movementSpeed;
    private Rigidbody2D rb;
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private Animator anim;
    private SpriteRenderer sr;

    PhotonView PV;

    //ability 
    public float speedTimer;
    public bool activateSpeed;
    public Collectable collectableMeter;//Access the collectable script

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        AddObservable();

        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }

       
        speedTimer = 0;
        activateSpeed = false;

    }

    private void AddObservable()
    {
        if (!PV.ObservedComponents.Contains(this))
        {
            PV.ObservedComponents.Add(this);
        }
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
        var moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);

        //check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);

        //jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        //flip player facing direction
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
           // transform.Rotate(0f, 180f, 0f);
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        //check for animation 
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
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

    //Sending Data that needs to be seen by other players across the network
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if data needs to be sent
        if (stream.IsWriting)
        {
            //sending flipping information across the network
            stream.SendNext(sr.flipX);
        }
        //if data needs to be received
        else
        {
            sr.flipX = (bool)stream.ReceiveNext();
        }

    }

    //Change the speed of the character
    public void SpeedAbility()
    {
        collectableMeter.UpdateCoins();
        movementSpeed = 20;
    }
}

