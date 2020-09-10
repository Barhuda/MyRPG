using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingHealthBar : MonoBehaviour
{
    int maxHealth = 0, currentHealth = 0;
    [SerializeField] Health enemyHealth;
    [SerializeField] Slider slider;
    [SerializeField] Image barColor;

    private void Start()
    {
            maxHealth = enemyHealth.GetMaxHealth();
            currentHealth = enemyHealth.GetCurrentHealth();
            slider.maxValue = maxHealth;
            slider.value = currentHealth;
    }

    public void UpdateHealth(int health)
    {
        slider.value =  health;
    }

    public void SetBarColorYellow()
    {
        barColor.color = new Color32(192, 10, 10, 255);
    }

    public void SetBarColorRed()
    {
        barColor.color = Color.yellow;
    }
}
