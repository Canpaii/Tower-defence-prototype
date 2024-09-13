using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float radius;
    public GameObject levelUpUI;
    
    private GameObject[] towers;
    public Transform enemy;
   public Transform FindEnemy()
   {
       Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);
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
