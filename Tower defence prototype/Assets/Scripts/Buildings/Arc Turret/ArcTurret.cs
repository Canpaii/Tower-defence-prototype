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
    [SerializeField] private int arcLifeTime; 
    private float timer;
    
    public GameObject bulletPrefab;
    
    // Update is called once per frame
    void Update()
    {
        enemy = FindEnemy();
        
        if (enemy)
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
            
            currentBullet.GetComponent<LaserBullet>().target = enemy;
            currentBullet.GetComponent<LaserBullet>().damage = damage;
            
            timer = 0;
        }
    }
   
    protected override void ApplyUpgrade(int level)
    {
        radius = upgrades[level].range;
        attackWaitTime = upgrades[level].attackWaitTime;
        damage = upgrades[level].damage;
        
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