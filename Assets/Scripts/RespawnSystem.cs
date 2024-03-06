using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject[] enemies; // Tipos de enemigos
    [SerializeField] private GameObject[] powerUps; // Tipos de power up
    [SerializeField] private GameObject[] rows; // Filas disponibles para generar el spawn
   

    [Header("Enemies Configuration")]
    [SerializeField] private float currentEnemySpeed; // Velocidad actual del enemigo
    [SerializeField] private float enemyInitialSpeed = 3f; 
    [SerializeField] private float enemyMaxSpeed = 10f;
    [SerializeField] private float enemyIncreaseSpeed = 0.5f; // Numero que incrementará la velocidad del enemigo
    [SerializeField] private float intervalEnemySpeed = 10f; // Intervalo sobre cada cuanto se aumentará el enemyIncreaseSpeed
    [SerializeField] private float delayEnemySpeed = 20f; // Determina cuanto tiempo se esperará antes de ejecutar el incremento (En la corrutina)
    [SerializeField] private float enemyRespawnTime; // Intervalo de respawneo actual
    [SerializeField] private float countRespawnEnemyTime; // Lleva la cuenta del enemyRespawnTime
    [SerializeField] private float subtractEnemyTime = 0.5f; // Numero de decremendo en el tiempo de respawneo
    [SerializeField] private float intervalEnemySpawn = 10f; // Intervalo en el que se decrementará el tiempo de respawneo
    [SerializeField] private float minIntervalEnemySpawn = 1f; // Minimo tiempo de respawneo
    [SerializeField] private float delayEnemySpawn = 20f; // Determina cuanto tiempo se esperará antes de ejecutar el decremento (En la corrutina)
   
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
        currentPowerUpSpeed = powerUpInitialSpeed; // Se inicializa la velocidad inicial de los Power Up
    }
    void Update()
    {
        //Los countRespawn se encargarán de llevar el tiempo que se limitará en las siguientes condiciones
        countRespawnPowerUpTime += Time.deltaTime; 
        countRespawnEnemyTime += Time.deltaTime; 
        
        if(countRespawnEnemyTime >= enemyRespawnTime) //Cuando el conteo sea mayor o igual al intervalo establecido entonces...
        {
             countRespawnEnemyTime = 0; //Se reinicia el conteo
             SpawnEnemy(); // Spawnea un enemigo
        }
        
        if(countRespawnPowerUpTime >= powerUpRespawnTime) 
            {
                countRespawnPowerUpTime = 0;
                SpawnPowerUp();
            }
        
        background.GetComponent<ParallaxBackground>().speed = currentEnemySpeed;
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
   
    IEnumerator EnemyIncreaseSpeed() //Corrutina que incrementa la velocidad del enemigo a través del tiempo 
    {
        yield return new WaitForSeconds(delayEnemySpeed); // Declara los segundos que se espera antes de ejecutar la corrutina despues de iniciar el juego
        while (currentEnemySpeed < enemyMaxSpeed) // Mientras la velocidad actual del enemigo sea menor a la velocidad maxima entonces...
        {
            yield return new WaitForSeconds(intervalEnemySpeed); 
            currentEnemySpeed += enemyIncreaseSpeed; //Se sumará el incremento a la velocidad actual en un intervalo equivalente al "intervalEnemySpeed"
        }
    }

    IEnumerator RaiseEnemies() // Corrutina que disminuye los intervalos de spawn para que salgan cada vez mas rapido
    {
        yield return new WaitForSeconds(delayEnemySpawn); 

        while (enemyRespawnTime > minIntervalEnemySpawn) // Mientras el intervalo de respawn sea mayor al numero minimo de intervalos entonces... 
        {
            yield return new WaitForSeconds(intervalEnemySpawn);
            enemyRespawnTime -= subtractEnemyTime; // Se restará el decremento a los intervalos
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
