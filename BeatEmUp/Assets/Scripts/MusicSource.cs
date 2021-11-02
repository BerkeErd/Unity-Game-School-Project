using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSource : MonoBehaviour
{
    public AudioClip MainMenuTheme,SkillMenuTheme,Level1Theme, Level2Theme, Level3Theme, Level1BossTheme, Level2BossTheme, Level3BossTheme, ManyEnemiesTheme, NowPlaying;
    private AudioSource source;
    public LevelManager levelmanager;

    void Start()
    {
           levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
           source = GetComponent<AudioSource>();

        if(GameObject.Find("Fighter"))
        {
            ChangeMusic(levelmanager.Level);
            
        }
        else if(GameObject.Find("PlayButton"))
        {
            source.clip = MainMenuTheme;
            source.Play();
        }
        else if (GameObject.Find("SkillMenuController"))
        {
            source.clip = SkillMenuTheme;
            source.Play();
        }

    }


    public void ChangeMusic(int Level, bool Boss = false, bool ManyEnemies = false)
    {
        
        if(Level < 11)
        {
            if(Boss)
            {
                NowPlaying = Level1BossTheme;
                StartCoroutine(NewMusic(source, NowPlaying));
            }
            else if(ManyEnemies)
            {
                NowPlaying = ManyEnemiesTheme;
                StartCoroutine(NewMusic(source, NowPlaying));
            }
            else
            {
                NowPlaying = Level1Theme;
            }
        }
       else if (Level < 21)
        {
            if (Boss)
            {
                NowPlaying = Level2BossTheme;
                StartCoroutine(NewMusic(source, NowPlaying));
            }
            else if (ManyEnemies)
            {
                NowPlaying = ManyEnemiesTheme;
                StartCoroutine(NewMusic(source,  NowPlaying));
            }
            else
            {
                NowPlaying = Level2Theme;
            }
        }
       else if (Level < 31)
        {
            if (Boss)
            {
                NowPlaying = Level3BossTheme;
                StartCoroutine(NewMusic(source,  NowPlaying));
            }
            else if (ManyEnemies)
            {
                NowPlaying = ManyEnemiesTheme;
                StartCoroutine(NewMusic(source,  NowPlaying));
            }
            else
            {
                NowPlaying = Level3Theme;
            }
        }

        source.clip = NowPlaying;
        source.Play();

    }



    public IEnumerator NewMusic(AudioSource audioSource, AudioClip newMusic)
    {
        float currentTime = 0;
        float startVolume = audioSource.volume;
        float duration = 1;
        float targetVolume = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        source.clip = newMusic;
        source.Play();
        currentTime = 0;

        float currentVolume = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(currentVolume, startVolume, currentTime / duration);
            yield return null;
        }

        yield break;
    }
}
