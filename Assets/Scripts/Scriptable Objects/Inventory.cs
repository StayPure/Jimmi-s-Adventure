using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    [Header("Items")]
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numKeys;
    public int numCoins;
    
    [Header("Mana")]
    public float maxMana = 10;
    public float currentMana;
    public BoolValue manaInitialized;

    public void OnEnable()
    {
        if(!manaInitialized.RuntimeValue)
            currentMana = maxMana;
    }

    public void reduceMana(float manaCost)
    {
        currentMana -= manaCost;
        if (currentMana < 0)
            currentMana = 0;
    }
    
    public void addMana(float magicAdded)
    {
        currentMana += magicAdded;
        if (currentMana > maxMana)
            currentMana = maxMana;
    }
    
    public bool checkForItem(Item item)
    {
        if (items.Contains(item))
            return true;

        return false;
    }
    
    public void addItem(Item itemToAdd)
    {
        if (itemToAdd.isKey)
            numKeys++;
        else if (!items.Contains(itemToAdd))
            items.Add(itemToAdd);
    }
}
