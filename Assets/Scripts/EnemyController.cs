using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
       
    }

    void FixedUpdate()
    {
       transform.Translate(-1 * speed * Time.deltaTime,0,0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject,0);
    }

}
