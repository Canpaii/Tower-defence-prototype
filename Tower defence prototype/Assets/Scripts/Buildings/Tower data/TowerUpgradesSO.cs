using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretUpgrade", menuName = "Upgrades/TurretUpgrade")]
public class TowerUpgradesSO : ScriptableObject
{
    public int upgradeCost;
    
    public int sellAmount;
    
    public int damage;
    public float attackWaitTime;
    public float range;
    
    public float oilHarvestRate;
}
