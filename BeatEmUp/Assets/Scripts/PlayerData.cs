using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Str, Sta, SkillPoint, Gold, Exp, PlayerLevel;

    public float Agi,Lck;
    
    //public int currentStageLevel;

    public PlayerData(Skills skills)
    {
        PlayerLevel = skills.PlayerLevel;
        Exp = skills.Exp;
        Gold = skills.Gold;
        Str = skills.str;
        Agi = skills.agi;
        Sta = skills.sta;
        Lck = skills.lck;
        SkillPoint = skills.skillpoints;
    }
}
