using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemyChecker : MonoBehaviour
{
    public int EnemyCount = 0;
    public int EnemiesNeedToKill = 0;

    public LevelManager levelmanager;

    // Start is called before the first frame update
    void Start()
    {
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

       
    }

    public void CountEnemies()
    {
        foreach (var Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyCount += 1;
        }

        foreach (var Spawner in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            EnemyCount += Spawner.GetComponent<Spawner>().EnemyNumber;
        }

        EnemiesNeedToKill = EnemyCount;
    }

   public void CheckLevelEnd()
    {

        if(EnemyCount <= 0)
        {
            //Level sonu ekranı belirir
        }

    }
}
