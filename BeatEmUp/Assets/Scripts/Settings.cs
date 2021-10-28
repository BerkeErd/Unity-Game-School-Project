using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Button SoundButton, MainMenuButton;
    public GameObject MuteImage;
    public GameObject UnMuteImage;
    public bool Sound;
    public Slider Volume;
    public float TempVolume;
    // Start is called before the first frame update
    void Start()
    {
        Sound = true;
        ChangeButtonImage();

        SoundButton.onClick.AddListener(ChangeButtonImage);
        MainMenuButton.onClick.AddListener(LoadMainMenu);
        Volume.onValueChanged.AddListener(delegate { SliderValue(); });
        
    }

    public void SliderValue()
    {
        if(Volume.value != 0)
        {
            TempVolume = Volume.value;
        }
    }

    public void ChangeButtonImage()
    {
        if(Sound == true)
        {
            MuteImage.gameObject.SetActive(false);
            UnMuteImage.gameObject.SetActive(true);
            Volume.value = TempVolume;
            Sound = false;
        }

        else
        {
            UnMuteImage.gameObject.SetActive(false);
            MuteImage.gameObject.SetActive(true);
            Volume.value = 0;
            Sound = true;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
