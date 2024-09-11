using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> tanks = new List<GameObject>();
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
            }

            if (coinflip == 1)
            {
                tanksRightLane.Add(tank);
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
