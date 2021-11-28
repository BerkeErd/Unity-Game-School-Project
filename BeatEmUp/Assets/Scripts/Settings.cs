using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer MusicMixer, SoundsMixer;
    public Button MusicButton, SoundButton;
    public Image MusicImage, SoundsImage;
    public Sprite MusicMuted, SoundsMuted, SoundunMuted, MusicunMuted;
    public Slider MusicSlider, SoundsSlider;

    private void Start()
    {
        MusicImage = GameObject.Find("MusicIcon").GetComponent<Image>();
        SoundsImage = GameObject.Find("SoundsIcon").GetComponent<Image>();
        
    }

    public void SetSoundLevel(float SliderValue)
    {
        SoundsMixer.SetFloat("SoundVol", Mathf.Log10(SliderValue) * 20);
        if(SliderValue <= 0.0001f)
        {
            SoundsImage.sprite = SoundsMuted;
        }
        else
        {
            SoundsImage.sprite = SoundunMuted;
        }
    }

    public void SetMusicLevel(float SliderValue)
    {
        MusicMixer.SetFloat("MusicVol", Mathf.Log10(SliderValue)*20);
        if (SliderValue == 0.0001f)
        {
            MusicImage.sprite = MusicMuted;
           
        }
        else
        {
            MusicImage.sprite = MusicunMuted;
           
        }
    }

    public void Done()
    {
        this.gameObject.SetActive(false);
    }

    
}
