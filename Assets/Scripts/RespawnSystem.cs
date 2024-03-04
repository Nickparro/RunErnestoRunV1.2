using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private GameObject[] rows;

    [Header("Enemies Configuration")]
    [SerializeField] private float currentEnemySpeed;
    [SerializeField] private float enemyInitialSpeed = 3f;
    [SerializeField] private float enemyMaxSpeed = 10f;
    [SerializeField] private float enemyIncreaseSpeed = 0.5f;
    [SerializeField] private float intervalEnemySpeed = 10f;
    [SerializeField] private float delayEnemySpeed = 20f;
    [SerializeField] private float enemyRespawnTime;
    [SerializeField] private float countRespawnEnemyTime;
    [SerializeField] private float subtractEnemyTime = 0.5f;
    [SerializeField] private float intervalEnemySpawn = 10f;
    [SerializeField] private float minIntervalEnemySpawn = 1f;
    [SerializeField] private float delayEnemySpawn = 20f;
   
    [Header("Power Up Configuration")]
    [SerializeField] private float currentPowerUpSpeed;
    [SerializeField] private float powerUpInitialSpeed = 3f;
    [SerializeField] private float powerUpMaxSpeed = 10f;
    [SerializeField] private float powerUpIncreaseSpeed = 0.5f;
    [SerializeField] private float intervalPowerUpSpeed = 10f;
    [SerializeField] private float delayPowerUpSpeed = 20f;
    [SerializeField] private float powerUpRespawnTime;
    [SerializeField] private float countRespawnPowerUpTime;
    [SerializeField] private float subtractPowerUpTime = 0.5f;
    [SerializeField] private float intervalPowerUpSpawn = 10f;
    [SerializeField] private float minIntervalPowerUpSpawn = 1f;
    [SerializeField] private float delayPowerUpSpawn = 20f;
    
    void Start()
    {
        StartCoroutine(EnemyIncreaseSpeed());   
        StartCoroutine(RaiseEnemies());
        StartCoroutine(PowerUpIncreaseSpeed());
        StartCoroutine(RaisePowerUps());
        currentEnemySpeed = enemyInitialSpeed; // Se inicializa la velocidad inicial del enemigo
        currentPowerUpSpeed = powerUpInitialSpeed;
    }
    void Update()
    {
        countRespawnPowerUpTime += Time.deltaTime;
        countRespawnEnemyTime += Time.deltaTime; 
        
        if(countRespawnEnemyTime >= enemyRespawnTime)
        {
             countRespawnEnemyTime = 0;
             SpawnEnemy();
        }
        
        if(countRespawnPowerUpTime >= powerUpRespawnTime)
            {
                countRespawnPowerUpTime = 0;
                SpawnPowerUp();
            }
    }
    void SpawnEnemy()
    {
        int numberRows = Random.Range(0,rows.Length); // Establece el numero de filas disponibles para instanciar aleatoriamente
        int enemySelected = Random.Range(0, enemies.Length); // Elige los enemigos que apareceran de manera aleatoria
        enemies[enemySelected].GetComponent<EnemyController>().enemySpeed = currentEnemySpeed; //Le añade componente Speed al enemigo instanciado
        Vector2 enemyPosition = new Vector2(rows[numberRows].transform.position.x,rows[numberRows].transform.position.y); // Se determina una posición aleatoria
        
        Instantiate(enemies[enemySelected], enemyPosition, Quaternion.identity); // Instancia el enemigo seleccionado en una posicion aleatoria entre las 3 filas
    }

    void SpawnPowerUp()
    {
        int numberRows = Random.Range(0,rows.Length); 
        int powerUpSelected = Random.Range(0, powerUps.Length); 
        powerUps[powerUpSelected].GetComponent<PowerUp>().powerUpSpeed = currentEnemySpeed;
        Vector2 powerUpPosition = new Vector2(rows[numberRows].transform.position.x,rows[numberRows].transform.position.y); 
        
        Instantiate(powerUps[powerUpSelected], powerUpPosition, Quaternion.identity); 
    }
   
    IEnumerator EnemyIncreaseSpeed()
    {
        yield return new WaitForSeconds(delayEnemySpeed);
        while (currentEnemySpeed < enemyMaxSpeed)
        {
            yield return new WaitForSeconds(intervalEnemySpeed);
            currentEnemySpeed += enemyIncreaseSpeed;
        }
    }

    IEnumerator RaiseEnemies()
    {
        yield return new WaitForSeconds(delayEnemySpawn); 

        while (enemyRespawnTime > minIntervalEnemySpawn)
        {
            yield return new WaitForSeconds(intervalEnemySpawn);
            enemyRespawnTime -= subtractEnemyTime;
        }
    }

    IEnumerator PowerUpIncreaseSpeed()
    {
        yield return new WaitForSeconds(delayPowerUpSpeed);
        while (currentPowerUpSpeed < powerUpMaxSpeed)
        {
            yield return new WaitForSeconds(intervalPowerUpSpeed);
            currentPowerUpSpeed += powerUpIncreaseSpeed;
        }
    }

    IEnumerator RaisePowerUps()
    {
        yield return new WaitForSeconds(delayPowerUpSpawn); 

        while (powerUpRespawnTime > minIntervalPowerUpSpawn)
        {
            yield return new WaitForSeconds(intervalPowerUpSpawn);
            powerUpRespawnTime -= subtractPowerUpTime;
        }
    }


}
