using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject thisLoot;
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] lootables;

    public GameObject loot()
    {
        int cumProb = 0, currentProb = Random.Range(0, 100);

        foreach (Loot L in lootables)
        {
            cumProb += L.lootChance;
            if (currentProb <= cumProb)
                return L.thisLoot;
        }
        return null;
    }
}
