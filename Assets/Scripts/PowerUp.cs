using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool bikeActive; // Si es = true en el inspector, entonces el objeto es PowerUp cicla
    public bool gladiatorActive; // Si es = true en el inspector, entonces el objeto es PowerUp gladiador
    public float powerUpSpeed; // Velocidad en la que corre el powerup en el juego
    
    void FixedUpdate()
    {
       transform.Translate(-1 * powerUpSpeed * Time.deltaTime,0,0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Finish")) // Cuando collisione con el limite puesto antes del player...
        {
            Destroy(gameObject,0);
        }
    }
}
