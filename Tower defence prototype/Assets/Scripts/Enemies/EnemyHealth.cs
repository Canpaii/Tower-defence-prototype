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
    void Start()
    {
        currentHealth = maxHealth;
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
        Currency.Instance.AddCurrency(currencyOnDeath);
        
        Destroy(gameObject);
    }
    
}
