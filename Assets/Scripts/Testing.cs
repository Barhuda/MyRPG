using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //DealDamage();
        HealPlayer();
    }

   /*
    private void DealDamage()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Player Taking Damage");
            health.TakeDamage(10);
        }
        
    }
   */
    private void HealPlayer()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Player Healing");
            health.HealDamage(10);
        }

    }
}
