using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWayPoints : MonoBehaviour
{
    public static Transform[] wayPointsLeft;

    void Awake()
    {
        wayPointsLeft = new Transform[transform.childCount];
        for (int i = 0; i < wayPointsLeft.Length; i++)
        {
            wayPointsLeft[i] = transform.GetChild(i);
        }
    }
}
