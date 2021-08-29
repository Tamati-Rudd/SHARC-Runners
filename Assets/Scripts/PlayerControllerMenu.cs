using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControllerMenu : MonoBehaviour
{

    private Rigidbody2D rb;
    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private Animator anim;
    private SpriteRenderer sr;






    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
     



    }


    // Update is called once per frame
    void Update()
    {



        //move spawned character



        //check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, whatIsGround);



        //check for animation 
        anim.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);



    }


}

