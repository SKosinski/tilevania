using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed = 1f;
    [SerializeField] float jumpSpeed = 1f;
    //State
    bool isAlive = true;

    //Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        Run();
        if ( myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) )
        {
            Jump();
        }
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

        print(playerVelocity);
        myRigidBody.velocity = playerVelocity;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0, jumpSpeed);
            myRigidBody.velocity += jumpVelocity;
        }
    }
}
