using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetDisplay : MonoBehaviour
{
    Player player;
    TextMeshProUGUI targetText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        targetText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplay(string currentTarget)
    {
        targetText.SetText(currentTarget);

    }
}
