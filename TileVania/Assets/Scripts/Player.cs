using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float climbSpeed = 1f;
    //State
    bool isAlive = true;

    //Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;
    Collider2D myFeetCollider2D;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        ClimbLadder();
        Run();
        Jump();
    }

    void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");

        if (controlThrow<0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            myAnimator.SetBool("isRunning", true);
        }
        else if (controlThrow>0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }

        Vector2 playerVelocity = new Vector2(runSpeed * controlThrow, myRigidBody.velocity.y);

        //print(playerVelocity);
        myRigidBody.velocity = playerVelocity;
    }

    void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0, jumpSpeed);
            myRigidBody.velocity += jumpVelocity;
        }
    }

    private void ClimbLadder()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidBody.gravityScale = 1;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        myRigidBody.gravityScale = 0;
        Physics.gravity = new Vector3(0, 0, 0);
        myAnimator.SetBool("isClimbing", true);

        float controlThrow = Input.GetAxis("Vertical");

        Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, climbSpeed * controlThrow);

        myRigidBody.velocity = playerVelocity;
    }

}

