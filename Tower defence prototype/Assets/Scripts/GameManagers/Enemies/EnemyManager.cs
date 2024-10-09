using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

[System.Serializable]
public struct EnemyWave 
{ 
    public GameObject enemyPrefab;
    public bool airCraft;
    public bool isLeftLane; 
}

[System.Serializable]
public struct Waves 
{
    public EnemyWave[] enemies;  
}
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject button;
    public bool gameStarted = false;
    [SerializeField] public Waves[] waves;
    
    [SerializeField] private Transform leftLaneSpawnPoint;
    [SerializeField] private Transform rightLaneSpawnPoint;
    [SerializeField] private Transform leftLaneSpawnPointAir;
    [SerializeField] private Transform rightLaneSpawnPointAir;
    
    [SerializeField] private float countdown;
    [SerializeField] private float spawnDelay;  
    
    [SerializeField] private float timeBetweenWaves;  
    
    private Transform spawnPoint;
    private int currentWaveIndex = 0;

    private EnemyList _enemyList;

    private void Start()
    {
        instance = this;
        _enemyList = EnemyList.Instance;
    }

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
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                currentWaveIndex++;
                countdown = timeBetweenWaves;  
            }
        }
    }

    private IEnumerator SpawnWave(Waves wave)
    {
        foreach (EnemyWave enemyWave in wave.enemies)
        {
            
            if (enemyWave.isLeftLane)
            {
                if (enemyWave.airCraft)
                {
                    spawnPoint = leftLaneSpawnPointAir;
                }
                else
                {
                    spawnPoint = leftLaneSpawnPoint;
                }
            }
            else
            {
                if (enemyWave.airCraft)
                {
                    spawnPoint = rightLaneSpawnPointAir;
                }
                else
                {
                    spawnPoint = rightLaneSpawnPoint;
                }
            }
            
            GameObject currentEnemy =  Instantiate(enemyWave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            yield return new WaitForSeconds(spawnDelay);  // Adjust spawn delay
        }
    }
}