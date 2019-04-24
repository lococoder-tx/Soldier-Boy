using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
      [SerializeField] int damage = 1;
      private void OnCollisionStay2D(Collision2D col)
      {
          if(col.gameObject.layer == 10)
          {
              Player player = col.gameObject.GetComponent<Player>();
              player.DamagePlayer(damage, false);

          }
          
      }
}
