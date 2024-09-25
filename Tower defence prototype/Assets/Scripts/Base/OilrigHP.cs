using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilrigHP : BuildingHp
{
    public override void DIE()
    {
        Destroy(gameObject);
    }
}
