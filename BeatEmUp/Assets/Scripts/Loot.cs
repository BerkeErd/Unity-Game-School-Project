﻿using System.Collections;
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
    public int Gold = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCombat Player = GameObject.Find("Fighter").GetComponent<PlayerCombat>();
        Skills skills = GameObject.Find("Fighter").GetComponent<Skills>();

        if (collision.gameObject.name == "Fighter")
        {
            if(Name=="Watermelon")
            {
                Player.Heal(HealAmount);
            }
            if(Name == "Gold")
            {
                skills.Gold += Gold;
            }
            Destroy(gameObject);
        }
    }
   
}
