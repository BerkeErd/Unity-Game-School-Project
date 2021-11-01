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
    public int Exp;
    public int PlayerLevel;
    //public int currentStageLevel;
    public int Gold;

    public int BasepunchDamage = 10;
    public int BasekickDamage = 20;

    public int punchDamage;
    public int kickDamage;

    public float agiRatio;
    public int extraHealth;
    public float luckRatio;
    public float strRatio;

    public SaveData saveData;


    private void Awake()
    {
        saveData = GameObject.Find("Main Camera").GetComponent<SaveData>();
       
        CalculateStats();
        
    }

    public void CalculateStats()
    {
        strRatio = str + str / 10;
        agiRatio = agi / 100;
        luckRatio = lck / 5; 
        punchDamage = BasepunchDamage + (int)strRatio;
        kickDamage = BasekickDamage + (int)strRatio;
        extraHealth = sta * 10;
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
        //Gold = 0;
        PlayerLevel = 1;
        //currentStageLevel = 1;

        saveData.save();
        Debug.Log("Yeni Oyuncu Kaydedildi");
    }

   
    

    
}
