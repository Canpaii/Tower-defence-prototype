using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingLevelUp : MonoBehaviour
{
    public static BuildingLevelUp instance;
    public BuildingSelect buildingSelect;
    public TMP_Text nameText;
    public Image buildingPortrait;
    public GameObject activeBuilding;

    void Awake()
    {
        instance = this;
    }
    
    public void TurretUpgradeButton()
    {
        activeBuilding.GetComponent<TurretBasics>().UpgradeTurret();
        ChangeTurretInfo();
        ShowRadius();
    }

    public void SellBuildingButton()
    {
        activeBuilding.GetComponent<TurretBasics>().SellBuilding();
        buildingSelect.SetWindowShop();
    }

    public void ChangeTurretInfo()
    { 
        buildingPortrait.sprite = activeBuilding.GetComponent<TurretBasics>().UIData.icon;
        nameText.text = activeBuilding.GetComponent<TurretBasics>().UIData.buildingName;
    } 
    public void ShowRadius() 
    { 
        activeBuilding.GetComponent<TurretBasics>().ShowRadius();
    }

    public void HideRadius()
    {
        activeBuilding.GetComponent<TurretBasics>().HideRadius();
    }
}
