using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [Header("Contents")]
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;
    
    [Header("Signals and Dialog")]
    public Signal presentItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anime;

    [Header("Sound Effects")]
    public AudioSource chestOpening;
    
    void Start()
    {
        dialogBox = GameObject.FindWithTag("Canvas").transform.GetChild(1).gameObject;
        dialogText = dialogBox.GetComponentInChildren<Text>();
        anime = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        
        if (isOpen)
            anime.SetBool("open", true);
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            if (!isOpen)
                openChest();
            else
                ChestIsOpen();
        }
    }
    public void openChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.addItem(contents);
        playerInventory.currentItem = contents;
        presentItem.Raise();
        isOpen = true;
        interactableOff.Raise();
        anime.SetBool("open", true);
        storedOpen.RuntimeValue = isOpen;
        Instantiate(chestOpening, transform.position, Quaternion.identity);
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !isOpen)
        {
            interactableOn.Raise();
            playerInRange = true;
        }
    }

    public void ChestIsOpen()
    {
        dialogBox.SetActive(false);
        presentItem.Raise();
        if(CompareTag("BossChest"))
            Destroy(gameObject);
    }
}
