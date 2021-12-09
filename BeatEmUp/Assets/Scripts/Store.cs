using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    public List<Image> PunchImage, KickImage, PunchSkillImage, KickSkillImage;
    public Button KickUpgradeButton, PunchUpgradeButton, PunchSkillButton, KickSkillButton, MainMenuButton, PlayButton;
    public Text KickStats, PunchStats, PunchUpgradePriceText, KickUpgradePriceText, PunchSkillPriceText, KickSkillPriceText, KickSkillStats,PunchSkillStats;
    public int PunchUpgradePrice, KickUpgradePrice, PunchSkillPrice, KickSkillPrice;
    public SaveData saveData;
    public Skills skills;
    public Text GoldText;

    public void Awake()
    {
        GoldText = GameObject.Find("GoldText").GetComponent<Text>();
        KickSkillStats = GameObject.Find("KickSkillDamageText").GetComponent<Text>();
        PunchSkillStats = GameObject.Find("PunchSkillDamageText").GetComponent<Text>();
        MainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        PlayButton = GameObject.Find("PlayButton").GetComponent<Button>();
        PunchUpgradeButton = GameObject.Find("PunchUpgradeButton").GetComponent<Button>();
        KickUpgradeButton = GameObject.Find("KickUpgradeButton").GetComponent<Button>();
        PunchSkillButton = GameObject.Find("PunchSkillBuy").GetComponent<Button>();
        KickSkillButton = GameObject.Find("KickSkillBuy").GetComponent<Button>();

        PunchStats = GameObject.Find("PunchDamageText").GetComponent<Text>();
        KickStats = GameObject.Find("KickDamageText").GetComponent<Text>();

        KickUpgradePriceText = GameObject.Find("KickUpgradePrice").GetComponent<Text>();
        PunchUpgradePriceText = GameObject.Find("PunchUpgradePrice").GetComponent<Text>();

        KickSkillPriceText = GameObject.Find("KickSkillPrice").GetComponent<Text>();
        PunchSkillPriceText = GameObject.Find("PunchSkillPrice").GetComponent<Text>();

        skills = GameObject.Find("Skills").GetComponent<Skills>();

        saveData = GameObject.Find("Main Camera").GetComponent<SaveData>();

    }

    public void Start()
    {
        PunchUpgradePriceText.text = "" + PunchUpgradePrice;
        KickUpgradePriceText.text = "" + KickUpgradePrice;
        PunchSkillPriceText.text = "" + PunchSkillPrice;
        KickSkillPriceText.text = "" + KickSkillPrice;

        PunchUpgradeButton.onClick.AddListener(delegate { Buy("PunchUpgrade"); });
        KickUpgradeButton.onClick.AddListener(delegate { Buy("KickUpgrade"); });
        PunchSkillButton.onClick.AddListener(delegate { Buy("PunchSkill"); });
        KickSkillButton.onClick.AddListener(delegate { Buy("KickSkill"); });

        UpdatePrice();
        DisableButton();
        skillBar();
        ShowStats();
        
    }


    public void UpdatePrice()
    {
        PunchUpgradePrice = 100 + 100 * skills.PunchUpgrade;
        KickUpgradePrice = 100 + 100 * skills.KickUpgrade;
        PunchSkillPrice = 100 + 100 * skills.PunchSkill;
        KickSkillPrice = 100 + 100 * skills.KickSkill;

        PunchUpgradePriceText.text = PunchUpgradePrice.ToString();
        KickUpgradePriceText.text = KickUpgradePrice.ToString();
        PunchSkillPriceText.text = PunchSkillPrice.ToString();
        KickSkillPriceText.text = KickSkillPrice.ToString();
    }
    public void ShowStats()
    {
        PunchStats.text = "Punch Damage : " + (skills.punchDamage + skills.PunchUpgrade * 5); // Eğer burda ki 5 kat değişirse playercombat'dan da değiştirmemiz lazım
        KickStats.text = "Kick Damage : " + (skills.kickDamage + skills.KickUpgrade * 5); // Eğer burda ki 5 kat değişirse playercombat'dan da değiştirmemiz lazım
        KickSkillStats.text = "Kick Skill Damage : " + (saveData.KickSkill * (skills.kickDamage  + skills.KickUpgrade * 5) * 2);
        PunchSkillStats.text = "Punch Skill Damage : " + (saveData.PunchSkill * (skills.punchDamage  + skills.PunchUpgrade * 5) * 3);
        GoldText.text = skills.Gold.ToString();
    }
    public void skillBar()
    {
        //Imageleri 5 arttırmamın sebebi ilk 5 image siyah arkaplan sonraki 5 image ise yetenek barını dolduracak image
        for (int i = 0; i < 5; i++)
        {
            if (i < skills.PunchUpgrade)
            {
                PunchImage[i + 5].GetComponent<Image>().enabled = true;
            }
            else
            {
                PunchImage[i + 5].GetComponent<Image>().enabled = false;
            }

            if (i < skills.KickUpgrade)
            {
                KickImage[i + 5].GetComponent<Image>().enabled = true;
            }
            else
            {
                KickImage[i + 5].GetComponent<Image>().enabled = false;
            }

        }
        for (int i = 0; i < 2; i++)
        {
            if (i < skills.PunchSkill)
            {
                PunchSkillImage[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                PunchSkillImage[i].GetComponent<Image>().enabled = false;
            }

            if (i < skills.KickSkill)
            {
                KickSkillImage[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                KickSkillImage[i].GetComponent<Image>().enabled = false;
            }
        }

    }

    public void Buy(string item)
    {
        if (skills.Gold >= PunchUpgradePrice && skills.PunchUpgrade < 5 && item == "PunchUpgrade")
        {
            skills.PunchUpgrade += 1;
            skills.Gold -= PunchUpgradePrice;
            if (skills.PunchUpgrade < 5)
            {
                PunchUpgradePrice = PunchUpgradePrice + (PunchUpgradePrice * skills.PunchUpgrade);
                PunchUpgradePriceText.text = "" + (PunchUpgradePrice);
            }
        }

        else if (skills.Gold >= KickUpgradePrice && skills.KickUpgrade < 5 && item == "KickUpgrade")
        {
            skills.KickUpgrade += 1;
            skills.Gold -= KickUpgradePrice;
            if (skills.KickUpgrade < 5)
            {
                KickUpgradePrice = KickUpgradePrice + (KickUpgradePrice * skills.KickUpgrade);
                KickUpgradePriceText.text = "" + (KickUpgradePrice);
            }
        }

        else if (skills.Gold >= PunchSkillPrice && skills.PunchSkill < 2 && item == "PunchSkill")
        {
            skills.PunchSkill += 1;
            skills.Gold -= PunchSkillPrice;
            if (skills.PunchSkill < 2)
            {
                PunchSkillPrice = PunchSkillPrice + (PunchSkillPrice * skills.PunchSkill);
                PunchSkillPriceText.text = "" + (PunchSkillPrice);
            }
        }

        else if (skills.Gold >= KickSkillPrice && skills.KickSkill < 2 && item == "KickSkill")
        {
            skills.KickSkill += 1;
            skills.Gold -= KickSkillPrice;
            if (skills.KickSkill < 2)
            {
                KickSkillPrice = KickSkillPrice + (KickSkillPrice * skills.KickSkill);
                KickSkillPriceText.text = "" + (KickSkillPrice);
            }

        }
        UpdatePrice();
        DisableButton();
        skillBar();
        ShowStats();
        saveData.save();
    }

    public void DisableButton()
    {
        Debug.Log("disable butona girdi");
        if ((skills.Gold < PunchUpgradePrice || skills.PunchUpgrade == 5) && PunchUpgradeButton.interactable)
        {
            PunchUpgradeButton.interactable = false;
            Debug.Log("disable buton punch upgrade girdi");
        }

        if ((skills.Gold < KickUpgradePrice || skills.KickUpgrade == 5) && KickUpgradeButton.interactable)
        {
            KickUpgradeButton.interactable = false;
        }

        if ((skills.Gold < PunchSkillPrice || skills.PunchSkill == 2) && PunchSkillButton.interactable)
        {
            PunchSkillButton.interactable = false;
        }

        if ((skills.Gold < KickSkillPrice || skills.KickSkill == 2) && KickSkillButton.interactable)
        {
            KickSkillButton.interactable = false;
        }
    }
}
