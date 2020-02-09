using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerUp : PowerUp
{
    public FloatValue playerHealth;
    public float healthAmountUp;
    public FloatValue heartContainers;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerHealth.RuntimeValue += healthAmountUp;
            if (playerHealth.initialValue > heartContainers.initialValue * 2)
                playerHealth.initialValue = heartContainers.initialValue * 2f;

            Instantiate(powerUpSound, transform.position, Quaternion.identity);
            powerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
