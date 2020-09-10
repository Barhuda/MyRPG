using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PlayerHealthDisplay: MonoBehaviour
{
    [SerializeField] Health playerHealth;
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = playerHealth.GetMaxHealth();
        UpdateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        slider.value = playerHealth.GetCurrentHealth();
    }
    
}
