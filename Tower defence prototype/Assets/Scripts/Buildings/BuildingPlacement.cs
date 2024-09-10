using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] private float yOffset;
    public GameObject currentPlacingTower;
    public GameObject buildingLevelUp;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask grid;
    [SerializeField] private LayerMask oilGrid;
    
    private TowerData currentTowerData;
    

    // Update is called once per frame
    void Update()
    {
        if(currentPlacingTower != null)
        {
           
            Ray camray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (currentTowerData.towerType == TowerData.TowerType.OilRig)
            {
                if (Physics.Raycast(camray, out RaycastHit hitInfo, 150f, oilGrid))
                {
                    currentPlacingTower.transform.position = hitInfo.collider.gameObject.transform.position + new Vector3(0, yOffset, 0);
                    currentPlacingTower.SetActive(true);
                }
            }
            else if (currentTowerData.towerType == TowerData.TowerType.Tower)
            {  
                if(Physics.Raycast(camray, out RaycastHit hitInfo, 150f, grid))
                {
                    currentPlacingTower.transform.position = hitInfo.collider.gameObject.transform.position + new Vector3(0, yOffset, 0);
                    currentPlacingTower.SetActive(true);
                }
            }
            
            buildingLevelUp.SetActive(false);
            
            if(Input.GetMouseButtonDown(0))
            {   
                Instantiate(currentTowerData.towerPrefab, currentPlacingTower.transform.position, quaternion.identity);
                Destroy(currentPlacingTower);
                currentPlacingTower = null;
                buildingLevelUp.SetActive(true);
            }

             if(Input.GetMouseButtonDown(1))
            {
                Destroy(currentPlacingTower);
                buildingLevelUp.SetActive(true);
            }   
        }
    }

    public void SetTowerToPlace(TowerData towerData)
    {
        currentPlacingTower = Instantiate(towerData.towerBlueprintPrefab, Vector3.zero, quaternion.identity);
        currentPlacingTower.SetActive(false);
        currentTowerData = towerData;
    }
}
