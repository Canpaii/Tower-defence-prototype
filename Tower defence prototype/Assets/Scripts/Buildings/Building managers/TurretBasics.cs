using System;
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
    public TowerModels towerModels;

    private GameObject currentModel;
    private Currency currency;

    private void Start()
    {
        currency = Currency.Instance;
        currentModel = Instantiate(towerModels.allVariants[currentUpgrade], transform.position, Quaternion.identity); ;
    }

    public void UpgradeTurret() // call this method to change array index for upgrades 
    {
        if (currentUpgrade < upgrades.Length - 1 && currency.currency >= upgrades[currentUpgrade].upgradeCost)
        {
            currentUpgrade++;
            
            currency.SubtractCurrency(upgrades[currentUpgrade].upgradeCost);

            ChangeModel();
            ApplyUpgrade(currentUpgrade);
        }
    }
    protected virtual void ApplyUpgrade(int level) // apply the data from array to stats 
    {
        
    }

    protected void ChangeModel()
    {
        DestroyImmediate(currentModel);
        currentModel = Instantiate(towerModels.allVariants[currentUpgrade], transform.position, Quaternion.identity);
    }
    
    public void SellBuilding()
    {
        currency.AddCurrency(upgrades[currentUpgrade].sellAmount);
        Destroy(gameObject);
    }
    
}
