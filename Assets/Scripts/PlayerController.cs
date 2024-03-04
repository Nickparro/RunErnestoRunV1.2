using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerHealth = 1;
    public int damageCounter = 0;

    //Power Ups
    public bool bikeActive;
    public bool gladiatorActive;
    public int bikeHealth = 1;
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
        PowerUp powerUp = other.GetComponent<PowerUp>();
        if(powerUp)
        {
            if(powerUp.bikeActive && !gladiatorActive && !bikeActive)
            {
                bikeActive = true;
                BikeShield();
            } 
            if(powerUp.gladiatorActive && !bikeActive && !gladiatorActive)
            {
                gladiatorActive = true;
                GladiatorShield();
            }
            if(powerUp.gladiatorActive && bikeActive && !gladiatorActive)
            {
                playerHealth -= bikeHealth;
                bikeActive = false;
                gladiatorActive = true;
                playerAnim.SetBool("bikePowerUp",false);
                GladiatorShield();
            }
            Destroy(powerUp.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            ReceiveDamage();
            Destroy(other.gameObject);
        }
    }
    public void ReceiveDamage()
    {
        playerHealth--;
        damageCounter++;

        if (playerHealth <= 0)
        {
            Debug.Log("Cagaste");
        }
        else if (bikeActive && damageCounter == 1)
        {
            playerAnim.SetBool("bikePowerUp", false);
            damageCounter = 0;
            bikeActive = false;
        } else if (gladiatorActive && damageCounter == 2)
        {
            playerAnim.SetBool("gladiatorPowerUp", false);
            damageCounter = 0;
            gladiatorActive = false;
        }
        
    }

     private void BikeShield()
    {
        playerAnim.SetBool("bikePowerUp", true);
        playerHealth += bikeHealth;
    }
    private void GladiatorShield()
    {
        playerAnim.SetBool("gladiatorPowerUp", true);
        playerHealth += gladiatorHealth;
    }
}
