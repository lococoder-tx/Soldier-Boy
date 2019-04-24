using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health = 1;
    [SerializeField] protected int score = 10;
    public void DamageEnemy(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
                Die();
        }
    }

    public void Die()
    {
        FindObjectOfType<GameSession>().changeScore(score);
        Destroy(gameObject);
    }
    
}
