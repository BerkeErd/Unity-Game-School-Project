using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Str, Sta, SkillPoint, Gold, Exp, PlayerLevel, currentStageLevel, KickSkill, PunchSkill, KickUpgrade, PunchUpgrade;

    public float Agi,Lck;
    
    //public int currentStageLevel;

    public PlayerData(SaveData saveData)
    {
        saveData.UpdateSaveData();
        PlayerLevel = saveData.PlayerLevel;
        Exp = saveData.Exp;
        Gold = saveData.Gold;
        Str = saveData.str;
        Agi = saveData.agi;
        Sta = saveData.sta;
        Lck = saveData.lck;
        SkillPoint = saveData.skillpoints;
        currentStageLevel = saveData.currentStageLevel;
        KickSkill = saveData.KickSkill;
        PunchSkill = saveData.PunchSkill;
        KickUpgrade = saveData.KickUpgrade;
        PunchUpgrade = saveData.PunchUpgrade;
    }
}
