using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    private List<Item> m_shopItemPool = new List<Item>();
    private List<Item> m_currentShopitems = new List<Item>();
    private int m_amountOfShopItemsAllowed = 3;
    void Start()
    {
        // Add to shopitem pool
        // -- NEED TO IMPLEMENT -- USE ADDRESSABLE ASSETS?
        // for now we will generate random items here
        for (int i = 0; i < 30; i++) {
            float randomSpeed = Random.Range(-2, 10);
            float randomPower = Random.Range(-50,50);
            float randomAccuracy = Random.Range(-100,100);
            //Item randomItem = new Item(randomSpeed, randomPower, randomAccuracy);
            Item randomItem = (Item)ScriptableObject.CreateInstance("Item");
            randomItem.itemName = "randomItem" + i;
            randomItem.itemDescription = "randomDescription" + i;
            randomItem.itemSpeed = randomSpeed;
            randomItem.itemPower = randomPower;
            randomItem.itemAccuracy = randomAccuracy;
            m_shopItemPool.Add(randomItem);
        }
    }

    public void RandomizeCurrentShopItems() {
        for (int i = 0; i < m_amountOfShopItemsAllowed; i++) {
            Item item = m_shopItemPool[Random.Range(0,m_shopItemPool.Count)];
            m_currentShopitems.Add(item);
        }
    }

    public List<Item> GetCurrentShopitems() {
        return m_currentShopitems;
    }

    public void AddToShopItemPool(Item item) {
        m_shopItemPool.Add(item);
    }
}
