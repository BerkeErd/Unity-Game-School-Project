using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skills : MonoBehaviour
{

    public int str;
    public float agi;
    public int sta;
    public float lck;
    public int skillpoints;
    public int Gold;
    public int Exp;
    public int PlayerLevel;

    public int BasepunchDamage = 10;
    public int BasekickDamage = 20;

    public int punchDamage;
    public int kickDamage;

    public float agiRatio;
    public int extraHealth;
    public float luckRatio;
    public float strRatio;

    
   
    public int changeSpeed;

    public LevelManager levelManager;
    public Text LevelText;
    public Slider ExpBar;

    

    private void Awake()
    {
        CalculateStats();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void CalculateStats()
    {
        strRatio = str + str / 10;
        agiRatio = agi / 100;
        luckRatio = lck / 5; 
        punchDamage = BasepunchDamage + (int)strRatio;
        kickDamage = BasekickDamage + (int)strRatio;
        extraHealth = sta * 10;
    }

    public void save()
    {
        SaveSystem.SavePlayerSkills(this);
        CalculateStats();
    }

    public void load()
    {
        
       PlayerData data = SaveSystem.LoadPlayerSkills();
        if(data != null)
        {
            str = data.Str;
            agi = data.Agi;
            sta = data.Sta;
            lck = data.Lck;
            skillpoints = data.SkillPoint;
            Exp = data.Exp;
            Gold = data.Gold;
            PlayerLevel = data.PlayerLevel;
        }
        else
        {
            newPlayer();
        }

        CalculateStats();
    }
    
    public void newPlayer()
    {
        Debug.Log("Yeni Oyuncu Oluşturuldu");
        str = 0;
        agi = 0;
        sta = 0;
        lck = 0;
        skillpoints = 0;
        Exp = 0;
        Gold = 0;
        PlayerLevel = 0;

        save();
        Debug.Log("Yeni Oyuncu Kaydedildi");
    }

    public void GainExp()
    {
        Exp += levelManager.Level * 5; // Enemy'de yazmalı
        if (Exp >= 100)
        {
            PlayerLevel++;
            skillpoints++;
            Exp -= 100;
        }
        LevelText.text = "Level : " + PlayerLevel;
    }
    public void FixedUpdate()
    {
        if(Exp>0 && Exp<=100)
        {
            ExpBar.value = Mathf.MoveTowards(ExpBar.value, Exp, changeSpeed * Time.fixedDeltaTime);
        }    
    }
}
