using System;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    [Header("Inventory and UI")]
    public Inventory playerInventory;
    public Slider magicSlider;
    public BoolValue initialized;
    
    void Start()
    {
        magicSlider.maxValue = playerInventory.maxMana;
        if(!initialized.RuntimeValue)
        {
            magicSlider.value = playerInventory.maxMana;
            playerInventory.currentMana = playerInventory.maxMana;
            initialized.RuntimeValue = true;
        }
        else
        {
            manaChange();
        }
    }

    public void manaChange()
    {
        magicSlider.value = playerInventory.currentMana;
    }
}
