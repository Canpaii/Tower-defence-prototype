using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcTurret : TowerBehaviour
{
    [SerializeField] private Transform arcTower;
    [SerializeField] private Transform rotatingGunHolder;
    [SerializeField] private Transform gunShootPoint;
    
    [SerializeField] private float rotationSmooth;
    [SerializeField] private float attackWaitTime;
    
    [SerializeField] private int damage;
    [SerializeField] private float arcLifeTime; 
    private float timer;
    
    public GameObject bulletPrefab;
    
    // Update is called once per frame
    void Update()
    {
        enemy = FindEnemy();
        
        if (enemy && isActive)
        {
            RotateGunHolder();
                               
            Shoot();
        }
    }
    
    void Shoot()
    {
        timer += Time.deltaTime;
        if (timer > attackWaitTime)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, gunShootPoint.position, gunShootPoint.rotation);
            
            currentBullet.GetComponent<ArcBullet>().damage = damage;
            currentBullet.GetComponent<ArcBullet>().lifeTime = arcLifeTime;
            timer = 0;
        }
    }
    #region RotationManager
    
    void RotateGunHolder()
    {
        Vector3 direction = enemy.position - arcTower.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotatingGunHolder.rotation = Quaternion.Slerp(rotatingGunHolder.rotation, rotation, rotationSmooth);
    }
    #endregion
}
