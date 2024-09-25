using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseHP : BuildingHp
{
    public TMP_Text healthText;
    public GameObject loseScreen;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public override void TakeDamage(float damage)
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
    public override void DIE()
    {
       
        loseScreen.SetActive(true);
        
        
        Destroy(gameObject);
    }
}
