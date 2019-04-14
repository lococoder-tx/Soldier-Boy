using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damageInflicted = 1;
    
    public int getDamage()
    {
        return damageInflicted;
    }
    
}
