using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        
        // if (shouldRotate)
        // {
        //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmooth);
        //
        //     // Check if the rotation is close enough to the target to stop rotating
        //     if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        //     {
        //         transform.rotation = targetRotation;
        //         shouldRotate = false;
        //     }
        // }
    }

    public virtual void Update()
    {
        Vector3 direction = (wayPoints.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, wayPoints.position, speed * Time.deltaTime);

        // Rotate smoothly towards the target waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the enemy has reached the waypoint
        // if (Vector3.Distance(transform.position, wayPoints.position) < 0.1f)
        // {
        //     currentWaypointIndex++;
        //
        //     // If there are more waypoints, set the next target
        //     if (currentWaypointIndex < wayPoints.Length)
        //     {
        //         wayPoints = wayPoints[currentWaypointIndex];
        //     }
        // }
        
    }
    
    // public void OnTriggerEnter(Collider other) // Set target rotation when entering trigger
    // {
    //     targetRotation = other.transform.rotation;
    //     shouldRotate = true;
    // }
}
