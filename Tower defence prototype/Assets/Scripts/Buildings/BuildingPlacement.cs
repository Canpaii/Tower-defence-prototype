using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    [SerializeField] private GameObject buildingLevelUpPrefab; 
    [SerializeField] private Camera playerCamera; 
    [SerializeField] private LayerMask grid; 
    [SerializeField] private LayerMask oilGrid;
    [SerializeField] private float yOffset;

    public EnemyManager enemyManager;
    public GameObject currentPlacingTower;
    public GameObject buildingLevelUp;
    private int buildingCost;
    
    
    private TowerData currentTowerData;
    

    // Update is called once per frame
    void Update()
    {
        if(currentPlacingTower != null)
        {
            Ray camray = playerCamera.ScreenPointToRay(Input.mousePosition);
            
            // set false so it wont immediatly open lvl up ui
            buildingLevelUp.SetActive(false); 
            
            if (currentTowerData.towerType == TowerData.TowerType.OilRig)
            {
                if (Physics.Raycast(camray, out RaycastHit hitInfo, 150f, oilGrid))
                {
                    currentPlacingTower.transform.position = hitInfo.collider.gameObject.transform.position + new Vector3(0, yOffset, 0);
                    currentPlacingTower.SetActive(true);
                    
                    if(Input.GetMouseButtonDown(0))
                    {   
                        PlaceTower();
                    }
                }
                else
                {
                    if(Input.GetMouseButtonDown(0))
                    {   
                        DeselectTower();
                    }
                }
                
            }
            else if (currentTowerData.towerType == TowerData.TowerType.Tower)
            {  
                if(Physics.Raycast(camray, out RaycastHit hitInfo, 150f, grid))
                {
                    currentPlacingTower.transform.position = hitInfo.collider.gameObject.transform.position + new Vector3(0, yOffset, 0);
                    currentPlacingTower.SetActive(true);
                    
                    if(Input.GetMouseButtonDown(0))
                    {   
                        PlaceTower();
                        hitInfo.collider.gameObject.SetActive(false); // give the tower a reference to the grid its on so you can sell the tower later and bring back the tile
                    }
                }
                else
                {
                    if(Input.GetMouseButtonDown(0)) 
                    {   
                        DeselectTower();
                    }
                }
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
        buildingCost = towerData.cost;
    }
    #region Tower Selection
    void DeselectTower()
    {
        Destroy(currentPlacingTower);
        currentPlacingTower = null;
        buildingLevelUp.SetActive(true);
    }

    void PlaceTower()
    {
        // if you dont have enough money dont place the tower
        if (Currency.Instance.currency >= buildingCost) 
        { 
             Currency.Instance.SubtractCurrency(buildingCost);
            
             // place tower on cursor and clear currentplacingtower gameOBject
             GameObject placingTower = Instantiate(currentTowerData.towerPrefab, currentPlacingTower.transform.position, quaternion.identity);
             
             placingTower.GetComponent<TowerBehaviour>().enemyManager = enemyManager;
             
             Destroy(currentPlacingTower);
             
             currentPlacingTower = null;
             buildingLevelUp.SetActive(true);
        }
        else
        {
            print ("You do not have enough money to place this tower.");
            
            Destroy(currentPlacingTower);
            currentPlacingTower = null;
        }
    }
    #endregion
}
