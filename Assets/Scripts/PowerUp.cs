using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool bikeActive;
    public bool gladiatorActive;
    public float powerUpSpeed;
    
    void FixedUpdate()
    {
       transform.Translate(-1 * powerUpSpeed * Time.deltaTime,0,0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject,0);
        }
    }
}
