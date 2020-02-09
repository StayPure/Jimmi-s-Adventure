using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public enum doorPlacement
{
    top,
    left,
    right,
    bottom
}

public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType doorType;
    public bool open;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    public doorPlacement doorPlacement;

    public void Start()
    {
        if (doorType == DoorType.enemy)
            Open();
        else
            Close();
    }
    
    public void Update()
    {
        if (Input.GetButtonDown("Interact") 
            && playerInRange && doorType == DoorType.key 
            && playerInventory.numKeys > 0)
        {
            playerInventory.numKeys--;
            Open();
        }
    }

    public void Open()
    {
        if (doorType == DoorType.key)
            Destroy(gameObject);
        
        setObjectState(false);
        doorSprite.enabled = false;
        open = true;
        physicsCollider.enabled = false;
    }

    public void Close()
    {
        setObjectState(true);
        doorSprite.enabled = true;
        open = false;
        physicsCollider.enabled = true;
    }
}
