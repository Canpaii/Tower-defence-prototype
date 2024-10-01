using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRig : TurretBasics
{
    public float harvestAmount;
    void Update()
    {
        if (enemyManager.gameStarted)
        {
            Currency.Instance.AddCurrency(harvestAmount * Time.deltaTime);
        }
    }
    
    protected override void ApplyUpgrade(int level)
    {
        harvestAmount = upgrades[level].oilHarvestRate;
    }
}
