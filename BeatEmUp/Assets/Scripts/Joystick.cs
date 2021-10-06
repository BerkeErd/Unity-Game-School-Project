using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    private Vector2 pointA2;
    

    public Transform circle;
    public Transform outerCircle;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x <= Screen.width / 2)
        {
            touchStart = true;
            pointA = new Vector2( Input.mousePosition.x ,  Input.mousePosition.y);

            pointA2 =Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            circle.transform.position = pointA2;
            outerCircle.transform.position = pointA2;
            circle.GetComponent<SpriteRenderer>().enabled = true;
            outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0) && Input.mousePosition.x <= Screen.width / 2)
        {
            
                
                pointB = new Vector2(Input.mousePosition.x,  Input.mousePosition.y);
                
        }
       
        else
        {
            touchStart = false;
        }

    }
    private void FixedUpdate()
    {
        if (touchStart && Input.mousePosition.x <= Screen.width / 2)
        {
            Vector2 offset = pointB - pointA;
            
            Vector2 direction = Vector2.ClampMagnitude(offset / (Screen.width / 19), 1.0f);

            Debug.Log("B" + pointB + " - A" + pointA + " - A2" + pointA2 + " - camera " +Camera.main.transform.position);
            circle.transform.position = new Vector2(outerCircle.transform.position.x + direction.x, outerCircle.transform.position.y + direction.y);

            //Player'a yön bilgilerini gönder
            player.GetComponent<PlayerMovement>().horizontal =  direction.x;
            player.GetComponent<PlayerMovement>().vertical =  direction.y;
        }
        else
        {
            circle.GetComponent<SpriteRenderer>().enabled = false;
            outerCircle.GetComponent<SpriteRenderer>().enabled = false;

            //Player'a yön bilgilerini gönder
            player.GetComponent<PlayerMovement>().horizontal = 0;
            player.GetComponent<PlayerMovement>().vertical = 0;
        }

    }


   
}
