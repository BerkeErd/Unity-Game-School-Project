using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fighter2Enemy : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDistanceX;
    public float stopDistanceY;
    public bool isAttacking = false;
    public GameObject target;

    bool targetClose = false;

    Animator animator;

    public float targetDistanceX;
    public float targetDistanceY;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Fighter");
    }

    // Update is called once per frame
    void Update()
    {
        targetDistanceX = Mathf.Abs(transform.position.x - target.transform.position.x);
        targetDistanceY = Mathf.Abs(transform.position.y - target.transform.position.y);

        if (targetDistanceX < chaseDistance && targetDistanceX > stopDistanceX || targetDistanceY < chaseDistance && targetDistanceY > stopDistanceY)
            ChasePlayer();
        else
            StopChasePlayer();
    }

    private IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(2);
        if(targetClose)
        {
            isAttacking = true;
            int AttackNo = Random.Range(0, 3);
            switch (AttackNo)
            {
                case 0:
                    animator.SetTrigger("Attack1");
                    break;

                case 1:
                    animator.SetTrigger("Attack2");
                    break;
                case 2:
                    animator.SetTrigger("Attack3");
                    break;

                default:
                    break;
            }
        }
      
    }

    private void StopChasePlayer()
    {
        targetClose = true;
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
