using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    
    public int currencyOnDeath;
    
    public Slider healthSlider;
    private EnemyCounter _enemyCounter;
    private EnemyList _enemyList;
    
    public GameObject deathParticle;

    private void Awake()
    {
        _enemyList = EnemyList.Instance;
    }

    void Start()
    {
        currentHealth = maxHealth;
        _enemyCounter = WinconditionManager.instance.counter;
        
        _enemyList.RegisterEnemy(transform);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        UpdateHealthBar(currentHealth);
        
        print("I took " + damage + " damage");
        if (currentHealth <= 0)
        {
            DIE();
        }
    }
    
    void UpdateHealthBar(float currenthealth)
    {
        healthSlider.GetComponent<Slider>().value = currenthealth;
    }
    public void DIE()
    {
        // doe nog wat coole dingetjes ofzo
        _enemyCounter.UpdateEnemiesKilled();
        Currency.Instance.AddCurrency(currencyOnDeath);
        _enemyList.UnregisterEnemy(transform);
        
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
    
}
