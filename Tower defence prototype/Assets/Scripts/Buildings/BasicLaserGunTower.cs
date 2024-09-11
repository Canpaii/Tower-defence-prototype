using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BasicLaserGunTower : MonoBehaviour
{
    [SerializeField] private Transform laserTower;
    [SerializeField] private Transform rotatingGunHolder;
    [SerializeField] private Transform guns;
    [SerializeField] private Transform gunLeftShootPoint, gunRightShootPoint;
    [SerializeField] private Transform enemy;
    
    [SerializeField] private float rotationSmooth;
    [SerializeField] private float attackWaitTime;
    private float timer;
    
    private bool leftShot = false;
    private bool rightShot = false;
    
    public GameObject bulletPrefab;
    public float range;
    void Start()
    {
        GameObject enemyHolder = GameObject.FindGameObjectWithTag("Enemy");
        enemy = enemyHolder.transform;
    }

    // Update is called once per frame
    void Update()
    {
       RangeCalc();
    }

    void RangeCalc()
    {
        
        float distance = Vector3.Distance(laserTower.position, enemy.position);

        if (distance < range)
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
            Instantiate(bulletPrefab, gunLeftShootPoint.position, gunLeftShootPoint.rotation);
            
            leftShot = true;
            rightShot = false;
            timer = 0;
        }
        else if (!rightShot && timer > attackWaitTime)
        {
            Instantiate(bulletPrefab, gunRightShootPoint.position, gunRightShootPoint.rotation);
            
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
