using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSlime : MonoBehaviour
{
    Rigidbody2D enemyRigidBody;
    CircleCollider2D bodyCollider;
    BoxCollider2D ledgeCollider;
    [SerializeField] float movementSpeed = 1f;
    void Start()
    {
         enemyRigidBody = GetComponent<Rigidbody2D>();
         bodyCollider = GetComponent<CircleCollider2D>();
         ledgeCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        enemyRigidBody.velocity = new Vector2(movementSpeed, 0f);
        
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
        
    //}

     private void OnTriggerExit2D()
     {
        changeDirection();
        
     }

     void changeDirection()
     {
         transform.localScale = new Vector2(-Mathf.Sign(enemyRigidBody.velocity.x), transform.localScale.y);
            movementSpeed = -movementSpeed;
     }
}
