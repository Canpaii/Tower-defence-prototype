using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseHP : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    
    public TMP_Text healthText;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        UpdateHealth(currentHealth);
        
        print("The Base took" + damage + " damage");
        if (currentHealth <= 0)
        {
            DIE();
        }
    }
    
    void UpdateHealth(float currenthealth)
    {
        healthText.text = currenthealth.ToString();
    }
    public void DIE()
    {
        // doe nog wat coole dingetjes ofzo
        
        //lose screen
        
        Destroy(gameObject);
    }
}
