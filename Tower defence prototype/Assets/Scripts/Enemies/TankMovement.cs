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
            if (wavePointIndex >= AirWaypointLeft.airWayPointsLeft.Length - 1)
            {
                transform.position = transform.position;
                speed = 0;
            }
            else if (wavePointIndex >= AirWaypointRight.airWayPointsRight.Length - 1 )
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
    public void GetNextPoint()
    {
        wavePointIndex++;
        if (left)
        {
            wayPoints = WayPointLeftSide.wayPointsLeft[wavePointIndex];
        }
        else if (right)
        {
            wayPoints = WayPointsRightSide.wayPointsRight[wavePointIndex];
        }
    }
    
}
