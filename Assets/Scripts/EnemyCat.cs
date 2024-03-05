using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCat : MonoBehaviour
{
    public float speed;
    private Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject, 0);
    }

}