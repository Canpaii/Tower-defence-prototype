using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsRightSide : MonoBehaviour
{
    public static Transform[] wayPointsRight;

    void Awake()
    {
        wayPointsRight = new Transform[transform.childCount];
        for (int i = 0; i < wayPointsRight.Length; i++)
        {
            wayPointsRight[i] = transform.GetChild(i);
        }
    }
}
