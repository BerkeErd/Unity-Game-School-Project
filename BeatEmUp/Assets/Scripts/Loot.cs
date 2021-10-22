using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public string Name;
    public int ID;
    public AudioClip DropSound;
    public AudioClip CollectSound;
    public float DropRate;
    public int HealAmount = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement Player = GameObject.Find("Fighter").GetComponent<PlayerMovement>();

        if (collision.gameObject.name == "Fighter")
        {

            Player.Heal(HealAmount);
            Destroy(gameObject);

        }
    }
   
}
