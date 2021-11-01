using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int Level;
    public string LevelName;
    public Text LevelNameText;

    private void Start()
    {
        //Level = GameObject.Find("Fighter").GetComponent<Skills>().currentStageLevel; // Saçma ama öyle 
       


        LevelName = SceneManager.GetActiveScene().name;
        if(GameObject.Find("LevelName"))
        {
            LevelNameText = GameObject.Find("LevelName").GetComponent<Text>();
            LevelNameText.text = "Level " + Level;
        }
    }

    public void PlayCurrentLevel()
    {
        
        if (Level < 11)
        {
            SceneManager.LoadScene("LEVEL 01");
        }
    }

    public void LoadNextLevel()
    {
        //LEVEL 1 ARTMALI
        PlayCurrentLevel();
    }

    public void LoadTestMap()
    {
        SceneManager.LoadScene("TileMapTest");
    }

    public void LoadSkillsScene()
    {
        SceneManager.LoadScene("SkillMenu");
    }

    public void LoadMainMenu()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("Settings");
    }


}

