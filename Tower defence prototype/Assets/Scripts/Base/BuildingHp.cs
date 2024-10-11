using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingHp : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    protected BuildingList buildingList;
    protected void Start()
    {
        currentHealth = maxHealth;
        buildingList = BuildingList.Instance;
        buildingList.RegisterBuilding(transform);
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
