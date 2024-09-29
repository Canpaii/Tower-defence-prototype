using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBehaviour : TurretBasics
{
    [SerializeField] private LayerMask enemyLayerMask;
    public float radius;
    
    public Transform enemy;
    
   public Transform FindEnemy()
   {
       Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, radius, enemyLayerMask ); //get all enemies inrange 
       Transform closestTarget = null;
       float maxDistance = radius;
       
       foreach (Collider enemyCollider in nearbyEnemies)// search for closest enemy
       { 
           float enemyDistance = Vector3.Distance(enemyCollider.transform.position, transform.position);
           if (enemyDistance < maxDistance)
           {
               closestTarget = enemyCollider.transform; 
               maxDistance = enemyDistance;
           }
       }

       if (nearbyEnemies.Length == 0)
       {
           maxDistance = radius;
           closestTarget = null;
           
       }
       return closestTarget;
    }
}
