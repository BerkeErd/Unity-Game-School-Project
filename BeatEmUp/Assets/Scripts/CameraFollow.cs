using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public float CameraLeftXLimit = -3.3f;
    public float CameraRightXLimit = 39.7f;

    public bool isFrozen = false;

    private void Start()
    {
        player = GameObject.Find("Fighter").GetComponent<Transform>();
       
    }
    void Update()
    {
        if(player.transform.position.x > CameraLeftXLimit && player.transform.position.x < CameraRightXLimit && !isFrozen)
        {
            transform.position = new Vector3(player.position.x + offset.x, transform.position.y, offset.z); // Camera follows the player with specified offset position
        }
            
        
        
    }

}
