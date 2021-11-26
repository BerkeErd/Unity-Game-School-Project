using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillMenuController : MonoBehaviour
{

    public Skills TempSkills;

    public int TempStr = 0,  TempSta = 0, TempSkillsPoints=0;
    public float TempAgi = 0, TempLck = 0;
    public List<Image> StrImages, AgiImages, StaImages, LckImages; 
    public Button StrPlus, StrMinus, AgiPlus, AgiMinus, StaPlus, StaMinus, LckPlus, LckMinus, CancelButton, ApplyButton, ContinueButton, MainMenu;
    public Text StatsText, HealthText, PunchDamageText, KickDamageText, AttackSpeedText, LuckText;
    public Skills skills;
    public SaveData saveData;

    private void Awake()
    {
        skills = GetComponent<Skills>();

        CancelButton = GameObject.Find("CancelButton").GetComponent<Button>();
        ApplyButton = GameObject.Find("ApplyButton").GetComponent<Button>();
        ContinueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        MainMenu = GameObject.Find("MainMenu").GetComponent<Button>();

        StrPlus = GameObject.Find("Strength+").GetComponent<Button>();
        StrMinus = GameObject.Find("Strength-").GetComponent<Button>();
        AgiPlus = GameObject.Find("Agility+").GetComponent<Button>();
        AgiMinus = GameObject.Find("Agility-").GetComponent<Button>();
        StaPlus = GameObject.Find("Stamina+").GetComponent<Button>();
        StaMinus = GameObject.Find("Stamina-").GetComponent<Button>();
        LckPlus = GameObject.Find("Luck+").GetComponent<Button>();
        LckMinus = GameObject.Find("Luck-").GetComponent<Button>();

        StatsText = GameObject.Find("StatsText").GetComponent<Text>();
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        PunchDamageText = GameObject.Find("PunchDamageText").GetComponent<Text>();
        KickDamageText = GameObject.Find("KickDamageText").GetComponent<Text>();
        AttackSpeedText = GameObject.Find("AttackSpeedText").GetComponent<Text>();
        LuckText = GameObject.Find("LuckRatioText").GetComponent<Text>();

        saveData = GameObject.Find("Main Camera").GetComponent<SaveData>();

    }

    private void Start()
    {
        TempSkillsPoints = skills.skillpoints;

        StrPlus.onClick.AddListener(ChangePlusStr);
        StrMinus.onClick.AddListener(ChangeMinusStr);

        AgiPlus.onClick.AddListener(ChangePlusAgi);
        AgiMinus.onClick.AddListener(ChangeMinusAgi);

        StaPlus.onClick.AddListener(ChangePlusSta);
        StaMinus.onClick.AddListener(ChangeMinusSta);

        LckPlus.onClick.AddListener(ChangePlusLck);
        LckMinus.onClick.AddListener(ChangeMinusLck);

        ApplyButton.onClick.AddListener(ChangeSkillValue);
        
        skillBar();
    }

    void ShowStatsTexts()
    {
        int Maxhealth = skills.extraHealth + 100;

        StatsText.text = "Stat Points : " + skills.skillpoints;
        HealthText.text = "Health : " + Maxhealth;
        PunchDamageText.text = "Punch Damage : " + skills.punchDamage;
        KickDamageText.text = "Kick Damage : " + skills.kickDamage;
        AttackSpeedText.text = "Attack Speed : " + skills.agiRatio;
        LuckText.text = "Luck : " + skills.luckRatio;

        if(skills.skillpoints > TempSkillsPoints)
        {
            StatsText.text = "Stat Points : " + skills.skillpoints + "<color=#ff0000ff>" + -(skills.skillpoints-TempSkillsPoints)+"</color>";
        }

        if (TempStr > 0)
        {
            PunchDamageText.text = "Punch Damage : " + skills.punchDamage + "<color=#00ff00ff>" + "+" + (TempStr + TempStr/10) + "</color>";
            KickDamageText.text = "Kick Damage : " + skills.kickDamage + "<color=#00ff00ff>" + "+"    + (TempStr + TempStr/10) + "</color>";
        }

        if (TempAgi > 0)
        {
            AttackSpeedText.text = "Attack Speed : " + skills.agiRatio + "<color=#00ff00ff>" + "+" + (TempAgi/100) + "</color>";           
        }

        if (TempSta > 0)
        {
            HealthText.text = "Health : " + Maxhealth + "<color=#00ff00ff>" + "+" + (TempSta * 10) + "</color>";          
        }

        if (TempLck > 0)
        {
            LuckText.text = "Luck : " + skills.luckRatio + "<color=#00ff00ff>" + "+" + (TempLck/5) + "</color>";          
        }
    }

 
    void skillBar()
    {
        ShowStatsTexts();
        MinusButton();
        PlusButton();
        
        for (int i = 0; i < 10; i++)
        {
            if(i<skills.str + TempStr)
            {
                StrImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                StrImages[i].GetComponent<Image>().enabled = false;
            }

            if (i < skills.agi + TempAgi)
            {
                AgiImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                AgiImages[i].GetComponent<Image>().enabled = false;
            }


            if (i < skills.sta + TempSta)
            {
                StaImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                StaImages[i].GetComponent<Image>().enabled = false;
            }


            if (i < skills.lck + TempLck)
            {
                LckImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                LckImages[i].GetComponent<Image>().enabled = false;
            }
        }    
    }

    void otherButtons()
    {
        CancelButton.gameObject.GetComponent<Button>().interactable = false;
        ApplyButton.gameObject.GetComponent<Button>().interactable = false;
        ContinueButton.gameObject.GetComponent<Button>().interactable = false;
        
        if(TempAgi + TempLck + TempSta + TempStr > 0)
        {
            ApplyButton.gameObject.GetComponent<Button>().interactable = true;
            CancelButton.gameObject.GetComponent<Button>().interactable = true;
        }

        //if() //Devam etme butonu bu ekrana bölüm sonundan gelindiyse olacak eğer menüden girildiyse o buton çalışmayacak
    }

    void MinusButton()
    {
        StrMinus.gameObject.GetComponent<Button>().interactable = false;
        AgiMinus.gameObject.GetComponent<Button>().interactable = false;
        StaMinus.gameObject.GetComponent<Button>().interactable = false;
        LckMinus.gameObject.GetComponent<Button>().interactable = false;

        if (TempStr > 0)
        {
            StrMinus.gameObject.GetComponent<Button>().interactable = true;
        }

        if (TempAgi > 0)
        {
            AgiMinus.gameObject.GetComponent<Button>().interactable = true;
        }

        if (TempSta > 0)
        {
            StaMinus.gameObject.GetComponent<Button>().interactable = true;
        }

        if (TempLck > 0)
        {
            LckMinus.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    void PlusButton()
    {
        StrPlus.gameObject.GetComponent<Button>().interactable = false;
        AgiPlus.gameObject.GetComponent<Button>().interactable = false;
        StaPlus.gameObject.GetComponent<Button>().interactable = false;
        LckPlus.gameObject.GetComponent<Button>().interactable = false;

        if (TempSkillsPoints > 0)
        {
            if(skills.str + TempStr < 10)
            {
                StrPlus.gameObject.GetComponent<Button>().interactable = true;
            }

            if(skills.agi + TempAgi < 10)
            {
                AgiPlus.gameObject.GetComponent<Button>().interactable = true;
            }

            if (skills.sta + TempSta < 10)
            {
                StaPlus.gameObject.GetComponent<Button>().interactable = true;
            }

            if (skills.lck + TempLck < 10)
            {
                LckPlus.gameObject.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ChangePlusStr()
    {
        if (TempStr<10-skills.str)
        {
            TempStr += 1;
            TempSkillsPoints -= 1;
            skillBar();
        }   
    }
    public void ChangeMinusStr()
    {
        if(TempStr>0)
        {
            TempStr -= 1;
            TempSkillsPoints += 1;
            skillBar();
        }     
    }

    public void ChangePlusAgi()
    {
        if (TempAgi < 10 - skills.agi)
        {
            TempAgi += 1;
            TempSkillsPoints -= 1;
            skillBar();
        }
    }
    public void ChangeMinusAgi()
    {
        if (TempAgi > 0)
        {
            TempAgi -= 1;
            TempSkillsPoints += 1;
            skillBar();
        }
    }

    public void ChangePlusSta()
    {
        if (TempSta < 10 - skills.sta)
        {
            TempSta += 1;
            TempSkillsPoints -= 1;
            skillBar();
        }
    }
    public void ChangeMinusSta()
    {
        if (TempSta > 0)
        {
            TempSta -= 1;
            TempSkillsPoints += 1;
            skillBar();
        }
    }

    public void ChangePlusLck()
    {
        if (TempLck < 10 - skills.lck)
        {
            TempLck += 1;
            TempSkillsPoints -= 1;
            skillBar();
        }
    }
    public void ChangeMinusLck()
    {
        if (TempLck > 0)
        {
            TempLck -= 1;
            TempSkillsPoints += 1;
            skillBar();
        }
    }

    public void ChangeSkillValue()
    {
        skills.str += TempStr;
        skills.agi += TempAgi;
        skills.sta += TempSta;
        skills.lck += TempLck;

        TempStr = 0;
        TempAgi = 0;
        TempSta = 0;
        TempLck = 0;

        skills.skillpoints = TempSkillsPoints;

        skills.Gold = saveData.Gold;
        saveData.save();
        skillBar();
    }


    
    
    // X butonuna basılırsa sayfa tekrardan yüklenebilir.


}
