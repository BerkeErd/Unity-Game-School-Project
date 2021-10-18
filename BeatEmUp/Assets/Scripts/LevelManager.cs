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
        Level = SceneManager.GetActiveScene().buildIndex;
        // save dosyasından level çekilecek


        LevelName = SceneManager.GetActiveScene().name;
        if(GameObject.Find("LevelName"))
        {
            LevelNameText = GameObject.Find("LevelName").GetComponent<Text>();
            LevelNameText.text = LevelName;
        }
       
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadTestMap()
    {
        SceneManager.LoadScene("TileMapTest");
    }
}
