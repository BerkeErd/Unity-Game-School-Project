using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform Player;

    public PlayerMovement PlayerMovement;

    public GameObject Enemy;

    public int EnemyNumber;

    public CameraFollow camerafollow;
    

    public bool isSpawning = false;
    private void Start()
    {
        camerafollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        Player = GameObject.Find("Fighter").GetComponent<Transform>();
        PlayerMovement = GameObject.Find("Fighter").GetComponent<PlayerMovement>();
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!isSpawning && collision.gameObject.name == "Fighter")
        {
            PlayerMovement.isFrozen = true;
            camerafollow.isFrozen = true;
            StartCoroutine(SpawnEnemies());
        }
    }
  

    public IEnumerator SpawnEnemies()
    {
       
        isSpawning = true;
        float maxYpos = PlayerMovement.maxYpos;
        float minYpos = PlayerMovement.minYpos;

        

        for (int i = 0; i <= EnemyNumber; i++)
        {
            float EnemyX = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0, 2), 0, 0)).x;

            float EnemyY = Random.Range(maxYpos, minYpos);

            Vector2 EnemyPos = new Vector2 (EnemyX, EnemyY);

            Instantiate(Enemy, EnemyPos, Quaternion.identity);

            Debug.Log("Enemy Spawned");
       }
        FreezeAllEnemies();
        yield return new WaitForSeconds(5);

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

      

      

        PlayerMovement.isFrozen = false;
       
        camerafollow.isFrozen = false;

        yield return new WaitForSeconds(1);

        Destroy(gameObject);


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
