using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWaypointLeft : MonoBehaviour
{
    public static Transform[] airWayPointsLeft;

    void Awake()
    {
        airWayPointsLeft = new Transform[transform.childCount];
        for (int i = 0; i < airWayPointsLeft.Length; i++)
        {
            airWayPointsLeft[i] = transform.GetChild(i);
        }
    }
}
