using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] int health = 5;
    Player player;
    Enemy enemy;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();

    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        if(enemy)
        {
            GetComponent<MovingHealthBar>().UpdateHealth(health);
        }
        if(health <= 0)
        {
            Death();
        }
    }

    public void HealDamage(int healValue)
    {
        health += healValue;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    private void Death()
    {
        Destroy(gameObject);
        if (enemy)
        {
player.RemoveEnemyFromTargetList(enemy);
        }
        
    }


}
