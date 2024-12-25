using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemValue;
    public bool isAvailable;
}
