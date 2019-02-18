using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{ 
   //config   
    [SerializeField] float runSpeed= 6f;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] float climbSpeed = 3f;
    
    //State
    bool isAlive = true;
    bool playerHasHorizontalSpeed;
    bool playerhasVericalSpeed;
    bool isJumping = false;
    
    //cached
    Rigidbody2D playerRigidBody;
    Animator myAnimator;
    Collider2D collider2D;

    float currentGravity;

    void Start()    
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        currentGravity = playerRigidBody.gravityScale;
    }

    void Update()
    {
        //Debug.Log(playerRigidBody.velocity);
        
        //Bools for state check
        playerHasHorizontalSpeed =  Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerhasVericalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        
        Run();
        Jump();
        ClimbLadder();
        Debug.Log(isJumping);
    
    }

    private void Run()
    {
       float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //-1 to 1
       Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidBody.velocity.y);
       playerRigidBody.velocity = playerVelocity;
       FlipSprite();
       myAnimator.SetBool("isRunning", playerHasHorizontalSpeed && !playerhasVericalSpeed);
    }

    private void ClimbLadder()
    {
        if(!collider2D.IsTouchingLayers(LayerMask.GetMask("Climbable")))
        {
            myAnimator.SetBool("isClimbing", false);
            playerRigidBody.gravityScale = currentGravity;
            return;
        }
        
        if(CrossPlatformInputManager.GetButton("Vertical"))
        {
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, controlThrow * climbSpeed);
            playerRigidBody.velocity = climbVelocity;
            playerRigidBody.gravityScale = 0f;
            Debug.Log(playerRigidBody.velocity);
            myAnimator.SetBool("isClimbing", playerhasVericalSpeed);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
        }
        
    }

    private void Jump()
    {
        if(!collider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
             return;
        }

        if(CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 velocityToAdd = new Vector2(0f, jumpSpeed);
            playerRigidBody.velocity += velocityToAdd;
            myAnimator.SetTrigger("Jump");
        }
    }

    private void FlipSprite()
    {
        
        if(playerHasHorizontalSpeed)
        {
            //Mathf.Sign -> returns 1 if 0 or > 0, -1 otherwise
            transform.localScale = new Vector3(Mathf.Sign(playerRigidBody.velocity.x), transform.localScale.y);
        }
    }

    

}

