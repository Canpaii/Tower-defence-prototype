using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingSelect : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private BuildingPlacement buildingPlacement;

    [SerializeField] private LayerMask tower;

    public GameObject levelUpUIPanel;
    public GameObject shopUIPanel;
    
    public BuildingLevelUp buildingLevelUp;
    private GameObject _activeTower;
    
    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetMouseButtonDown(0)  && !IsPointerOverUI() && !buildingPlacement.isPlacingTower )
        {
            buildingLevelUp.activeBuilding = ActiveTower();
            

            if (buildingLevelUp.activeBuilding != null)
            {
                buildingLevelUp.ChangeTurretInfo();
                levelUpUIPanel.SetActive(true); 
                shopUIPanel.SetActive(false);
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            buildingLevelUp.activeBuilding = null;
            levelUpUIPanel.SetActive(false);
            shopUIPanel.SetActive(true);
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
    
    private bool IsPointerOverUI() // check if mouse is over UI
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
