using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform Player;

    public LevelManager levelmanager;

    public PlayerMovement PlayerMovement;

    public GameObject Boss;

    public GameObject Enemy;

    public int FirstLevelNumber;

    public CameraFollow camerafollow;

    public MusicSource MusicSource;

    public int EnemiesWillSpawn;
    

    public bool isSpawning = false;
    private void Start()
    {
        
        MusicSource = GameObject.Find("MusicSource").GetComponent<MusicSource>();
        levelmanager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        camerafollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        Player = GameObject.Find("Fighter").GetComponent<Transform>();
        PlayerMovement = GameObject.Find("Fighter").GetComponent<PlayerMovement>();

        if (levelmanager.Level - FirstLevelNumber >= 5)
        {
            EnemiesWillSpawn = levelmanager.Level - FirstLevelNumber;
        }
        else if (levelmanager.Level - FirstLevelNumber == 10)
        {
            EnemiesWillSpawn = 1;
        }
        else { EnemiesWillSpawn = 0; }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!isSpawning && collision.gameObject.name == "Fighter" )
        {
            if (levelmanager.Level - FirstLevelNumber == 10)
            {
                PlayerMovement.isFrozen = true;
                camerafollow.isFrozen = true;
                StartCoroutine(SpawnBoss());
                MusicSource.ChangeMusic(levelmanager.Level, true, true);
            }

            else if(levelmanager.Level - FirstLevelNumber >= 5)
            {
                PlayerMovement.isFrozen = true;
                camerafollow.isFrozen = true;
                StartCoroutine(SpawnEnemies(levelmanager.Level - FirstLevelNumber));
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
            float EnemyX = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0, 2), 0, 0)).x;

            float EnemyY = Random.Range(maxYpos, minYpos);

            Vector2 EnemyPos = new Vector2 (EnemyX, EnemyY);

            Instantiate(Enemy, EnemyPos, Quaternion.identity);

            Debug.Log("Enemy Spawned");
       }
        FreezeAllEnemies();
        yield return new WaitForSeconds(5);

        UnFreezeAllEnemies();






        PlayerMovement.isFrozen = false;
       
        camerafollow.isFrozen = false;

        yield return new WaitForSeconds(1);

        Destroy(gameObject);


    }

    public IEnumerator SpawnBoss()
    {

        isSpawning = true;
        float maxYpos = PlayerMovement.maxYpos;
        float minYpos = PlayerMovement.minYpos;

            float EnemyX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

            float EnemyY = Random.Range(maxYpos, minYpos);

            Vector2 EnemyPos = new Vector2(EnemyX, EnemyY);

            Instantiate(Boss, EnemyPos, Quaternion.identity);

            Debug.Log("Enemy Boss Spawned");
        
        FreezeAllEnemies();
        yield return new WaitForSeconds(5);



        UnFreezeAllEnemies();



        PlayerMovement.isFrozen = false;

        camerafollow.isFrozen = false;

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
