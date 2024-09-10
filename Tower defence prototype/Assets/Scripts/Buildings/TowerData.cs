using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class TowerData : ScriptableObject
{
   public TowerType towerType;
   public GameObject towerPrefab;
   public GameObject towerBlueprintPrefab;
   
   public enum TowerType
   {
      Tower,
      OilRig,
   }
}
