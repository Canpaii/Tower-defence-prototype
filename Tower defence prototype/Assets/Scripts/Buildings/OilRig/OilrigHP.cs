using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilrigHP : BuildingHp
{
    public override void DIE()
    {
        buildingList.UnregisterBuilding(transform);
        Destroy(gameObject);
    }
}
