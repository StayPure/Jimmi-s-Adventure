using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activateSign : Interactable
{
    [Header("Dialog UI")]
    public GameObject dialogBox;
    public Text dialogText;
    
    [Header("Actual Dialog")]
    public string dialog;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            interactableOff.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
