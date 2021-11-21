using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Slider ProgressBar;
    public Image Background;
    public List<Sprite> Backgrounds;
    public LevelManager levelmanager;

    public Gradient gradient;
    public Image fill;

    // Use this for initialization
    void Start()
    {
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        if(levelmanager.Level < 11)
        {
            Background.sprite = Backgrounds[0];
        }
        else if (levelmanager.Level < 21)
        {
            Background.sprite = Backgrounds[1];
        }
        else
        {
            Background.sprite = Backgrounds[2];
        }
        StartCoroutine(LoadAsyncOperation());
    }

    // Update is called once per frame
    IEnumerator LoadAsyncOperation()
    {
        yield return null;

        AsyncOperation gamelevel = levelmanager.SendLevelInfo();
        while (gamelevel.progress <= 1)
        {
            ProgressBar.value = gamelevel.progress;
            fill.color = gradient.Evaluate(ProgressBar.normalizedValue);
            yield return new WaitForEndOfFrame();
        }


    }
}


