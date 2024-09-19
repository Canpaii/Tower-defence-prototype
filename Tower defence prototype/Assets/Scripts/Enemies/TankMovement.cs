using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankMovement : EnemyMovement
{
    [SerializeField] LayerMask tank;
    
    public override void Update()
    {
        base.Update();
        
        if (Vector3.Distance(transform.position, wayPoints.position) <= 0.2f)
        {
            if (wayPointIndex >= AirWaypointLeft.airWayPointsLeft.Length || wayPointIndex >= AirWaypointRight.airWayPointsRight.Length)
            {
                transform.position = transform.position;
                speed = 0;
            }
            else
            {
                GetNextPoint();
            }
        }
        
        if (Physics.Raycast(transform.position, transform.forward, rayLength, tank ))
        {
            speed = 0; 
        }
        else
        {
            speed = speedHolder;
        }
    }
    void GetNextPoint()
    {
        wayPointIndex++;
        if (left)
        {
            wayPoints = WayPointLeftSide.wayPointsLeft[wayPointIndex];
        }
        else if (right)
        {
            wayPoints = WayPointsRightSide.wayPointsRight[wayPointIndex];
        }
    }
    
}
