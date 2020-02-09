using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemDescription;
    public string flavorText;
    public bool isKey;
}
