using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float radius;
    private GameObject grid; // the exact grid this gameObject is on can be used later for selling this building
    
    public GameObject levelUpUI;
    public EnemyManager enemyManager;
    
    public Transform enemy;
   public Transform FindEnemy()
   {
       Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, radius, enemyLayerMask );
       Transform closestTarget = null;
       float maxDistance = radius;
       
       foreach (Collider enemyCollider in nearbyEnemies)
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
