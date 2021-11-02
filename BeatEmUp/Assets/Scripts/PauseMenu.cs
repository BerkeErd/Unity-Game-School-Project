using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button PauseButton, ContinueButton;
    public bool pause = false;
    public GameObject pauseMenu;
    public Joystick joystick;

    void Start()
    {
        joystick = GetComponent<Joystick>();
        PauseButton.onClick.AddListener(Pause);
        ContinueButton.onClick.AddListener(Continue);
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        joystick.circle.gameObject.SetActive(false);
        joystick.outerCircle.gameObject.SetActive(false);
    }
    public void Continue()
    {
        pauseMenu.SetActive(false);
        joystick.circle.gameObject.SetActive(true);
        joystick.outerCircle.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
