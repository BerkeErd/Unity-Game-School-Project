using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemyChecker : MonoBehaviour
{
    public int EnemyCount = 0;
    public int EnemiesNeedToKill = 0;

    public GameObject NextLevelMenu;
    public LevelManager levelmanager;
    public SaveData saveData;
    public Spawner Spawner;

    private void Awake()
    {
        Spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }
    void Start()
    {
        NextLevelMenu = GameObject.Find("NextLevelMenu");
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        saveData = GameObject.Find("Main Camera").GetComponent<SaveData>();

        NextLevelMenu.SetActive(false);
    }

    public void CountEnemies()
    {
        foreach (var Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyCount += 1;
        }

        EnemyCount += Spawner.EnemiesWillSpawn;
        EnemiesNeedToKill = EnemyCount;
    }

    private void CheckLevelEnd()
    {
        if (EnemyCount <= 0)
        {
            levelmanager.Level += 1;
            saveData.save();
            NextLevelMenu.SetActive(true);
        }
    }

    public void EnemyDied()
    {
        EnemyCount--;
        CheckLevelEnd();
    }
}
