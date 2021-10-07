using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
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
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        tookDamage = true;
        isAttacking = false;

        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y);
        }
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y);
        }

        if (!isKnockedUp)
        animator.SetTrigger("Hit");

        if(currentHealth <= 0 )
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
                        animator.SetTrigger("Attack1");
                        Debug.Log("Attack 1");
                        break;

                    case 1:
                        animator.SetTrigger("Attack2");
                        Debug.Log("Attack 2");
                        break;
                    case 2:
                        animator.SetTrigger("Attack3");
                        Debug.Log("Attack 3");
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
        if (transform.position.x < target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
            GetComponent<SpriteRenderer>().flipX = true;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        
        isWalking = true;
        animator.SetBool("IsWalking", isWalking);
    }


}
