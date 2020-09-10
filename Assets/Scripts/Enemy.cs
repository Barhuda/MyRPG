using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{

    public bool isAttacking = false;
    [SerializeField] int damage = 5;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float timeBetweenAttacks = 1.5f;
    [SerializeField] float dropchance = 0.5f;
    EnemyMovement enemyMovement;



    Player player;
    Health playerHealth;
    EnemyAI enemyAI;


    public bool isCurrentTarget = false;
    bool isCastingTarget = false;




    // Start is called before the first frame update
    void Start()
    {
        
        enemyAI = GetComponent<EnemyAI>();
        enemyMovement = GetComponent<EnemyMovement>();
        player = FindObjectOfType<Player>();
        playerHealth = player.GetComponent<Health>();
    }

    private void Update()
    {
        CheckAttackRange();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.GetComponent<Projectile>();
        if (projectile != null)
        {
            if (gameObject.CompareTag("Current Target"))
            {
                GetComponent<Health>().TakeDamage(projectile.projectileDamage);
                TriggerSpellEffects(collision);
                Destroy(collision.gameObject);
            }
        }
    }



    public void CheckAttackRange()
    {
        if (isAttacking == false)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= attackRange)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {        
        isAttacking = true;
        enemyAI.StopMovement();
        Debug.Log(gameObject.name +" isAttacking");
        playerHealth.TakeDamage(damage);
        yield return new WaitForSeconds(timeBetweenAttacks);
        Debug.Log("Waiting finished");
        enemyAI.RestartMovement();
        isAttacking = false;
    }



    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void TriggerSpellEffects(Collider2D collision)
    {
        //Trigger IceBall
        IceBall iceBall = collision.GetComponent<IceBall>();
        if(iceBall)
        {
            StartCoroutine(SlowEnemy(iceBall.slowAmount, iceBall.slowDuration));
        }
    }

    IEnumerator SlowEnemy(float slowAmount, float slowDuration)
    {
        if (enemyMovement)
        {
            enemyMovement.MoveSpeed = enemyMovement.MoveSpeed * slowAmount;
        }
        else if (enemyAI)
        {
            enemyAI.currentMoveSpeed = enemyAI.currentMoveSpeed * slowAmount;
        }
        Debug.Log("Enemy slowed");
        yield return new WaitForSeconds(slowDuration);
        if (enemyMovement)
        {
            enemyMovement.MoveSpeed = enemyMovement.StartingMoveSpeed;
        }
        else if (enemyAI)
        {
            enemyAI.currentMoveSpeed = enemyAI.startingMoveSpeed;
        }

    }


}
