using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillMenuController : MonoBehaviour
{

    public int TempStr = 0, TempAgi = 0, TempSta = 0, TempLck = 0, TempSkillsPoints=0;
    public List<Image> StrImages, AgiImages, StaImages, LckImages;
    //public List<Button> PlusButtons;
    //public List<Button> MinusButtons;
    public Button StrPlus, StrMinus, AgiPlus, AgiMinus, StaPlus, StaMinus, LckPlus, LckMinus, CancelButton, ApplyButton, ContinueButton, MainMenu;
    public Skills skills;

    private void Awake()
    {
        CancelButton = GameObject.Find("CancelButton").GetComponent<Button>();
        ApplyButton = GameObject.Find("ApplyButton").GetComponent<Button>();
        ContinueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        StrPlus = GameObject.Find("Strength+").GetComponent<Button>();
        StrMinus = GameObject.Find("Strength-").GetComponent<Button>();
        AgiPlus = GameObject.Find("Agility+").GetComponent<Button>();
        AgiMinus = GameObject.Find("Agility-").GetComponent<Button>();
        StaPlus = GameObject.Find("Stamina+").GetComponent<Button>();
        StaMinus = GameObject.Find("Stamina-").GetComponent<Button>();
        LckPlus = GameObject.Find("Luck+").GetComponent<Button>();
        LckMinus = GameObject.Find("Luck-").GetComponent<Button>();
        MainMenu = GameObject.Find("MainMenu").GetComponent<Button>();
        skills = GetComponent<Skills>();

    }

    private void Start()
    {
        //- Butonları kaybolacak


        //Player save datası buraya çekilip içindeki skills özellikleri buradaki intlere dolduralacak.
        //Player Skills yüklenecek.
        //Player Skills'e göre STR AGI vs. Attributelarının imageleri ayarlanacak.

        //Player'ın harcayabileceği skill point sayısı ekranda yazacak ve 0'dan yüksekse + butonları oluşacak (Eğer skill maxiumumsa + oluşmayacak)
        //Devam etme butonu bu ekrana bölüm sonundan gelindiyse olacak eğer menüden girildiyse o buton görünmeyecek 

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
        MainMenu.onClick.AddListener(LoadMainMenu);
        ContinueButton.onClick.AddListener(LoadContinue);
        skillBar();


    }


    void skillBar()
    {
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
        if(TempStr<10-skills.str)
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

        skillBar();

        skills.save();
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadContinue()
    {

    }

    //+ Butonuna basıldığında + Butonu hangi özelliğe denk geliyorsa o 1 artacak ve Image'ı da 1 kare yükselecek ve - Butonu oluşacak. Playerın özelliği de 1 artacak fakat tik butonuna basılmadığı sürece save edilmeyecek.
    // X butonuna basılırsa sayfa tekrardan yüklenebilir.


}
