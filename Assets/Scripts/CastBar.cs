using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBar : MonoBehaviour
{
    Slider slider;
    float castTimer = 0;
    bool isCasting = false;
    [SerializeField] Text castName;
    [SerializeField] Text castDurationText;
    [SerializeField] Image barColor;
    [SerializeField] Image spellIcon;
    [SerializeField] int fadeInSpeed = 3;
    CanvasGroup canvasGroup;

    Coroutine co;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        slider = GetComponent<Slider>();
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator CastProgress()
    {
        while (castTimer <= slider.maxValue)
        {
            castTimer += Time.deltaTime;
            slider.value = castTimer;
            var roundedCastTime = Math.Round(castTimer, 1);
            castDurationText.text = roundedCastTime.ToString();
            canvasGroup.alpha = castTimer * fadeInSpeed;
            yield return null;
        }
        if(castTimer >= slider.maxValue)
        {
            castTimer = 0;
            isCasting = false;
            canvasGroup.alpha = 0;
        }
    }

    public void StartCast(Spell spell)
    {
        isCasting = true;
        slider.maxValue = spell.castTime;
        slider.value = 0;
        castName.text = spell.name;
        barColor.color = spell.barcolor;
        spellIcon.sprite = spell.icon;
        co = StartCoroutine(CastProgress());
    }

    public void StopCasting()
    {
        if(isCasting)
        {
            StopCoroutine(co);
            co = null;
            isCasting = false;
            castTimer = 0;
            canvasGroup.alpha = 0;
        }
    }

}
