using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] tanks;
    private List<GameObject> tanksLeftLane = new List<GameObject>();
    private List<GameObject> tanksRightLane = new List<GameObject>();
    
    public bool gameStarted = false;
    void Start()
    {
        foreach (var tank in tanks)
        {
            int coinflip = Random.Range(0, 1);
            
            if (coinflip == 0)
            {
               tanksLeftLane.Add(tank);
               tank.GetComponent<EnemyMovement>().tankLeft = true;
            }

            if (coinflip == 1)
            {
                tanksRightLane.Add(tank);
                tank.GetComponent<EnemyMovement>().tankRight = true;
            }
        }
    }
    void StartGame()
    {
        gameStarted = true;
    }

    // IEnumerator StartWaves()
    // {
    //     
    // }
   
}
