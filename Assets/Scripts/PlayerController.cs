using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerHealth = 1;
    public int damageCounter = 0; // Lleva el conteo del daño recibido

    //Power Ups
    public bool bikeActive; // Rectifica que el power-Up de casco de cicla/gladiador este activado
    public bool gladiatorActive;
    public int bikeHealth = 1; // Se guarda la cantidad de golpes que aguanta cada Power-Up
    public int gladiatorHealth = 2;
    public Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PowerUp powerUp = other.GetComponent<PowerUp>(); // Se llama a toda colision que esté sujeta a un objeto con el script "PowerUp"
        if(powerUp)
        {
            if(powerUp.bikeActive && !gladiatorActive && !bikeActive) // Se pregunta si el objeto es un PowerUp de casco de cicla y si Player no tiene acivado ningun Power Up
            {
                bikeActive = true; 
                BikeShield();
            } 
            if(powerUp.gladiatorActive && !bikeActive && !gladiatorActive) // Se pregunta si el objeto es un PowerUp de casco de gladiador y si Player no tiene acivado ningun Power Up
            {
                gladiatorActive = true;
                GladiatorShield();
            }
            if(powerUp.gladiatorActive && bikeActive && !gladiatorActive) // Se pregunta si el objeto es un PowerUp de casco de gladiador teniendo activado el casco de cicla
            {
                playerHealth -= bikeHealth; //Se elimina la vida otorgada por el casco de la cicla para cambiar al casco del gladiador
                bikeActive = false;
                gladiatorActive = true;
                playerAnim.SetBool("bikePowerUp",false); // Se detiene la animación del casco de cicla
                GladiatorShield();
            }
            Destroy(powerUp.gameObject); //Se destruye el powerup al ser recogido
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")) //Se pregunta cuando el jugador colisione con un objeto con el tag "Enemy"
        {
            ReceiveDamage();
            Destroy(other.gameObject);
        }
    }
    public void ReceiveDamage() // Se implementan los eventos al recibir daño
    {
        playerHealth--; // Se resta una vida por cada colision
        damageCounter++; // El conteo de daño suma 1

        if (playerHealth <= 0) // Cuando la vida llega a 0 el jugador pierde (Falta implementar el resto)
        {
            Debug.Log("Cagaste");
        }
        else if (bikeActive && damageCounter == bikeHealth) // Si tiene el powerup de cicla y el contador de daño se igual a la vida del power up entonces...
        {
            playerAnim.SetBool("bikePowerUp", false); // Animacion de cicla se desactiva
            damageCounter = 0; // El contador de daño vuelve a cero
            bikeActive = false; // Se desactiva el powerup
        } else if (gladiatorActive && damageCounter == gladiatorHealth) // Misma situación con powerup de gladiador
        {
            playerAnim.SetBool("gladiatorPowerUp", false);
            damageCounter = 0;
            gladiatorActive = false;
        }
        
    }

     private void BikeShield() // Metodo que activa power up de cicla
    {
        playerAnim.SetBool("bikePowerUp", true); // Se activa animacion de casco de cicla
        playerHealth += bikeHealth; // la vida del power up se le suma a la vida del jugador
    }
    private void GladiatorShield() // Metodo que activa power up de gladiador
    {
        playerAnim.SetBool("gladiatorPowerUp", true);
        playerHealth += gladiatorHealth;
    }
}
