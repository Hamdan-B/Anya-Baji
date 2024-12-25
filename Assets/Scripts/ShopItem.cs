using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int itemPrice;

    public Image itemImg;
    public TMP_Text itemName;

    void Start() { }

    void Update() { }

    public void PushToInventory()
    {
        if (itemName.text == "Tomato")
        {
            var _inventoryItems = FindObjectOfType<PlayerManager>().inventoryItems;
            _inventoryItems.Add(itemName.text);
        }
    }
}
