using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public string LevelName;
    public Text LevelNameText;

    private void Start()
    {
        LevelName = SceneManager.GetActiveScene().name;
        if(LevelNameText)
        LevelNameText.text = LevelName;
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
