using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaShootScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float chainRadius = 10f;
    public int maxChains = 5;
    public LayerMask enemyLayer;
    public Transform startPoint;
    public int repeatCount = 5;
    public float delayBetweenChains = 2f;
    public float chainSpeed = 0.2f;
    public float trailVisibleDuration = 2f;  // De tijd dat de trail zichtbaar blijft
    public Transform firstEnemyInChain;
    public GameObject chainEffect;

    private List<Transform> hitEnemies = new List<Transform>();
    private bool isChainRunning = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isChainRunning)
        {
            StartCoroutine(StartChainAttack());
        }
    }

    IEnumerator StartChainAttack()
    {
        isChainRunning = true;

        for (int repeat = 0; repeat < repeatCount; repeat++)
        {
            chainEffect.SetActive(true);

            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, startPoint.position);

            yield return StartCoroutine(ChainToEnemies(startPoint.position));

            yield return new WaitForSeconds(trailVisibleDuration);  // Wacht totdat de trail zichtbaar is

            lineRenderer.positionCount = 0;

            chainEffect.SetActive(false);

            yield return new WaitForSeconds(delayBetweenChains);
        }

        isChainRunning = false;
    }

    IEnumerator ChainToEnemies(Vector3 startPos)
    {
        Vector3 currentPos = startPos;
        bool isFirstEnemy = true;

        for (int i = 0; i < maxChains; i++)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(currentPos, chainRadius, enemyLayer);

            Transform closestEnemy = null;
            float shortestDistance = Mathf.Infinity;

            foreach (Collider enemy in enemiesInRange)
            {
                float distanceToEnemy = Vector3.Distance(currentPos, enemy.transform.position);

                if (distanceToEnemy < shortestDistance && !hitEnemies.Contains(enemy.transform))
                {
                    closestEnemy = enemy.transform;
                    shortestDistance = distanceToEnemy;
                }
            }

            if (closestEnemy == null)
            {
                break;
            }

            if (isFirstEnemy)
            {
                firstEnemyInChain = closestEnemy;
                isFirstEnemy = false;
            }

            hitEnemies.Add(closestEnemy);

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, closestEnemy.position);

            currentPos = closestEnemy.position;

            yield return new WaitForSeconds(chainSpeed);
        }

        hitEnemies.Clear();
    }
}
