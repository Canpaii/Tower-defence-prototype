using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float speedHolder;
    public float rayLength;
    public float rotationSpeed;

    public Transform wayPoints;
    

    public int wayPointIndex = 0;

    public bool left;
    public bool right;

    public bool airShip;
    // Update is called once per frame
    public virtual  void Start()
    {
        speedHolder = speed; 
        if (left)
        {
            if (airShip)
            {
                wayPoints = AirWaypointLeft.airWayPointsLeft[0];
            }
            else
            {
                wayPoints = WayPointLeftSide.wayPointsLeft[0];
            }
            
        }
        else if (right)
        {
            if (airShip)
            {
                wayPoints = AirWaypointRight.airWayPointsRight[0];
            }
            else
            {
                wayPoints = WayPointsRightSide.wayPointsRight[0];
            }
        }
        
    }

    public virtual void Update()
    {
        Vector3 direction = (wayPoints.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, wayPoints.position, speed * Time.deltaTime);

        // Rotate smoothly towards the target waypoint
        if (direction != null )
        {
             Quaternion targetRotation = Quaternion.LookRotation(direction);
             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
