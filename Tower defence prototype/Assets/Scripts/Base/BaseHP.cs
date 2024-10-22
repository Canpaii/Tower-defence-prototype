using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseHP : BuildingHp
{
    public TMP_Text healthText;
    public GameObject loseScreen;
    public Image bloodyOverlay; 
    public float maxOpacity = 0.8f;
    public float opacityIncreaseRate = 0.1f; 

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealth(currentHealth);

        print("The Base took " + damage + " damage");

        if (bloodyOverlay != null)
        {
            Color overlayColor = bloodyOverlay.color;
            overlayColor.a += opacityIncreaseRate * damage;
            overlayColor.a = Mathf.Clamp(overlayColor.a, 0, maxOpacity); 
            bloodyOverlay.color = overlayColor;
        }

        if (currentHealth <= 0)
        {
            DIE();
        }
    }

    void UpdateHealth(float currentHealth)
    {
        healthText.text = currentHealth.ToString() + "+";
    }

    public override void DIE()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
        }
        else
        {
            print("loseScreen is null");
        }

        enemyList.DestroyAllEnemies();
        enemyManager.gamePaused = true;

        Destroy(gameObject);
    }
}
