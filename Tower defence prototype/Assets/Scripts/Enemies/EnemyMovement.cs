using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform target;

    private int wavePointIndex = 0;

    public bool left;
    public bool right;

    public bool airShip;
    // Update is called once per frame
    private void Start()
    {
        if (left)
        {
            if (airShip)
            {
                target = AirWaypointLeft.airWayPointsLeft[0];
            }
            else
            {
                target = WayPointLeftSide.wayPointsLeft[0];
            }
            
        }
        else if (right)
        {
            if (airShip)
            {
                target = AirWaypointRight.airWayPointsRight[0];
            }
            else
            {
                target = WayPointsRightSide.wayPointsRight[0];
            }
        }
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            if (wavePointIndex >= WayPointLeftSide.wayPointsLeft.Length - 1 || wavePointIndex >= WayPointsRightSide.wayPointsRight.Length - 1 )
            {
                transform.position = transform.position;
                speed = 0;
            }
            else
            {
                GetNextPoint();
            }
        }
    }

    void GetNextPoint()
    {
        wavePointIndex++;
        if (left)
        {
            target = WayPointLeftSide.wayPointsLeft[wavePointIndex];
        }
        else if (right)
        {
            target = WayPointsRightSide.wayPointsRight[wavePointIndex];
        }
    }
}
