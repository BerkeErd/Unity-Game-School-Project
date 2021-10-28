using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skills : MonoBehaviour
{

    public int str=0; //strenght
    public float agi=0; //agility
    public int sta=0; //stamina
    public float lck=0; //luck
    public int skillpoints = 0;
    

    public int BasepunchDamage = 10;
    public int BasekickDamage = 20;

    public int punchDamage;
    public int kickDamage;

    public float agiRatio;
    public int extraHealth;
    public float luckRatio;
    public float strRatio;

    public int Exp;
    public int PlayerLevel;
    public int changeSpeed;

    public LevelManager levelManager;
    public Text LevelText;
    public Slider ExpBar;

    public int Gold;

    private void Awake()
    {
        CalculateStats();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void CalculateStats()
    {
        strRatio = str + str / 10;
        agiRatio = agi / 100;
        luckRatio = lck / 5; //Değişebilir
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
        }

        CalculateStats();
    }

    public void GainExp()
    {
        Exp += levelManager.Level * 5;
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
