using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask hitableLayer;
    
    [SerializeField] private Transform laserTower;
    [SerializeField] private Transform rotatingGunHolder;
    [SerializeField] private Transform guns;
    [SerializeField] private Transform gunLeftShootPoint, gunRightShootPoint;
    
    [SerializeField] private float rotationSmooth;
    [SerializeField] private float attackWaitTime;
   
    [SerializeField] private float timer;
    
    
    public GameObject bulletPrefab;
    
    [SerializeField] Transform target;
    void Update()
    {
        target = FindBuilding();
        
        if (target != null)
        {
            RotateGunHolder();
            RotateGun();
                               
            Shoot();
        }
    }
    
    void Shoot()
    {
        timer += Time.deltaTime;
        if (timer > attackWaitTime)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, gunLeftShootPoint.position, gunLeftShootPoint.rotation);
            currentBullet.GetComponent<TankBullet>().target = target; 
            
            timer = 0;
        }
    }
    #region FindBuilding

     public Transform FindBuilding() // find the closest target in an area around the object
        {
            Collider[] nearbyBuildings = Physics.OverlapSphere(transform.position, radius, hitableLayer );
            Transform closestTarget = null;
            float maxDistance = radius;
           
            foreach (Collider buildings in nearbyBuildings)
            { 
                float enemyDistance = Vector3.Distance(buildings.transform.position, transform.position);
                if (enemyDistance < maxDistance)
                {
                    closestTarget = buildings.transform; 
                    maxDistance = enemyDistance;
                }
            }
    
            if (nearbyBuildings.Length == 0)
            {
                maxDistance = radius;
                closestTarget = null;
            }
            return closestTarget;
        }

    #endregion
   
    
    #region RotationManager
    
    void RotateGunHolder()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotatingGunHolder.rotation = Quaternion.Slerp(rotatingGunHolder.rotation, rotation, rotationSmooth);
    }

    void RotateGun()
    {
        Vector3 direction = target.position - guns.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        guns.rotation = Quaternion.Slerp(guns.rotation, rotation, rotationSmooth);
    } 
    #endregion
}
