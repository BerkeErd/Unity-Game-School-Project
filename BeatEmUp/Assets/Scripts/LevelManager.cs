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

    public void LevelEnded()
    {
        
    }

    public void LoadNextLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

