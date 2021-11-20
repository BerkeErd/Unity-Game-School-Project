using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button NewGameButton, ContinueButton;

    public SaveData savedata;
    // Start is called before the first frame update
    void Start()
    {
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

   
}
