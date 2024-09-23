using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BasicLaserGunTower : TowerBehaviour
{
    [SerializeField] private Transform laserTower;
    [SerializeField] private Transform rotatingGunHolder;
    [SerializeField] private Transform[] guns;
    [SerializeField] private Transform gunLeftShootPoint, gunRightShootPoint;
    
    [SerializeField] private float rotationSmooth;
    [SerializeField] private float attackWaitTime;
    
    [SerializeField] private int damage;
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
            currentBullet.GetComponent<LaserBullet>().damage = damage;
            
            leftShot = true;
            rightShot = false;
            timer = 0;
        }
        else if (!rightShot && timer > attackWaitTime)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, gunRightShootPoint.position, gunRightShootPoint.rotation);
            
            currentBullet.GetComponent<LaserBullet>().target = enemy;
            currentBullet.GetComponent<LaserBullet>().damage = damage;
            
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
        for (int i = 0; i < guns.Length; i++)
        {
            Vector3 direction = enemy.position - guns[i].position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            guns[i].rotation = Quaternion.Slerp(guns[i].rotation, rotation, rotationSmooth);
        }
    } 
    #endregion
}
