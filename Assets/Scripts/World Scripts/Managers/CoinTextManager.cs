using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    [Header("Inventory and HUD")]
    public Inventory playerInventory;
    public TextMeshProUGUI coinCounter;

    public void UpdateCoinCount()
    {
        coinCounter.text = playerInventory.numCoins + "";
    }
}
