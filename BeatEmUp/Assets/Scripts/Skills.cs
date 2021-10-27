using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        CalculateStats();
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

}
