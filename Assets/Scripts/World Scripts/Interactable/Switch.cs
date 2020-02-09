using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("Active Boolean")]
    public bool active;
    public BoolValue storedValue;
    
    [Header("Sprites")]
    private SpriteRenderer currentSprite;
    public Sprite activeSprite;
    
    [Header("Attached Door")]
    public Door attachedDoor;
    
    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        active = storedValue.RuntimeValue;
        if (active) 
            activeSwitch();
    }

    public void activeSwitch()
    {
        active = true;
        storedValue.RuntimeValue = active;
        attachedDoor.Open();
        currentSprite.sprite = activeSprite;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
            activeSwitch();
    }
}
