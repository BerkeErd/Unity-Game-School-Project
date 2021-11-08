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
    public Spawner Spawner;

    void Start()
    {
        Spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        levelEnemyChecker = GameObject.Find("LevelEnemyChecker").GetComponent<LevelEnemyChecker>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        currentLevel = levelManager.Level;


        switch (currentLevel)
        {
            case 1:
                enemy1Amount = 4;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 2:
                enemy1Amount = 6;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 3:
                enemy1Amount = 8;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 4:
                enemy1Amount = 10;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 5:
                enemy1Amount = 12;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 6:
                enemy1Amount = 14;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 7:
                enemy1Amount = 16;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 8:
                enemy1Amount = 18;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 9:
                enemy1Amount = 20;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 10:
                enemy1Amount = 20;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 11:
                enemy1Amount = 0;
                enemy2Amount = 6;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 12:
                enemy1Amount = 0;
                enemy2Amount = 8;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 13:
                enemy1Amount = 0;
                enemy2Amount = 10;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 14:
                enemy1Amount = 0;
                enemy2Amount = 12;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 15:
                enemy1Amount = 0;
                enemy2Amount = 14;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 16:
                enemy1Amount = 0;
                enemy2Amount = 16;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 17:
                enemy1Amount = 0;
                enemy2Amount = 18;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 18:
                enemy1Amount = 0;
                enemy2Amount = 20;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 19:
                enemy1Amount = 0;
                enemy2Amount = 25;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 20:
                enemy1Amount = 0;
                enemy2Amount = 25;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 21:
                enemy1Amount = 0;
                enemy2Amount = 0;
                enemy3Amount = 4;
                enemy4Amount = 0;
                break;
            case 22:
                enemy1Amount = 4;
                enemy2Amount = 4;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 23:
                enemy1Amount = 6;
                enemy2Amount = 6;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 24:
                enemy1Amount = 8;
                enemy2Amount = 8;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 25:
                enemy1Amount = 10;
                enemy2Amount = 10;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 26:
                enemy1Amount = 12;
                enemy2Amount = 12;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 27:
                enemy1Amount = 14;
                enemy2Amount = 14;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 28:
                enemy1Amount = 16;
                enemy2Amount = 16;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 29:
                enemy1Amount = 18;
                enemy2Amount = 18;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;
            case 30:
                enemy1Amount = 0;
                enemy2Amount = 0;
                enemy3Amount = 0;
                enemy4Amount = 0;
                break;

            default:
                break;
        }

        totalenemy = enemy1Amount + enemy2Amount + enemy3Amount + enemy4Amount;
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
