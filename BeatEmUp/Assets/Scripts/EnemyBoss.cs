using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyBoss : MonoBehaviour

{
    public List<Loot> Loots;
    bool facingRight;
    public int Damage = 40;
    public int maxHealth = 1000;
    public int currentHealth;

    public Transform AttackPoint;
    public float AttackRange = 1f;

    Vector3 TargetOldPos;
    private SoundManager soundmanager;
    private HealthBar healthBar;
    private GameObject HealthBarObject;
    private HitText HitText;
    private GameObject HitsObject;

    private AudioSource AudioSource;
    public bool isDead = false;
    public bool isAwaken = false; 
    public bool attackStarted = false;

    public int EXP;
    public float speed;
    public float chaseDistance;

    public bool isFrozen = false;
    public bool isAttacking = false;
    
    public bool IsRunning = false;

    public GameObject target;


    public LevelEnemyChecker levelEnemyChecker;
    public LayerMask playerLayer;

    Animator animator;

    public float targetDistanceX;
    public float targetDistanceY;

    private void Awake()
    {
        levelEnemyChecker = GameObject.Find("LevelEnemyChecker").GetComponent<LevelEnemyChecker>();
        target = GameObject.Find("Fighter");
        HealthBarObject = Instantiate(Resources.Load("Prefabs/HealthBar")) as GameObject;
        HealthBarObject.transform.parent = GameObject.Find("LevelCanvas").GetComponent<Canvas>().transform;
        HealthBarObject.transform.localScale = new Vector3(1, 1, 1);
        healthBar = HealthBarObject.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        
        AudioSource = GetComponent<AudioSource>();
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
       
        healthBar.BossHealth();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
     
        //isAttacking = false;

        float pushPower = 0;

        if (facingRight)
        {
            transform.position = new Vector2(transform.position.x - pushPower, transform.position.y);
        }
        if (!facingRight)
        {
            transform.position = new Vector2(transform.position.x + pushPower, transform.position.y);
        }

        
            //animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
        healthBar.SetHealth(currentHealth);
        hits(damage);
    }


    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    public void Destroy()
    {
        levelEnemyChecker.EnemyCount -= 1;
        target.GetComponent<PlayerCombat>().GainExp(EXP);
        LootDrop();
        Destroy(HealthBarObject);
        Destroy(gameObject);
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", isDead);
        StopAllCoroutines();

    }

    public void LootDrop()
    {
        float playerLuck = GameObject.Find("Fighter").GetComponent<Skills>().luckRatio;
        float dice = Random.Range(0, 100);



        foreach (var loot in Loots)
        {
            Debug.Log("Loot düştü : " + loot.name);
            if (dice >= 100 - (loot.DropRate * playerLuck))
            {
                if (loot.ID == 1)
                {
                    Instantiate(Resources.Load("Prefabs/Loots/Watermelon Loot"), transform.position, Quaternion.identity);
                }

                else if (loot.ID == 2)
                {
                    transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    Instantiate(Resources.Load("Prefabs/Loots/Gold Loot"), transform.position, Quaternion.identity);
                }
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = new Vector2(transform.position.x, transform.position.y + 4.0f);

        if (!isFrozen)
        {
            if (targetDistanceX < chaseDistance && targetDistanceX > 0)
            {

                isAwaken = true;
            }

            if (isAwaken && !attackStarted)
            {
                attackStarted = true;
                StartCoroutine(AttackPlayer());
            }
            if (!IsRunning && !isAttacking)
            {
                if (transform.position.x < target.transform.position.x && !facingRight)
                {
                    Flip();
                }
                else if (transform.position.x > target.transform.position.x && facingRight)
                {
                    Flip();
                }
            }


            if (IsRunning)
            {

                transform.position = Vector2.MoveTowards(transform.position, TargetOldPos, speed * Time.deltaTime);
                if (transform.position.x < TargetOldPos.x && !facingRight)
                {
                    Flip();
                }
                else if (transform.position.x > TargetOldPos.x && facingRight)
                {
                    Flip();
                }
            }
            else
            {

            }

            if (isDead) // Bazen bug oluyor ölse de ölme animasyonuna girmiyor bu onu engellemek için
            {
                Die();
            }

           
            targetDistanceX = Mathf.Abs(transform.position.x - target.transform.position.x);
            targetDistanceY = Mathf.Abs(transform.position.y - target.transform.position.y);


        }


    }

    private void Attack1()
    {
        animator.SetTrigger("Attack");


       
    }
    
    public void Attack1Damage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, playerLayer);
        if (hitEnemies.Length > 0)
        {
            //AudioSource.clip = soundmanager.Punch2;
            // AudioSource.Play();
        }
        foreach (Collider2D player in hitEnemies)
        {
            if (player.GetComponent<PlayerCombat>())
            {
                if (!player.GetComponent<PlayerCombat>().isDead)
                {
                    player.GetComponent<PlayerCombat>().TakeDamage(Damage);
                }
            }
            else if (player.GetComponent<Enemy>())
            {
                if (!player.GetComponent<Enemy>().isDead)
                {
                    player.GetComponent<Enemy>().TakeDamage(Damage);
                    player.GetComponent<Enemy>().KnockUp();
                }

                {

                }
            }
        }
    }

    private IEnumerator AttackPlayer()
    {



        // IDLE ÖNCE

        TargetOldPos = target.transform.position;
        // DÜŞMANIN YERİ BELİRLENDİ
        IsRunning = true;
        animator.SetBool("IsRunning", IsRunning);
        // DÜŞMANIN BELİRLENEN YERİNE HIZLA KOŞULUYOR
        yield return new WaitUntil(() => transform.position == TargetOldPos);


        // ULAŞINCA VUR
        IsRunning = false;
        isAttacking = true;
        Attack1();
        // VURMA BİTİNCE IDLE A GERİ DÖN 2 SANİYE DUR

        yield return new WaitUntil(() => !isAttacking);
        animator.SetBool("IsRunning", IsRunning);
        yield return new WaitForSeconds(3f);
        // SONRA BAŞA DÖN

        StartCoroutine(AttackPlayer());












    }

   

   

    public void AlertObservers(string message)
    {
        if (message == "AttackEnded")
        {
            isAttacking = false;
        }

    }

    private void ChasePlayer()
    {
        animator.SetBool("IsRunning", IsRunning);

        

      

        if(!IsRunning && !isAttacking)
        {
            IsRunning = true;

            TargetOldPos = target.transform.position;
            
         
           
        }

        if (TargetOldPos == transform.position)
        {
            StartCoroutine(AttackPlayer());
        }



    }

    private void Flip()
    {

        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }

    public void hits(int damage)
    {
        HitsObject = Instantiate(Resources.Load("Prefabs/Hit")) as GameObject;
        HitsObject.transform.parent = GameObject.Find("LevelCanvas").GetComponent<Canvas>().transform;
        HitsObject.transform.localScale = new Vector3(1, 1, 1);
        HitsObject.transform.position = new Vector2(transform.position.x, transform.position.y + 4f);
        HitsObject.GetComponentInChildren<Text>().text = "-" + damage;

        HitText = HitsObject.GetComponent<HitText>();


    }

}


