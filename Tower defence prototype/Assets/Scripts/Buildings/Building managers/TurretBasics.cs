using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBasics : MonoBehaviour
{
    protected GameObject grid; // the exact grid this gameObject is on can be used later for selling this building to reset the grid
    
    public int upgradeCost;
    public int sellWorth;
    
    public EnemyManager enemyManager;
    
    public TowerUiData[] UIData;

    public GameObject towerUpgrade;
    private Currency currency;

    protected void Start()
    {
        currency = Currency.Instance;
        enemyManager = EnemyManager.instance;
    }

    public void UpgradeTurret() // spawn upgraded variant of the turret 
    {
        currency.SubtractCurrency(upgradeCost);
        
        Instantiate(towerUpgrade, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
  
    public void SellBuilding()
    {
        currency.AddCurrency(sellWorth);
        Destroy(gameObject);
    }
}
