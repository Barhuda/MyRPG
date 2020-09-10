using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaDisplay : MonoBehaviour
{
    [SerializeField] Mana mana;
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = mana.GetMaxMana();
        UpdateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        slider.value = mana.GetCurrentMana();
    }
}
