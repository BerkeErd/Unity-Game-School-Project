using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    public string Name;
    public int ID;
    public AudioClip DropSound;
    public AudioClip CollectSound;
    public float DropRate;
    public int HealAmount = 0;
    public int Gold = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCombat Player = GameObject.Find("Fighter").GetComponent<PlayerCombat>();
        Skills skills = GameObject.Find("Fighter").GetComponent<Skills>();

        if (collision.gameObject.name == "Fighter")
        {
           Player.Heal(HealAmount);
           skills.Gold += Gold;
           collision.gameObject.GetComponent<AudioSource>().PlayOneShot(CollectSound);
           GameObject.Find("GoldText").GetComponent<Text>().text = "x "+skills.Gold;
           Debug.Log(collision.gameObject.name);
           Destroy(gameObject);
        }
    }
   
}
