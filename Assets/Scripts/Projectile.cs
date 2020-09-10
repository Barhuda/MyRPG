using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Enemy castEnemy;
    Vector2 enemyPosition;
    GameObject projectileParent;
    const string PROJECTILE_PARENT = "Projectiles";

    private float speed;
    public int projectileDamage;

    

    private void Start()
    {
        castEnemy = FindObjectOfType<Player>().GetCastingTarget();
        //enemyPosition = new Vector2(castEnemy.transform.position.x, castEnemy.transform.position.y);

        projectileParent = GameObject.Find(PROJECTILE_PARENT);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT);
        }
        transform.parent = projectileParent.transform;
    }

    void Update()
    {
        if (castEnemy)
        {
            transform.position = Vector2.MoveTowards(transform.position, castEnemy.transform.position, speed * Time.deltaTime);
        }
        

    }

    public void SetSpeed(float travelSpeed)
    {
        speed = travelSpeed;
    }

    public void SetDamage(int damage)
    {
        projectileDamage = damage;
    }




    /*
    private void OnCollisionEnter2D(Collider2D collision)
    {
        Debug.Log("Projectile Hit");
        collision.GetComponent<Health>().TakeDamage(projectileDamage);
    }
    */

}
