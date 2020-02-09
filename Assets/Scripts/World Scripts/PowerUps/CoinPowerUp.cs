using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : PowerUp
{

    public void Start()
    {
        powerUpSignal.Raise();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.numCoins++;
            powerUpSignal.Raise();
            Instantiate(powerUpSound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
