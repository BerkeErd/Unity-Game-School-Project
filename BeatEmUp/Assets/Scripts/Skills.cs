using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{

    public int str=0; //strenght
    public float agi=0; //agility
    public int sta=0; //stamina
    public int lck=0; //luck
    public int skillpoints = 0;

 
    public void save()
    {
        SaveSystem.SavePlayerSkills(this);
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
        
    }

}
