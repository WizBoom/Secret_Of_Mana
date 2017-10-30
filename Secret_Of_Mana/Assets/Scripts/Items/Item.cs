using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Other
}
public abstract class Item : ScriptableObject
{
    public String m_ItemName = "Item Name";
    public int m_Amount = 1;
    public ItemType m_Type = ItemType.Other;

    public static Color GetTypeColor(ItemType item)
    {
        switch (item)
        {
            case ItemType.Weapon:
                return Color.cyan;
            case ItemType.Consumable:
                return Color.yellow;
            case ItemType.Other:
                return Color.white;
            default:
                return Color.red;
        }
    }
}
