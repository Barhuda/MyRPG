using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHealthDisplay : MonoBehaviour
{
    Slider slider;
    Enemy currentTarget;
    float maxHealth;

    private void Start()
    {
        // GetEnemy();
        slider = GetComponent<Slider>();   
    }

    public void SetCurrentHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void UpdateDisplay(float health)
    {
        slider.value = health   ;
    }

    public void GetEnemy()
    {
        currentTarget = FindObjectOfType<Player>().GetEnemy();
        ProcessHealthInfo();
    }

    private void ProcessHealthInfo()
    {
        Health currentTargetHealth = currentTarget.GetComponent<Health>();
        maxHealth = currentTargetHealth.GetMaxHealth();
        int targetHealth = currentTargetHealth.GetCurrentHealth();
        slider.maxValue = maxHealth;
        slider.value = targetHealth;
    }
    
}
