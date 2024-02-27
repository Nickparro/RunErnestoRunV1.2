using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] rows;
    [SerializeField] private float currentEnemySpeed;
    [SerializeField] private float enemyInitialSpeed = 3f;
    [SerializeField] private float enemyMaxSpeed = 10f;
    [SerializeField] private float enemyIncreaseSpeed = 0.5f;
    [SerializeField] private float intervalEnemySpeed = 10f;
    [SerializeField] private float delayEnemySpeed = 20f;
    [SerializeField] private float enemyRespawnTime;
    [SerializeField] private float countRespawnTime;
    [SerializeField] private float subtractEnemyTime = 0.5f;
    [SerializeField] private float intervalSpawn = 10f;
    [SerializeField] private float minIntervalSpawn = 1f;
    [SerializeField] private float delaySpawn = 20f;
    
    void Start()
    {
        StartCoroutine(IncreaseSpeed());   
        StartCoroutine(RaiseEnemiesCoroutine());
        currentEnemySpeed = enemyInitialSpeed;
    }
    void Update()
    {

        countRespawnTime += Time.deltaTime; 
        
        if(countRespawnTime >= enemyRespawnTime)
        {
             countRespawnTime = 0;
             SpawnEnemy();
        }

    }
    void SpawnEnemy()
    {
        int numberRows = Random.Range(0,rows.Length);
        int enemySelected = Random.Range(0, enemies.Length);
        enemies[enemySelected].GetComponent<EnemyController>().speed = currentEnemySpeed;
        Vector2 enemyPosition = new Vector2(rows[numberRows].transform.position.x,rows[numberRows].transform.position.y);
        
        Instantiate(enemies[enemySelected], enemyPosition, Quaternion.identity);
    }

    IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(delayEnemySpeed);
        while (currentEnemySpeed < enemyMaxSpeed)
        {
            yield return new WaitForSeconds(intervalEnemySpeed);
            currentEnemySpeed += enemyIncreaseSpeed;
        }
    }

    IEnumerator RaiseEnemiesCoroutine()
    {
        yield return new WaitForSeconds(delaySpawn); // Delay inicial

        while (enemyRespawnTime > minIntervalSpawn)
        {
            yield return new WaitForSeconds(intervalSpawn);
            enemyRespawnTime -= subtractEnemyTime;
        }
    }


}
