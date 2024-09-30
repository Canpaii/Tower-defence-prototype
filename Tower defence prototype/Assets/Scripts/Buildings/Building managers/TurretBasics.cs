using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBasics : MonoBehaviour
{
    public GameObject levelUpUI;
    [SerializeField] protected TowerUpgradesSO[] upgrades;
    
    public int currentUpgrade = 0;
    protected GameObject grid; // the exact grid this gameObject is on can be used later for selling this building
    
    public EnemyManager enemyManager;
    
    public TowerUiData[] UIData;
    public void UpgradeTurret() // call this method to change array index for upgrades 
    {
        if (currentUpgrade < upgrades.Length - 1)
        {
            currentUpgrade++;
            ApplyUpgrade(currentUpgrade);
        }
    }
     protected virtual void ApplyUpgrade(int level) // apply the data from array to stats 
    {
        
    }

    public void SellBuilding()
    {
        
    }
    
}
