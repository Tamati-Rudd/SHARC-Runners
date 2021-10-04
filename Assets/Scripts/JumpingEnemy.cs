using System.Collections.Generic;
using UnityEngine;
//This contains the code for the jumping enemy.
public class JumpingEnemy : MonoBehaviour
{
    private float jumpForce;
    public bool isJumpingEnemy;
    private Animator myAnimator;

    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (isJumpingEnemy == true)
        StartCoroutine(JumpAttack ());
    }

    IEnumerator JumpAttack()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        jumpForce = Random.Range(500, 600); //Change values to change range of jump height.
        rb.AddForce(new Vector2(0, jumpForce));
        myAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds (1.5f);
        myAnimator.SetBool("isAttacking", false);
        StartCoroutine(JumpAttack());
    }
}
