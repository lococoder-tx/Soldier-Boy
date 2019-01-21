using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{ 
      
    [SerializeField] float runSpeed= 5f;
    Rigidbody2D playerRigidBody;

    void Start()    
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    private void Run()
    {
       float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //-1 to 1
       Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidBody.velocity.y);
       playerRigidBody.velocity = playerVelocity;
       FlipSprite();
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed =  Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        
        if(playerHasHorizontalSpeed)
        {
            
            //Mathf.Sign -> returns 1 if 0 or > 0, -1 otherwise
            transform.localScale = new Vector3(Mathf.Sign(playerRigidBody.velocity.x), transform.localScale.y);
        }
    }

}

