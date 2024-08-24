using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingLevelUp : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] BuildingPlacement buildingPlacement;

    [SerializeField] LayerMask tower;

    private TowerBehaviour activeTower;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (buildingPlacement.currentPlacingTower == null)
        {
            Ray camray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {

                print("Click");
                if (Physics.Raycast(camray, out RaycastHit hitInfo, 100f, tower))
                {
                    TowerBehaviour tower = hitInfo.collider.gameObject.GetComponent<TowerBehaviour>();
                    print("Tower hit");
                    if (tower != null)
                    {
                        
                        if(activeTower != null && activeTower.levelUpUI.activeSelf)
                        {
                            activeTower.levelUpUI.SetActive(false);
                        }

                        tower.levelUpUI.SetActive(true);
                        activeTower = tower;
                    }
                    else
                    {
                        print("Tower is null");
                    }
                }

                
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(activeTower != null && activeTower.levelUpUI.activeSelf)
            {
              activeTower.levelUpUI.SetActive(false);
              activeTower = null;
            }
        }
       


    }
}
