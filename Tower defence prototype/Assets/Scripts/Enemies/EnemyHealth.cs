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
    private EnemyList _enemyList;
    
    public GameObject deathParticle;
    // public AudioSource audioSource;
    public AudioClip deathSound;

    private void Awake()
    {
        _enemyList = EnemyList.Instance;
    }

    void Start()
    {
        currentHealth = maxHealth;
        
        _enemyList.RegisterEnemy(transform);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        UpdateHealthBar(currentHealth);
        
        print("I took " + damage + " damage");
        if (currentHealth <= 0)
        { 
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Destroy(gameObject);
        }
    }
    
    void UpdateHealthBar(float currenthealth)
    {
        healthSlider.GetComponent<Slider>().value = currenthealth;
    }
    
    private void OnDestroy()
    {
        Currency.Instance.AddCurrency(currencyOnDeath); 
        _enemyList.UnregisterEnemy(transform);
             
        
    }
}
