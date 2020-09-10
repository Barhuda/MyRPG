using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform lastPlayerPos;
    [SerializeField] float startingMoveSpeed = 3f;
    [SerializeField] float moveSpeed = 3f;
    Enemy enemy;
    private Vector2 movement;
    Rigidbody2D rb;



    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float StartingMoveSpeed { get => startingMoveSpeed; }


    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPos = FindObjectOfType<Player>().transform;
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (Vector2)lastPlayerPos.position - (Vector2)transform.position;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        if (!enemy.isAttacking)
        {
            enemy.CheckAttackRange();
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }

}
