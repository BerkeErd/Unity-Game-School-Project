using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour
{

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    public int enemy1Amount;
    public int enemy2Amount;
    public int enemy3Amount;
    public int enemy4Amount;

    public int totalenemy;

    public float minX;
    public float minY;

    public float maxX;
    public float maxY;

    public int currentLevel;

    public LevelManager levelManager;
    public LevelEnemyChecker levelEnemyChecker;

    void Start()
    {
        levelEnemyChecker = GameObject.Find("LevelEnemyChecker").GetComponent<LevelEnemyChecker>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        currentLevel = levelManager.Level;

        switch (currentLevel + 1)
        {
            case 1:
                enemy1Amount = 10;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 2:
                enemy1Amount = 15;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 3:
                enemy1Amount = 20;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 4:
                enemy1Amount = 20;
                enemy2Amount = 5;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 5:
                enemy1Amount = 20;
                enemy2Amount = 10;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 6:
                enemy1Amount = 20;
                enemy2Amount = 15;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 7:
                enemy1Amount = 20;
                enemy2Amount = 20;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 8:
                enemy1Amount = 20;
                enemy2Amount = 20;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 9:
                enemy1Amount = 20;
                enemy2Amount = 20;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 10:
                enemy1Amount = 20;
                enemy2Amount = 20;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 11:
                enemy1Amount = 20;
                enemy2Amount = 20;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;

            default:
                break;
        }

        totalenemy = enemy1Amount + enemy2Amount + enemy3Amount + enemy4Amount;
        SpawnEnemy();
        levelEnemyChecker.CountEnemies();
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemy1Amount; i++)
        {
            float randY = Random.Range(minY, maxY);

            //float randX = Random.Range(minX + maxX / totalenemy +1, maxX / totalenemy);
            float randX = Random.Range(minX + (maxX - minX) / totalenemy * i, maxX - (maxX - minX) / totalenemy * (totalenemy - i) + 5);

            Vector2 EnemyPos = new Vector2(randX, randY);

            Instantiate(enemy1, EnemyPos, Quaternion.identity);

            //totalenemy -= 1;
        }

        for (int i = 0; i < enemy2Amount; i++)
        {
            float randY = Random.Range(minY, maxY);

            //float randX = Random.Range(minX + maxX / totalenemy +1, maxX / totalenemy);
            float randX = Random.Range(minX + (maxX - minX) / totalenemy * i, maxX - (maxX - minX) / totalenemy * (totalenemy - i) + 5);

            Vector2 EnemyPos = new Vector2(randX, randY);

            Instantiate(enemy2, EnemyPos, Quaternion.identity);

            //totalenemy -= 1;
        }

    }
}
