using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform Player;
    public LevelManager levelmanager;
    public PlayerMovement PlayerMovement;
    public GameObject Boss;
    public GameObject Enemy;
    public CameraFollow camerafollow;
    public MusicSource MusicSource;

    public Text CountdownText;

    public int FirstLevelNumber;
    public int EnemiesWillSpawn;
    public bool isSpawning = false;

    private void Awake()
    {
        CountdownText = GameObject.Find("Countdown Text").GetComponent<Text>();
        MusicSource = GameObject.Find("MusicSource").GetComponent<MusicSource>();
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        camerafollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        Player = GameObject.Find("Fighter").GetComponent<Transform>();
        PlayerMovement = GameObject.Find("Fighter").GetComponent<PlayerMovement>();


        CountdownText.gameObject.SetActive(false);
        
    }

    
    public void CalculateSpawnEnemies()
    {
        if(levelmanager.Level < 11)
        {
            FirstLevelNumber = 0;
        }
        else if (levelmanager.Level < 21)
        {
            FirstLevelNumber = 10;
        }
        else if (levelmanager.Level < 31)
        {
            FirstLevelNumber = 20;
        }

        if (levelmanager.Level - FirstLevelNumber == 10)
        {
            EnemiesWillSpawn = 1 + levelmanager.Level - FirstLevelNumber;

        }
        else if (levelmanager.Level - FirstLevelNumber >= 5)
        {
            EnemiesWillSpawn = levelmanager.Level - FirstLevelNumber + 3;
        }
        else { EnemiesWillSpawn = 0; }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSpawning && collision.gameObject.name == "Fighter")
        {
            if (levelmanager.Level - FirstLevelNumber == 10)
            {
                Player.GetComponent<PlayerCombat>().isUsingKickSkill = false;
                Player.GetComponent<PlayerCombat>().isUsingPunchSkill = false;
                PlayerMovement.isFrozen = true;
                camerafollow.isFrozen = true;
                StartCoroutine(SpawnBoss());
                StartCoroutine(SpawnEnemies(levelmanager.Level - FirstLevelNumber));
                MusicSource.ChangeMusic(levelmanager.Level, true, true);
            }

            else if (levelmanager.Level - FirstLevelNumber >= 5)
            {
                Player.GetComponent<PlayerCombat>().isUsingKickSkill = false;
                Player.GetComponent<PlayerCombat>().isUsingPunchSkill = false;
                PlayerMovement.isFrozen = true;
                camerafollow.isFrozen = true;
                StartCoroutine(SpawnEnemies(levelmanager.Level - FirstLevelNumber + 3));
                MusicSource.ChangeMusic(levelmanager.Level, false, true);
            }
        }
    }


    public IEnumerator SpawnEnemies(int EnemyNumber)
    {

        isSpawning = true;
        float maxYpos = PlayerMovement.maxYpos;
        float minYpos = PlayerMovement.minYpos;

        for (int i = 0; i < EnemyNumber; i++)
        {
            float randomX = Random.Range(0, 2);
            
            float EnemyX = Camera.main.ViewportToWorldPoint(new Vector3(randomX, 0, 0)).x;
           
            float EnemyY = Random.Range(maxYpos, minYpos);

            Vector2 EnemyPos = new Vector2(EnemyX, EnemyY);

            Instantiate(Enemy, EnemyPos, Quaternion.identity);

            Debug.Log("Enemy Spawned");
        }

        yield return StartCoroutine(CountDown());

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }


    public IEnumerator CountDown()
    {
        FreezeAllEnemies();
        CountdownText.gameObject.SetActive(true);
        CountdownText.text = "Get Ready";
        yield return new WaitForSeconds(1);
        CountdownText.text = "3...";
        yield return new WaitForSeconds(1);
        CountdownText.text = "2...";
        yield return new WaitForSeconds(1);
        CountdownText.text = "1...";
        yield return new WaitForSeconds(1);
        CountdownText.text = "FIGHT";
        yield return new WaitForSeconds(0.5f);
        UnFreezeAllEnemies();
        CountdownText.gameObject.SetActive(false);
        PlayerMovement.isFrozen = false;
        camerafollow.isFrozen = false;   
        
    }

    public IEnumerator SpawnBoss()
    {
        isSpawning = true;
        float maxYpos = PlayerMovement.maxYpos;
        float minYpos = PlayerMovement.minYpos;

        float EnemyX = Camera.main.ViewportToWorldPoint(new Vector3(1.5f, 0, 0)).x;
        float EnemyY = Random.Range(maxYpos, minYpos);

        Vector2 EnemyPos = new Vector2(EnemyX, EnemyY);

        Instantiate(Boss, EnemyPos, Quaternion.identity);

        Debug.Log("Enemy Boss Spawned");
;
        yield return StartCoroutine(CountDown());
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    public void UnFreezeAllEnemies()
    {
        foreach (var Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Enemy.GetComponent<Enemy>())
            {
                Enemy.GetComponent<Enemy>().isFrozen = false;
            }

            else if (Enemy.GetComponent<EnemyBoss>())
            {
                Enemy.GetComponent<EnemyBoss>().isFrozen = false;
            }

        }
    }
    public void FreezeAllEnemies()
    {
        
        foreach (var Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Enemy.GetComponent<Enemy>())
            {
                Enemy.GetComponent<Enemy>().isFrozen = true;
            }

            else if (Enemy.GetComponent<EnemyBoss>())
            {
                Enemy.GetComponent<EnemyBoss>().isFrozen = true;
            }

        }
    }
}
