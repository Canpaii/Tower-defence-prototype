using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAttack : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float chainRadius = 10f;   // De maximale afstand voor een chain naar de volgende vijand
    public int maxChains = 5;         // Maximaal aantal vijanden om te raken in één chain attack
    public LayerMask enemyLayer;      // Layer waar de vijanden zich op bevinden
    public Transform startPoint;      // Startpunt van de chain attack
    public int repeatCount = 5;       // Hoe vaak de chain attack herhaald wordt
    public float delayBetweenChains = 2f;  // Vertraging tussen elke chain attack
    public float chainSpeed = 0.2f;   // Snelheid van de chain tussen vijanden
    public Transform firstEnemyInChain;  // De eerste vijand in de chain (wordt weergegeven in de Inspector)
    public GameObject chainEffect;    // Het object dat zichtbaar/onzichtbaar wordt tijdens de chain

    private List<Transform> hitEnemies = new List<Transform>();
    private bool isChainRunning = false;

    void Update()
    {
        // Start chain attack wanneer de spatiebalk wordt ingedrukt en het niet al bezig is
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
            // Maak het object zichtbaar voordat elke chain begint
            chainEffect.SetActive(true);

            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, startPoint.position);

            // Start chain attack
            yield return StartCoroutine(ChainToEnemies(startPoint.position));

            // Verwijder de lijnen nadat een chain is voltooid
            lineRenderer.positionCount = 0;

            // Maak het object onzichtbaar nadat een chain eindigt
            chainEffect.SetActive(false);

            // Wacht 2 seconden voordat de volgende chain begint
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
            // Zoek naar vijanden in de buurt
            Collider[] enemiesInRange = Physics.OverlapSphere(currentPos, chainRadius, enemyLayer);

            Transform closestEnemy = null;
            float shortestDistance = Mathf.Infinity;

            // Vind de dichtstbijzijnde vijand die nog niet geraakt is
            foreach (Collider enemy in enemiesInRange)
            {
                float distanceToEnemy = Vector3.Distance(currentPos, enemy.transform.position);

                if (distanceToEnemy < shortestDistance && !hitEnemies.Contains(enemy.transform))
                {
                    closestEnemy = enemy.transform;
                    shortestDistance = distanceToEnemy;
                }
            }

            // Als er geen vijanden meer zijn om te raken, stop de chain attack
            if (closestEnemy == null)
            {
                break;
            }

            // Sla de eerste vijand op en toon hem in de Inspector
            if (isFirstEnemy)
            {
                firstEnemyInChain = closestEnemy;
                isFirstEnemy = false;
            }

            // Voeg de vijand toe aan de lijst van geraakte vijanden
            hitEnemies.Add(closestEnemy);

            // Voeg de vijand toe aan de LineRenderer
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, closestEnemy.position);

            // Update de positie voor de volgende chain
            currentPos = closestEnemy.position;

            // Wacht op basis van de ingestelde chainspeed voor de volgende kettinglink
            yield return new WaitForSeconds(chainSpeed);
        }

        // Maak de lijst van geraakte vijanden leeg voor de volgende chain attack
        hitEnemies.Clear();
    }
}
