using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelect : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private BuildingPlacement buildingPlacement;

    [SerializeField] private LayerMask tower;
    
    public BuildingLevelUp buildingLevelUp;
    private GameObject _activeTower;
    
    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            buildingLevelUp.activeBuilding = ActiveTower();
            buildingLevelUp.ChangeTurretInfo();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            buildingLevelUp.activeBuilding = null;
        }
    }

    private GameObject ActiveTower()
    {
        Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out RaycastHit hitInfo,300f ,tower))
        {
            _activeTower = hitInfo.collider.gameObject;
            return _activeTower;
        }
        return _activeTower;
    }
}
