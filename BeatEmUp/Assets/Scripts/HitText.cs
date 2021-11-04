using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitText : MonoBehaviour
{
    public Vector2 FirstPos;
    public float TargetY;
    
    


    void Start()
    {
        
        FirstPos = transform.position;      
        TargetY = FirstPos.y + 3;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(FirstPos.x, Mathf.MoveTowards(transform.position.y, TargetY, 5 * Time.deltaTime));

        transform.localScale *= Mathf.MoveTowards(1, 5, 3 * Time.deltaTime);

        if (transform.position == new Vector3(FirstPos.x,TargetY,transform.position.z))
        {  
            Destroy(gameObject);
        }
    }
}
