using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed;
    
    void Start()
    {
       
    }

    void FixedUpdate()
    {
       transform.Translate(-1 * enemySpeed * Time.deltaTime,0,0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject,0);
        }
    }

}
