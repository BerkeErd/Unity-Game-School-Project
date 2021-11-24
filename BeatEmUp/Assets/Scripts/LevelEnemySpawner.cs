using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
   

    public int enemy1Amount;
    public int enemy2Amount;
    public int enemy3Amount;
    

    public int totalenemy;

    public float minX;
    public float minY;

    public float maxX;
    public float maxY;

    public int currentLevel;

    public LevelManager levelManager;
    public LevelEnemyChecker levelEnemyChecker;
    public Spawner Spawner;

    void Start()
    {
        Spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        levelEnemyChecker = GameObject.Find("LevelEnemyChecker").GetComponent<LevelEnemyChecker>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        currentLevel = levelManager.Level;


        if(currentLevel > 20)
        {
            enemy3Amount = (currentLevel - 19) * 2;
        }
        else if(currentLevel > 10)
        {
            enemy2Amount = (currentLevel - 9) * 2;
        }
        else
        {
            enemy1Amount = (currentLevel + 1) * 2;
        }

        
        totalenemy = enemy1Amount + enemy2Amount + enemy3Amount;
        SpawnEnemy();
        Spawner.CalculateSpawnEnemies();
        levelEnemyChecker.CountEnemies();
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemy1Amount; i++)
        {
            float randY = Random.Range(minY, maxY);
            float randX = Random.Range(minX + (maxX - minX) / totalenemy * i, maxX - (maxX - minX) / totalenemy * (totalenemy - i) + 5);

            Vector2 EnemyPos = new Vector2(randX, randY);

            var Enemy1 = Instantiate(enemy1, EnemyPos, Quaternion.identity);
            Enemy1.transform.parent = GameObject.Find("Enemies").transform;
        }

        for (int i = 0; i < enemy2Amount; i++)
        {
            float randY = Random.Range(minY, maxY);
            float randX = Random.Range(minX + (maxX - minX) / totalenemy * i, maxX - (maxX - minX) / totalenemy * (totalenemy - i) + 5);

            Vector2 EnemyPos = new Vector2(randX, randY);

            var Enemy2 = Instantiate(enemy2, EnemyPos, Quaternion.identity);
            Enemy2.transform.parent = GameObject.Find("Enemies").transform;

        }

        for (int i = 0; i < enemy3Amount; i++)
        {
            float randY = Random.Range(minY, maxY);
            float randX = Random.Range(minX + (maxX - minX) / totalenemy * i, maxX - (maxX - minX) / totalenemy * (totalenemy - i) + 5);

            Vector2 EnemyPos = new Vector2(randX, randY);

            var Enemy3 = Instantiate(enemy3, EnemyPos, Quaternion.identity);
            Enemy3.transform.parent = GameObject.Find("Enemies").transform;

        }

    }
}
