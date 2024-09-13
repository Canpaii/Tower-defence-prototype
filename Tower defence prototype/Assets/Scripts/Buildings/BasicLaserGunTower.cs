using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BasicLaserGunTower : TowerBehaviour
{
    [SerializeField] private Transform laserTower;
    [SerializeField] private Transform rotatingGunHolder;
    [SerializeField] private Transform guns;
    [SerializeField] private Transform gunLeftShootPoint, gunRightShootPoint;
    
    [SerializeField] private float rotationSmooth;
    [SerializeField] private float attackWaitTime;
    private float timer;
    
    private bool leftShot = false;
    private bool rightShot = false;
    
    public GameObject bulletPrefab;
    
    // Update is called once per frame
    void Update()
    {
        enemy = FindEnemy();
        
        if (enemy != null)
        {
            RotateGunHolder();
            RotateGun();
                               
            Shoot();
        }
    }
    
    void Shoot()
    {
        timer += Time.deltaTime;
        if (!leftShot && timer > attackWaitTime)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, gunLeftShootPoint.position, gunLeftShootPoint.rotation);
            
            currentBullet.GetComponent<LaserBullet>().target = enemy;
            
            leftShot = true;
            rightShot = false;
            timer = 0;
        }
        else if (!rightShot && timer > attackWaitTime)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, gunRightShootPoint.position, gunRightShootPoint.rotation);
            
            currentBullet.GetComponent<LaserBullet>().target = enemy;
            
            leftShot = false;
            rightShot = true;
            timer = 0;
        }
    }
   
    #region RotationManager
    
    void RotateGunHolder()
    {
        Vector3 direction = enemy.position - laserTower.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotatingGunHolder.rotation = Quaternion.Slerp(rotatingGunHolder.rotation, rotation, rotationSmooth);
    }

    void RotateGun()
    {
        Vector3 direction = enemy.position - guns.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        guns.rotation = Quaternion.Slerp(guns.rotation, rotation, rotationSmooth);
    } 
    #endregion
}
