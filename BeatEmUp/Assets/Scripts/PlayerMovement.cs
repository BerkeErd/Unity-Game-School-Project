using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int runSpeed;

    public float horizontal;
    public float vertical;
    Animator animator;
    bool facingRight;

    bool isPunching;

    Button punchButton;
    public float maxYpos = -2.21f;
    public float minYpos = -4.90f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        punchButton = GameObject.Find("PunchButton").GetComponent<Button>();

        punchButton.onClick.AddListener(delegate { Punch(); });
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal != 0 ? horizontal : vertical));

        

    }



    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Released.");
    }









    void Punch()
    {
        isPunching = true;
        if (vertical != 0 || horizontal != 0)
        {
            vertical = 0;
            horizontal = 0;
            animator.SetFloat("Speed", 0);         
        }

        animator.SetTrigger("Punch");

    }

    void FixedUpdate()
    {
        if((horizontal != 0 || vertical != 0) && !isPunching)
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





        Flip(horizontal);

    }

    public void AlertObservers(string message)
    {
        if(message == "PunchEnded")
        {
            isPunching = false;
        }
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
