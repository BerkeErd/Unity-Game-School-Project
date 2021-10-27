using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Str, Sta, Lck, SkillPoint;
    public float Agi;
    // Level ?

    public PlayerData(Skills skills)
    {
        Str = skills.str;
        Agi = skills.agi;
        Sta = skills.sta;
        Lck = skills.lck;
        SkillPoint = skills.skillpoints;
    }
}
