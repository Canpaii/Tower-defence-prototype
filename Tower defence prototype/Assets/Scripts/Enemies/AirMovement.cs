using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovement : EnemyMovement
{
    [SerializeField] private LayerMask airCraft;
    public override void Update()
    {
        base.Update();
        
        if (Vector3.Distance(transform.position, wayPoints.position) <= 0.2f)
        {
            if (wayPointIndex >= AirWaypointLeft.airWayPointsLeft.Length  || wayPointIndex >= AirWaypointRight.airWayPointsRight.Length )
            {
                transform.position = transform.position;
                speed = 0;
            }
            else
            {
                GetNextPoint();
            }
        }
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength, airCraft ))
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
        wayPointIndex++;
        if (left)
        {
            wayPoints = AirWaypointLeft.airWayPointsLeft[wayPointIndex];
        }
        else if (right)
        {
            wayPoints = AirWaypointRight.airWayPointsRight[wayPointIndex];
        }
    }
   
}
