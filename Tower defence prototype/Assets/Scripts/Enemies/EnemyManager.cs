using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tanksLeftLane = new List<GameObject>();
    [SerializeField] private List<GameObject> tanksRightLane = new List<GameObject>();
    
    public GameObject button;
    public bool gameStarted = false;
    
    [SerializeField] private Transform leftLaneSpawnPoint;
    [SerializeField] private Transform rightLaneSpawnPoint;
    [SerializeField] private float countdown;
    [SerializeField] private float spawnDelay;  
    [SerializeField] private int minTanksPerWave; 
    [SerializeField] private float timeBetweenWaves;  
    
    private int waveNumber = 0;
    public void StartGame()
    {
        gameStarted = true;
        button.SetActive(false);
    }

    private void Update()
    {
        if (gameStarted)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                //if (waveNumber >= 7) // spawn heli's
                {
                    
                }
                StartCoroutine(StartWave());
                countdown = timeBetweenWaves;  // Reset the countdown for the next wave
            }
        }
    }

    IEnumerator StartWave()
    {
        waveNumber++;  // Increment the wave number
        int spawnedTanks = 0;
        int tanksPerWave = minTanksPerWave + (waveNumber * 2 - 3);
        
        while (spawnedTanks < tanksPerWave)
        {
            float random = Random.Range(0, 2);

            if (random == 0)
            {
                 if (tanksLeftLane.Count > 0)
                 {
                     SpawnTank(tanksLeftLane, leftLaneSpawnPoint);
                     spawnedTanks++;
                 }
                 else
                 {
                     SpawnTank(tanksRightLane, rightLaneSpawnPoint);
                 }
            }
            else if (random == 1)
            {
                 if (tanksRightLane.Count > 0)
                 {
                     SpawnTank(tanksRightLane, rightLaneSpawnPoint);
                     spawnedTanks++;
                 }
                 else
                 {
                     SpawnTank(tanksLeftLane, leftLaneSpawnPoint);
                 }
            }
            
            yield return new WaitForSeconds(spawnDelay);  // Delay between each tank spawn
        }
    }
    void SpawnTank(List<GameObject> laneTanks, Transform spawnPoint)
    {
        if (laneTanks.Count > 0)
        {
            GameObject tankToSpawn = laneTanks[0]; 
            Instantiate(tankToSpawn, spawnPoint.position, spawnPoint.rotation); 
            laneTanks.RemoveAt(0);
        }
    }
}
