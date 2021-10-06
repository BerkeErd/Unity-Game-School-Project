using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fighter2Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;

    bool isDead = false;
    bool isKnockedUp;
    public float speed;
    public float chaseDistance;
    public float stopDistanceX;
    public float stopDistanceY;
    public bool isAttacking = false;
    public bool tookDamage = false;
    public GameObject target;

    public bool targetClose = false;
 

    Animator animator;

    public float targetDistanceX;
    public float targetDistanceY;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        target = GameObject.Find("Fighter");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        tookDamage = true;
        isAttacking = false;
        animator.SetTrigger("Hit");
        if(currentHealth <= 0 )
        {
            Die();
        }

    }

    public void KnockUp()
    {
        
        animator.SetTrigger("KnockDown");
        isKnockedUp = true;

        
    }


    

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", isDead);
       
       
    }
    // Update is called once per frame
    void Update()
    {
        targetDistanceX = Mathf.Abs(transform.position.x - target.transform.position.x);
        targetDistanceY = Mathf.Abs(transform.position.y - target.transform.position.y);

        if(!isDead)
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
        
        animator.SetBool("IsWalking", false);

        if(!isAttacking)
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
        targetClose = false;
        if (transform.position.x < target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
            GetComponent<SpriteRenderer>().flipX = true;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        animator.SetBool("IsWalking", true);
    }


}
