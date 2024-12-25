using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public ShopItemData[] shopItems;
    public GameObject shopItemPrefab;
    public GameObject shopItemContainer;

    void Start()
    {
        foreach (var item in shopItems)
        {
            ShopItem _shopItem = Instantiate(
                    shopItemPrefab,
                    transform.position,
                    Quaternion.identity
                )
                .GetComponent<ShopItem>();

            if (item.itemIcon != null)
                _shopItem.itemImg.sprite = item.itemIcon;

            _shopItem.itemName.text = item.isAvailable ? item.itemName : "Locked";

            _shopItem.transform.SetParent(shopItemContainer.transform);
        }
    }

    void Update() { }
}
