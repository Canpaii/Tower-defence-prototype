using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWaypointRight : MonoBehaviour
{
    public static Transform[] airWayPointsRight;

    void Awake()
    {
        airWayPointsRight = new Transform[transform.childCount];
        for (int i = 0; i < airWayPointsRight.Length; i++)
        {
            airWayPointsRight[i] = transform.GetChild(i);
        }
    }
}
