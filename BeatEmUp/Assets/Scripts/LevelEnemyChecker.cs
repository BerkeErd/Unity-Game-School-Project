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
    GameObject Player;
    bool LootsAreFlying = false;


    private void Update()
    {
      if(LootsAreFlying)
        {
            foreach (var loot in GameObject.FindGameObjectsWithTag("Loot"))
            {
                loot.transform.position = new Vector2(Mathf.MoveTowards(loot.transform.position.x, Player.transform.position.x, 15 * Time.fixedDeltaTime),
                Mathf.MoveTowards(loot.transform.position.y, Player.transform.position.y, 10 * Time.fixedDeltaTime));
            }
        }
    }
    void Start()
    {
        Player = GameObject.Find("Fighter");
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
        Debug.Log(EnemyCount);
        EnemyCount += GameObject.Find("Spawner").GetComponent<Spawner>().EnemiesWillSpawn;
        Debug.Log(EnemyCount);
        EnemiesNeedToKill = EnemyCount;
    }

    private void CheckLevelEnd()
    {
        if (EnemyCount <= 0)
        {
            
            StartCoroutine(LevelEnd());
           
        }
    }

    public void EnemyDied()
    {
        EnemyCount--;
        CheckLevelEnd();
    }

    IEnumerator LevelEnd()
    {

        LootsAreFlying = true;

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Loot").Length == 0);
        LootsAreFlying = false;
        Time.timeScale = 0;
        levelmanager.Level += 1;
        saveData.save();
        NextLevelMenu.SetActive(true);
    }
}
