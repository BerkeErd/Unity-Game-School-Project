using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuController : MonoBehaviour
{
    
    public int Str, Agi, Sta, Lck;
    public List<Image> StrImages,AgiImages,StaImages,LckImages;
    //public List<Button> PlusButtons;
    //public List<Button> MinusButtons;
    public Button StrPlus, StrMinus, AgiPlus, AgiMinus, StaPlus, StaMinus, LckPlus, LckMinus, CancelButton, ApplyButton, ContinueButton;

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
    }

    private void Start()
    {
        //- Butonları kaybolacak
        StrMinus.gameObject.SetActive(false);
        AgiMinus.gameObject.SetActive(false);
        StaMinus.gameObject.SetActive(false);
        LckMinus.gameObject.SetActive(false);

       
        //Player save datası buraya çekilip içindeki skills özellikleri buradaki intlere dolduralacak.
        //Player Skills yüklenecek.
        //Player Skills'e göre STR AGI vs. Attributelarının imageleri ayarlanacak.
        
        //Player'ın harcayabileceği skill point sayısı ekranda yazacak ve 0'dan yüksekse + butonları oluşacak (Eğer skill maxiumumsa + oluşmayacak)
        //Devam etme butonu bu ekrana bölüm sonundan gelindiyse olacak eğer menüden girildiyse o buton görünmeyecek
    }


    //+ Butonuna basıldığında + Butonu hangi özelliğe denk geliyorsa o 1 artacak ve Image'ı da 1 kare yükselecek ve - Butonu oluşacak. Playerın özelliği de 1 artacak fakat tik butonuna basılmadığı sürece save edilmeyecek.
    // X butonuna basılırsa sayfa tekrardan yüklenebilir.


}
