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
    public SaveData savedata;


    private void Start()
    {
        savedata = GameObject.Find("Main Camera").GetComponent<SaveData>();
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
        if (Time.timeScale==0)
        {
            Time.timeScale = 1;
        }

        if (Level < 11 && SceneManager.GetActiveScene().name == "LEVEL 01")
        {
            SceneManager.LoadScene("LEVEL 01");
        }
        else if (Level < 21 && SceneManager.GetActiveScene().name == "LEVEL 02")
        {
            SceneManager.LoadScene("LEVEL 02");
        }
        else if (Level < 31 && SceneManager.GetActiveScene().name == "LEVEL 03")
        {
            SceneManager.LoadScene("LEVEL 03");
        }

        else
        {
            SceneManager.LoadScene("LoadingScreen");
        }

            
        
    }

    public void loadTest()
    {
        SceneManager.LoadScene("Test Scene");
    }

    public AsyncOperation SendLevelInfo()
    {
       

        if (Level < 11)
        {
           return SceneManager.LoadSceneAsync("LEVEL 01");
        }
        else if (Level < 21)
        {
            return SceneManager.LoadSceneAsync("LEVEL 02");
        }
        else
        {
            return SceneManager.LoadSceneAsync("LEVEL 03");
        }


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
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadSettingScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("StoreMenu");
    }

    public void LoadNewPlayer()
    {
        Level = 1;
        savedata.newPlayer();
        PlayCurrentLevel();
    }

}

