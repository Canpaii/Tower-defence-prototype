using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public static EnemyList Instance;
    private List<Transform> activeEnemies = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemy(Transform enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(Transform enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public List<Transform> GetActiveEnemies()
    {
        return activeEnemies;
    }
}
