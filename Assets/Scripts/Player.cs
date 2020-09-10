using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] GameObject projectile;
    [SerializeField] float dashSpeed = 20f;
    public Vector2 movement;
    List<Enemy> listOfEnemies;
    int targetingIndex = 0;
    Enemy currentTarget;
    TargetDisplay targetDisplay;
    SpellBook spellbook;
    bool isCasting = false;
    CastBar cb;
    Enemy targetDuringCastStart;
    Mana playerMana;
    Rigidbody2D rb;


    Coroutine co;



    // Start is called before the first frame update
    void Start()
    {
        listOfEnemies = FindObjectsOfType<Enemy>().ToList();
        targetDisplay = FindObjectOfType<TargetDisplay>();
        spellbook = GetComponent<SpellBook>();
        cb = FindObjectOfType<CastBar>();
        playerMana = GetComponent<Mana>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("GenerateManaPassive", 2, 5);

    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Move();
        TargetEnemy();
        //Shoot();
        Cast();
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            DashMove(direction);
        }
        else*/
        {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                StopCasting();
            }
        }
    }



    private void TargetEnemy()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (currentTarget != null)
            {
                currentTarget.isCurrentTarget = false;
            }
            
            targetingIndex++;
            if (targetingIndex > listOfEnemies.Count - 1)
            {
                targetingIndex = 0;

            }
            currentTarget = listOfEnemies[targetingIndex];
            currentTarget.isCurrentTarget = true;
            targetDisplay.UpdateDisplay(currentTarget.name);

        }
    }


    /*
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentTarget != null)
        {
            Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject newProjectile = Instantiate(projectile, startPos, Quaternion.identity);
            Projectile shootingProjectile = newProjectile.GetComponent<Projectile>();
            shootingProjectile.SetDamage(1);
            shootingProjectile.SetSpeed(7);
        }
    }
    */

    private void Cast()
    {
        if (Input.GetButtonDown("Fire1") && currentTarget != null && !isCasting)
        {
            co = StartCoroutine(CastSpell(0));
        }
        if (Input.GetKeyDown(KeyCode.R) && currentTarget != null &&!isCasting)
        {
            co = StartCoroutine(CastSpell(1));
        }
        if (Input.GetButtonDown("Fire2") && currentTarget != null && !isCasting)
        {
            co = StartCoroutine(CastSpell(2));
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentTarget != null && !isCasting)
        {
            co = StartCoroutine(CastSpell(3));
        }


    }

    IEnumerator CastSpell(int spellIndex)
    {
        if (targetDuringCastStart)
        {
        targetDuringCastStart.tag = "Enemy";
        }

        isCasting = true;
        targetDuringCastStart = currentTarget;
        targetDuringCastStart.tag = "Current Target";

        Spell castingSpell = spellbook.CastSpell(spellIndex);
        bool hasEnoughManaToCast = playerMana.GetCurrentMana() >= castingSpell.manaCost;
        if (hasEnoughManaToCast)
        {
            cb.StartCast(castingSpell);
            playerMana.SpendMana(castingSpell.manaCost);
            yield return new WaitForSeconds(castingSpell.castTime);
            InstantiateProjectile(castingSpell);
            isCasting = false;
        }
    }

    private void InstantiateProjectile(Spell castingSpell)
    {
        Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject newProjectile = Instantiate(castingSpell.spellPrefab, startPos, Quaternion.identity);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.SetSpeed(castingSpell.speed);
        projectile.SetDamage(castingSpell.damage);
    }

    public Enemy GetEnemy()
    {
        return currentTarget;
    }

    public Enemy GetCastingTarget()
    {
        return targetDuringCastStart;
    }

    private void StopCasting()
    {
        cb.StopCasting();
        if(co != null)
        {
            StopCoroutine(co);
            co = null;
        }
        
        isCasting = false;
    }

    private void DashMove(Vector2 direction) 
    {
        int dashManaCost = 10;
        if(dashManaCost <= playerMana.GetCurrentMana())
            {
            /*
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * dashSpeed;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * dashSpeed;

            var newXPos = transform.position.x + deltaX;
            var newYPos = transform.position.y + deltaY;

            transform.position = new Vector2(newXPos, newYPos);
            */
            rb.MovePosition((Vector2)transform.position + (direction * dashSpeed * Time.deltaTime));

            playerMana.SpendMana(dashManaCost);
        }
    }

    private void GenerateManaPassive()
    {
        playerMana.GainMana(5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ManaPotion manaPotion = collision.GetComponent<ManaPotion>();
        if (manaPotion != null)
        {
            Debug.Log("MANA Potion Touched!");
            GetComponent<Mana>().GainMana(manaPotion.manaToGain);
            Destroy(collision.gameObject);
        }
    }

    public void RemoveEnemyFromTargetList(Enemy enemyToRemove)
    {
        listOfEnemies.Remove(enemyToRemove);
    }

    public void AddEnemyToTargetList(Enemy enemyToAdd)
    {
        listOfEnemies.Add(enemyToAdd);
        Debug.Log("Target List is " + listOfEnemies.Count + " long");
    }

    public void UpdateTargetList()
    {
        GameObject[] enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyGameObjects != null)
            listOfEnemies = null;
            foreach(GameObject enemyObject in enemyGameObjects)
            {
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                listOfEnemies.Add(enemy);
            }
        }
    }



