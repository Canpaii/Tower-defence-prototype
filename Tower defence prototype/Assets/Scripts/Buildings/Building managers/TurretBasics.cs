using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBasics : MonoBehaviour
{
    public GameObject grid; // the exact grid this gameObject is on can be used later for selling this building to reset the grid
    public GameObject radiusUI;
    
    public int upgradeCost;
    public int sellWorth;
    public bool lastUpgrade;
    
    public TowerUiData UIData;
    public GameObject towerUpgrade;
    
    public EnemyManager enemyManager;
    private Currency currency;
    private BuildingLevelUp buildingLevelUp;
    

    protected void Start()
    {
        currency = Currency.Instance;
        enemyManager = EnemyManager.instance;
        buildingLevelUp = BuildingLevelUp.instance;
    }

    public void UpgradeTurret() // spawn upgraded variant of the turret 
    {
        if (!lastUpgrade && currency.currency >= upgradeCost)
        {
            currency.SubtractCurrency(upgradeCost);
            
            GameObject newTurret = Instantiate(towerUpgrade, transform.position, Quaternion.identity); 
            buildingLevelUp.activeBuilding = newTurret;
            Destroy(gameObject);
        }
    }
  
    public void SellBuilding()
    {
        currency.AddCurrency(sellWorth);
        grid.SetActive(true);
        Destroy(gameObject);
    }

    public void ShowRadius()
    {
        radiusUI.SetActive(true);
    }

    public void HideRadius()
    {
        radiusUI.SetActive(false);
    }
}
