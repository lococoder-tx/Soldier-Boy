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
    [SerializeField] float rollSpeed = 10f;
    [SerializeField] int health = 1;
    
    //State
    bool isAlive = true;
    bool playerHasHorizontalSpeed;
    bool playerhasVericalSpeed;
    bool isJumping = false;
    bool canMove = true;
    [SerializeField] bool invulnerable = false;
    
    //cached
    Rigidbody2D playerRigidBody;
    Animator myAnimator;
    CapsuleCollider2D  bodyCollider;
    BoxCollider2D feetCollider;
    float currentGravity;

    void Start()    
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        currentGravity = playerRigidBody.gravityScale;
    }

    void Update()
    {
       if(isAlive && canMove)
       {
        //Debug.Log(playerRigidBody.velocity);
    
        //Bools for state check
        playerHasHorizontalSpeed =  Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerhasVericalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        
        Run();
        Tumble();
        Jump();
        ClimbLadder();
        
        //Debug.Log(isJumping);
       }
    
    }

    private void Run()
    {
       float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //-1 to 1
       Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidBody.velocity.y);
       playerRigidBody.velocity = playerVelocity;
       FlipSprite();
       myAnimator.SetBool("isRunning", playerHasHorizontalSpeed && !playerhasVericalSpeed);
    }

    private void Tumble()
    {
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Hazards")))
            return;
        
        if(Input.GetKeyDown(KeyCode.Z))
        {
            canMove = false;
            float direction = transform.localScale.x;
            myAnimator.SetTrigger("tumble");
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            playerRigidBody.velocity = new Vector2(rollSpeed * direction, playerRigidBody.velocity.y);
            StartCoroutine(invulnerablePeriod(.7f));
        }
    }
    private void ClimbLadder()
    {
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbable")))
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
            //Debug.Log(playerRigidBody.velocity);
            myAnimator.SetBool("isClimbing", playerhasVericalSpeed);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0f);
        }
        
    }

    private void Jump()
    {
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Hazards")))
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        DamageDealer damageDealer = col.gameObject.GetComponent<DamageDealer>();
        
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && damageDealer)
        {
                DamagePlayer(damageDealer.getDamage(), true);
            
        }
    }

    private void Die()
    {
        if(isAlive)
        {
        isAlive = false;
        myAnimator.SetBool("isDead", true);
        //Debug.Log(-Mathf.Sign(playerRigidBody.velocity.x) * 25f);
        playerRigidBody.velocity = new Vector2(-transform.localScale.x * 25f, 10f);
        }
    }
    
    public void DamagePlayer(int damage, bool pushback)
    {
      if(!invulnerable)
      {
        health -= damage;
        if(health <= 0)
        {
                Die();
        }
        else
        {
            canMove = false;
            myAnimator.SetTrigger("damaged");
            if(pushback)
                playerRigidBody.velocity = new Vector2(-transform.localScale.x * 15f, playerRigidBody.velocity.y);
            StartCoroutine(invulnerablePeriod(2f));
        }
      } 
    }
    
    void changeCanMoveTrue()
    {
        canMove = true;
    }

    void changeCanMoveFalse()
    {
        canMove = false;
    }

    IEnumerator invulnerablePeriod(float time)
    {
        invulnerable = true;
        yield return new WaitForSeconds(time);
        invulnerable = false;
    }

    
}

