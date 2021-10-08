using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    bool facingRight;

    public int maxHealth = 100;
    public int currentHealth;

    public Transform PunchPoint;
    public float punchRange = 0.5f;

    public Transform KickPoint;
    public float kickRange = 0.5f;

    public SoundManager soundmanager;
    public HealthBar healthBar;
    public GameObject HealthBarObject;

    private AudioSource AudioSource;
    public bool isDead = false;
    bool isKnockedUp;

    public float speed;
    public float chaseDistance;
    public float stopDistanceX;
    public float stopDistanceY;
    public bool isAttacking = false;
    public bool tookDamage = false;
    public bool isWalking = false;
    
    public GameObject target;

    public bool targetClose = false;

    public LayerMask playerLayer;

    Animator animator;

    public float targetDistanceX;
    public float targetDistanceY;

    private void Awake()
    {
        
        HealthBarObject = Instantiate(Resources.Load("Prefabs/HealthBar")) as GameObject;
        HealthBarObject.transform.parent = GameObject.Find("Canvas").GetComponent<Canvas>().transform;
        HealthBarObject.transform.localScale =new Vector3(1,1,1);
        healthBar = HealthBarObject.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        target = GameObject.Find("Fighter");
        AudioSource = GetComponent<AudioSource>();
        soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        tookDamage = true;
        isAttacking = false;

        if (facingRight && !isKnockedUp)
        {
            transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y);
        }
        if (!facingRight && !isKnockedUp)
        {
            transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y);
        }

        if (!isKnockedUp)
        animator.SetTrigger("Hit");

        {
            Die();
        }
        healthBar.SetHealth(currentHealth);

    }

    public void KnockUp()
    {

        if(!isKnockedUp)
        {
            animator.SetTrigger("KnockDown");
            isKnockedUp = true;
        }
        

        
    }

    public void Destroy()
    {
        Destroy(HealthBarObject);
        Destroy(gameObject);
    }


    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", isDead);
        
       
    }
    // Update is called once per frame
    void Update()
    {

        if (isDead) // Bazen bug oluyor ölse de ölme animasyonuna girmiyor bu onu engellemek için
        {
            Die();
        }
           
        healthBar.transform.position = new Vector2(transform.position.x, transform.position.y + 3.2f);
        targetDistanceX = Mathf.Abs(transform.position.x - target.transform.position.x);
        targetDistanceY = Mathf.Abs(transform.position.y - target.transform.position.y);

        if(!isDead && !isKnockedUp && !tookDamage)
        {
            if (targetDistanceX < chaseDistance && targetDistanceX > stopDistanceX || targetDistanceY < chaseDistance && targetDistanceY > stopDistanceY && !isKnockedUp)
                ChasePlayer();
            else
                StopChasePlayer();

            if (targetDistanceX < stopDistanceX && targetDistanceY < stopDistanceY)
                targetClose = true;
        }
        else
        {
            //Bi şey yapma
        }

        
    }

    private void Attack1()
    {
        animator.SetTrigger("Attack1");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(KickPoint.position, kickRange, playerLayer);
        if (hitEnemies.Length > 0)
        {
            AudioSource.clip = soundmanager.Punch2;
            AudioSource.Play();
        }
        foreach (Collider2D player in hitEnemies)
        {
            if (!player.GetComponent<PlayerMovement>().isDead)
            {
                player.GetComponent<PlayerMovement>().TakeDamage(10);            
            }
        }
    }

    private void Attack2()
    {
        animator.SetTrigger("Attack2");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(KickPoint.position, kickRange, playerLayer);
        if (hitEnemies.Length > 0)
        {
            AudioSource.clip = soundmanager.Kick2;
            AudioSource.Play();
        }
        foreach (Collider2D player in hitEnemies)
        {
            if (!player.GetComponent<PlayerMovement>().isDead)
            {
                player.GetComponent<PlayerMovement>().TakeDamage(15);
            }
        }
    }

    private void Attack3()
    {
        animator.SetTrigger("Attack3");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(KickPoint.position, kickRange, playerLayer);
        if (hitEnemies.Length > 0)
        {
            AudioSource.clip = soundmanager.Punch1;
            AudioSource.Play();
        }
        foreach (Collider2D player in hitEnemies)
        {
            if (!player.GetComponent<PlayerMovement>().isDead)
            {
                player.GetComponent<PlayerMovement>().TakeDamage(15);

            }
        }

    }

    private IEnumerator AttackPlayer()
    {


         isAttacking = true;
         yield return new WaitForSeconds(2);

            if (targetClose && !tookDamage)
            {
                

                int AttackNo = Random.Range(0, 3);
                switch (AttackNo)
                {
                    case 0:
                        Attack1();
                  
                        break;

                    case 1:
                        Attack2();
    
                        break;

                    case 2:
                        Attack3();
                        break;

                    default:
                        break;


                }
            }
            else
            {
                isAttacking = false;
            }


        tookDamage = false;
        StopAllCoroutines();
    }

    private void StopChasePlayer()
    {
        isWalking = false;
        animator.SetBool("IsWalking", isWalking);

        if(!isAttacking)
        StartCoroutine(AttackPlayer());
    }

    public void AlertObservers(string message)
    {
        if (message == "AttackEnded")
        {
            isAttacking = false;
        }

        if (message == "StandUp")
        {
            isKnockedUp = false;
        }

        if (message == "HitEnded")
        {
            tookDamage = false;
        }

    }

    private void ChasePlayer()
    {
        targetClose = false;
        if (transform.position.x < target.transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (transform.position.x > target.transform.position.x && facingRight)
        {
            Flip();
        }
           

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
        isWalking = true;
        animator.SetBool("IsWalking", isWalking);
    }

    private void Flip()
    {
        
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
       
    }

}
