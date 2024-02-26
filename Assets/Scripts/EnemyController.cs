using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       transform.Translate(-1 * speed * Time.deltaTime,0,0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject,0);
    }
}
