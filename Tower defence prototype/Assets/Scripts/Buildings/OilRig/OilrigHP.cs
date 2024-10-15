using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilrigHP : BuildingHp
{
    public override void DIE()
    {
        gameObject.GetComponent<OilRig>().grid.SetActive(true);
        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        buildingList.UnregisterBuilding(transform);
    }
}
