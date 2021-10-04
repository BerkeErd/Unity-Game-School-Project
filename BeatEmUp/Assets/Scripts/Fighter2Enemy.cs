using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter2Enemy : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    public float stopDistanceX;
    public float stopDistanceY;
    public GameObject target;

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

    private void StopChasePlayer()
    {
        animator.SetBool("IsWalking", false);
    }

    private void ChasePlayer()
    {
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
