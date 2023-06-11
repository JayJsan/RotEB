using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    private ShopInventory m_shopInventory;
    private List<ShopItemUI> shopItemUIs;
    public GameObject itemContainer;
    private void Awake() {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        m_shopInventory = GetComponent<ShopInventory>();

        if (m_shopInventory == null) {
            m_shopInventory = gameObject.AddComponent<ShopInventory>();
        }

        // NEED TO FIND BETTER WAY OF FINDING ITEMCONTAINER
        if (!System.Object.ReferenceEquals(itemContainer, null)) {
            shopItemUIs = itemContainer.GetComponentsInChildren<ShopItemUI>(true).ToList();
        } else {
            Debug.LogWarning("ItemContainer not found!");
        }

    }

    public void RandomizeShop() {
        List<Item> shopItems;
        m_shopInventory.RandomizeCurrentShopItems();
        shopItems = m_shopInventory.GetCurrentShopitems();

        int index = 0;
        shopItemUIs.ForEach(x => {
            x.SetShopItem(shopItems[index]);
            index++;
        });
    }

    public List<Item> GetShopItems() {
        return m_shopInventory.GetCurrentShopitems();
    }

    public void ChooseItem(ShopItemUI shopItemUI, Item item) {
        PlayerItemManager.Instance.EquipItem(item);
        shopItemUIs.ForEach(x => {
            if (x.isItemChosen != shopItemUI.isItemChosen) {
                x.gameObject.SetActive(false);
            }
        });
    }
}
