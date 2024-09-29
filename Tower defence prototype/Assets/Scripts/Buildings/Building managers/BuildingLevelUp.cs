using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingLevelUp : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private BuildingPlacement buildingPlacement;

    [SerializeField] private LayerMask tower;

    [SerializeField] GameObject _activeTower;
    
    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _activeTower = ActiveTower();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            _activeTower = null;
        }
    }

    private GameObject ActiveTower()
    {
        Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(camRay, out RaycastHit hitInfo, tower))
        {
            _activeTower = hitInfo.collider.gameObject;
        }
        return _activeTower;
    }
}
