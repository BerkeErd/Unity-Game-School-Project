using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button NewGameButton, ContinueButton, SettingsButton;
    public Canvas Settings;

    public SaveData savedata;
    // Start is called before the first frame update
    private void Awake()
    {
        Settings = GameObject.Find("SettingsPanel").GetComponent<Canvas>();
    }
    void Start()
    {

        Settings.gameObject.SetActive(false);
        SettingsButton = GameObject.Find("Settings Button").GetComponent<Button>();
        NewGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        ContinueButton = GameObject.Find("ContinueGameButton").GetComponent<Button>();

        savedata = GameObject.Find("Main Camera").GetComponent<SaveData>();

        if(savedata.NewPlayer)
        {
            ContinueButton.interactable = false;
        }
        else
        {
            ContinueButton.interactable = true;
        }

        
    }

   public void SettingsButtonClick()
    {
        Settings.gameObject.SetActive(true);
    }
}
