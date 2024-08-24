using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    public GameObject currentPlacingTower;
    public GameObject buildingLevelUp;
    [SerializeField] private Camera playerCamera;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(currentPlacingTower != null)
        {
            Ray camray = playerCamera.ScreenPointToRay(Input.mousePosition);

            buildingLevelUp.SetActive(false);
            

            if(Physics.Raycast(camray, out RaycastHit hitInfo, 100f))
            {
                currentPlacingTower.transform.position = hitInfo.point;
            }

            if(Input.GetMouseButtonDown(0))
            {
                currentPlacingTower.GetComponent<TowerBehaviour>().EnableCollider();
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

    public void SetTowerToPlace(GameObject tower)
    {
        currentPlacingTower = Instantiate(tower , Vector3.zero, quaternion.identity);
    }
}
