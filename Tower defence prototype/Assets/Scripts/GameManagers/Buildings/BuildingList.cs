using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingList : MonoBehaviour
{
    public static BuildingList Instance;
    private List<Transform> activeBuildings = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterBuilding(Transform building)
    {
        activeBuildings.Add(building);
    }

    public void UnregisterBuilding(Transform building)
    {
        activeBuildings.Remove(building);
    }

    public List<Transform> GetActiveBuildings()
    {
        return activeBuildings;
    }
}
