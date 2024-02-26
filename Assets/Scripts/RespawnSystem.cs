using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] rows;
    [SerializeField] private float enemyTime;
    public float respawnTime;
    void Start()
    {

    }
    void Update()
    {
        respawnTime += Time.deltaTime;

        if(respawnTime >= enemyTime)
        {
            respawnTime = 0;
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        int numberRows = Random.Range(0,rows.Length);
        int enemySelected = Random.Range(0, enemies.Length);
        Vector2 enemyPosition = new Vector2(rows[numberRows].transform.position.x,rows[numberRows].transform.position.y);
        Instantiate(enemies[enemySelected], enemyPosition, Quaternion.identity);
    }
}
