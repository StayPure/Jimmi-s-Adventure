using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPowerup : PowerUp
{
    public float potionSize;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory.addMana(potionSize);
            Instantiate(powerUpSound, transform.position, Quaternion.identity);
            powerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
