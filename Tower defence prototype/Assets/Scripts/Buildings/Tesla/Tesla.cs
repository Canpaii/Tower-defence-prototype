using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Timers;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tesla : TowerBehaviour
{
   public int maxBounces;
   public int damage;
   public int chainRange;
   public int attackWaitTime;

   private float timer;

   public LayerMask enemyLayerMask;
   public void Update()
   {
       Shoot();
   }

  private void Shoot()
   {
       timer += Time.deltaTime;

       if (timer > attackWaitTime)
       {
           Transform firstTarget = FindEnemy();  // Your targeting logic
           if (firstTarget != null) 
           { 
               ChainLightning(firstTarget, maxBounces);
           }
       }
   }
   void ChainLightning(Transform currentTarget, int bouncesLeft)
   {
       if (bouncesLeft <= 0) return;
   
       // Deal damage to the current target
       EnemyHealth enemyScript = currentTarget.GetComponent<EnemyHealth>();
       if (enemyScript != null)
       {
           enemyScript.TakeDamage(damage);
       }
   
       // Find nearby enemies to bounce to
       Collider[] enemiesInRange = Physics.OverlapSphere(currentTarget.position, chainRange, enemyLayerMask);
       foreach (var enemyCollider in enemiesInRange)
       {
           Transform newTarget = enemyCollider.transform;
           if (newTarget != currentTarget)
           {
               ChainLightning(newTarget, bouncesLeft - 1);
               break;  // Only bounce to one enemy at a time
           }
       }
   }

}
