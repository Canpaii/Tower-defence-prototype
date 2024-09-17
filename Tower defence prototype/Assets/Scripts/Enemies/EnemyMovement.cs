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
    public float rotationSmooth;

    public Transform wayPoints;
    public Quaternion targetRotation;

    public int wavePointIndex = 0;

    public bool left;
    public bool right;
    public bool shouldRotate;

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

    virtual public void Update()
    {
        Vector3 dir = wayPoints.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        
    }
    
    // public void OnTriggerEnter(Collider other) // Set target rotation when entering trigger
    // {
    //     targetRotation = other.transform.rotation;
    //     shouldRotate = true;
    // }
}
