using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHeartContainerPowerUp : PowerUp
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            heartContainers.RuntimeValue += 1;
            playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2;
            Instantiate(powerUpSound, transform.position, Quaternion.identity);
            powerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
