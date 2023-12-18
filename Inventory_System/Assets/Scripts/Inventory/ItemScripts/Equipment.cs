using System.Collections.Generic;
using UnityEngine;
public enum SlotType { Head, Chest, Boots, Gloves, Weapon }
public enum RareLevel { Common, Rare, Unique, Legendary }
public class Equipment : Item
{
    public SlotType SlotType;
    public RareLevel RareLevel;
    public List<PlayerStats> StatsToModify = new List<PlayerStats>();
    public List<string> ModifyValue = new List<string>();
    public override void Use()
    {
        base.Use();
    }
}

