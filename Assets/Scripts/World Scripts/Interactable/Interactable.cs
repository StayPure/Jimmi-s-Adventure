using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Signal interactableOff, interactableOn;
    public bool playerInRange, objectOn = true;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && objectOn)
        {
            interactableOn.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player") && objectOn)
        {
            interactableOff.Raise();
            playerInRange = false;
        }
    }

    public void setObjectState(bool input)
    {
        if (!input)
        {
            interactableOff.Raise();
            objectOn = false;
        }
        else
        {
            objectOn = true;
        }
    }
}
