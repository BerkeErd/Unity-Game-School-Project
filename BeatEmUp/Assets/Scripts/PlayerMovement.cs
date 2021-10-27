using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerMovement : MonoBehaviour
{
 
    public int runSpeed;
    public float horizontal;
    public float vertical;
    Animator animator;
    public bool facingRight;
    public bool isFrozen = false;
    public PlayerCombat Playercombat;
    
    public float maxYpos = -2.21f;
    public float minYpos = -4.90f;

    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        Playercombat = GetComponent<PlayerCombat>();
        
    }


    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal != 0 ? horizontal : vertical));

    }

    void FixedUpdate()
    {
        if((horizontal != 0 || vertical != 0) && !Playercombat.isPunching && !Playercombat.isKicking)
        {

            Vector3 movement = new Vector3(horizontal * runSpeed, vertical * runSpeed, 0.0f);
            transform.position = transform.position + movement * Time.deltaTime;
        }
       

        if (transform.position.y >= maxYpos)
        {
            transform.position = new Vector2(transform.position.x, maxYpos - 0.01f);
        }

        else if (transform.position.y <= minYpos)
        {
            transform.position = new Vector2(transform.position.x, minYpos-0.01f);
        }



        if (Camera.main.WorldToViewportPoint(transform.position).x <= 0)
         {
            
             transform.position = new Vector2(transform.position.x+0.2f, transform.position.y);
         }

         else if (Camera.main.WorldToViewportPoint(transform.position).x >= 1)
         {
           
            transform.position = new Vector2(transform.position.x-0.2f, transform.position.y);
         }
         

       


        Flip(horizontal);

    }


    private void Flip(float horizontal)
    {
        if(horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    
}
