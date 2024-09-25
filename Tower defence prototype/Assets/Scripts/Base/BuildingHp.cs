using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHp : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        print("The Base took" + damage + " damage");
        if (currentHealth <= 0)
        {
            DIE();
        }
    }

    virtual public void DIE()
    {
        
    }
}
