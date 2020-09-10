using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAttack : MonoBehaviour
{
    [SerializeField] float timeBetweenAtttacks = 2f;
    [SerializeField] LineRenderer directionLine;
    bool attackRdy = false;
    float attackingTimer = 0f;
    int layerMask = 1 << 8;
    Player player;
    public Transform laserHit;



    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        AttackTimer();
    }

    void AttackTimer()
    {
        if (!attackRdy)
        {
            attackingTimer += Time.deltaTime;
            if(attackingTimer >= timeBetweenAtttacks)
            {
                attackRdy = true;
                ShootLaser();
            }
        }



    }

    private void ShootLaser()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 shooterPos = transform.position;
        Vector2 direction = playerPos - shooterPos;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layerMask);
        laserHit.position = hit.point;

        directionLine.SetPosition(0, transform.position);
        directionLine.SetPosition(1, laserHit.position);


        Debug.DrawRay(transform.position, direction, Color.red);
        attackRdy = false;
    }
}
