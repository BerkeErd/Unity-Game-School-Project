using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    //skills
    public int str;
    public int sta;
    public float agi;
    public float lck;
    public int skillpoints;
    public int Exp;
    public int PlayerLevel;

    //store
    //public int Gold;

    //LevelManager
    public int currentStageLevel;

    public Skills skills;
    public LevelManager levelManager;

    public void Awake()
    {
        if (GameObject.Find("Fighter"))
        {
            skills = GameObject.Find("Fighter").GetComponent<Skills>();
            LoadPlayer();
        }
        else if (GameObject.Find("SkillMenuController"))
        {
            skills = GameObject.Find("SkillMenuController").GetComponent<Skills>();
            LoadPlayer();
        }

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        LoadLevelManager();
        
    }
    public void UpdateSaveData()
    {
        str = skills.str;
        sta = skills.sta;
        agi = skills.agi;
        lck = skills.lck;
        skillpoints = skills.skillpoints;
        Exp = skills.Exp;
        PlayerLevel = skills.PlayerLevel;
        currentStageLevel = levelManager.Level;
    }

    public void save()
    {
        UpdateSaveData();
        SaveSystem.SavePlayerSkills(this);
        skills.CalculateStats();
    }



    public void LoadPlayer()
    {
        Debug.Log("Player Loaded");
        PlayerData data = SaveSystem.LoadPlayerSkills();
        if (data != null)
        {
            skills.str = data.Str;
            skills.agi = data.Agi;
            skills.sta = data.Sta;
            skills.lck = data.Lck;
            skills.skillpoints = data.SkillPoint;
            skills.Exp = data.Exp;
            //Gold = data.Gold;
            skills.PlayerLevel = data.PlayerLevel;      
        }
        else
        {
            skills.newPlayer();
        }
    }

    public void LoadLevelManager()
    {
        Debug.Log("Level Manager Loaded");
        PlayerData data = SaveSystem.LoadPlayerSkills();
        if (data != null)
        {
            levelManager.Level = data.currentStageLevel;
        }
        else
        {
            skills.newPlayer();
        }
    }
}
