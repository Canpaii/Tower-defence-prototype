using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingLevelUp : MonoBehaviour
{
    public BuildingSelect buildingSelect;
    public TMP_Text nameText;
    public Image buildingPortrait;
    public GameObject activeBuilding;
    
    public void TurretUpgradeButton()
    {
        activeBuilding.GetComponent<TurretBasics>().UpgradeTurret();
    }

    public void SellBuildingButton()
    {
        activeBuilding.GetComponent<TurretBasics>().SellBuilding();
    }

    public void ChangeTurretInfo()
    { 
        buildingPortrait.sprite = activeBuilding.GetComponent<TurretBasics>().UIData[0].icon;
        nameText.text = activeBuilding.GetComponent<TurretBasics>().UIData[0].buildingName;
    }
}
