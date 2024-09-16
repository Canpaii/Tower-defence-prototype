using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRig : TowerBehaviour
{
    public float harvestAmount;
    void Update()
    {
        if (enemyManager.gameStarted)
        {
            Currency.Instance.AddCurrency(harvestAmount * Time.deltaTime);
        }
    }
}
