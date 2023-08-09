using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    // 10/06/23 - NEED TO IMPROVE 
    public static PlayerItemManager Instance { get; private set; }
    [SerializeField]
    private List<Item> m_equippedPassiveItems;
    private Item m_equippedActiveItem = null;
    private List<Item> m_unequippedItems;
    private float m_totalAttackSpeed;
    private float m_totalMaxShootPower;
    private float m_totalAccuracy;
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

    public void EquipItem(Item itemToEquip) {
        if (itemToEquip.itemType == Item.Type.Active) {
            m_equippedActiveItem = itemToEquip;
        }
        if (itemToEquip.itemType == Item.Type.Passive) {
            m_equippedPassiveItems.Add(itemToEquip);
        }
        CalculateTotalStatsFromItems();
        PlayerStatManager.Instance.UpdateAllStats();
    }

    public void UnequipPassiveItem(Item itemToUnequip) {
        string nameToUnequip = itemToUnequip.itemName;
        foreach (Item item in m_equippedPassiveItems) {
            if (item.itemName == nameToUnequip) {
                m_unequippedItems.Add(item);
                m_equippedPassiveItems.RemoveAt(m_equippedPassiveItems.IndexOf(item));
                break;
            }
        }
        //m_equippedItems.ForEach(x => {if (x.name == nameToUnequip) m_equippedItems.Remove(x);});
        // Does equippedItems.Remove(itemToUnequip) work here?
        CalculateTotalStatsFromItems();
        PlayerStatManager.Instance.UpdateAllStats();
    }

    public void UnequipAllItems() {
        UnequipAllPassiveItems();
        UnequipCurrentActiveItem();
    }

    public void UnequipAllPassiveItems() {
        m_equippedPassiveItems.Clear();
        CalculateTotalStatsFromItems();
        PlayerStatManager.Instance.UpdateAllStats();
    }

    public void UnequipCurrentActiveItem() {
        if (!System.Object.ReferenceEquals(GameObject.FindGameObjectWithTag("Player"), null)) {
        m_unequippedItems.Add(m_equippedActiveItem);
        }
        m_equippedActiveItem = null;
        CalculateTotalStatsFromItems();
        PlayerStatManager.Instance.UpdateAllStats();
    }

    private void CalculateTotalStatsFromItems() {
        // Reset stats first
        m_totalAttackSpeed = 0;
        m_totalMaxShootPower = 0;
        m_totalAccuracy = 0;

        // Calcualte active item stats
        if (m_equippedActiveItem != null) {
            m_totalAttackSpeed += m_equippedActiveItem.itemSpeed;
            m_totalMaxShootPower += m_equippedActiveItem.itemPower;
            m_totalAccuracy += m_equippedActiveItem.itemAccuracy;
        }
        
        // Calculate passive item stats
        foreach (Item equippedItem in m_equippedPassiveItems) {
            m_totalAttackSpeed += equippedItem.itemSpeed;
            m_totalMaxShootPower += equippedItem.itemPower;
            m_totalAccuracy += equippedItem.itemAccuracy;
        }
    }

    public float GetTotalAttackSpeedFromItems() {
        //CalculateTotalStatsFromItems();
        return m_totalAttackSpeed;
    }

    public float GetTotalMaxShootPowerFromItems() {
        //CalculateTotalStatsFromItems();
        return m_totalMaxShootPower;
    }

    public float GetTotalAccuracyFromItems() {
        //CalculateTotalStatsFromItems();
        return m_totalAccuracy;
    }

    public List<Item> GetPassiveItems() {
        return m_equippedPassiveItems;
    }

    public Item GetActiveItem() {
        return m_equippedActiveItem;
    }

    public void ActivateItemAbility() {
        if (m_equippedActiveItem != null) {
            // IMPLEMENT COOLDOWN
            m_equippedActiveItem.itemAbility.Activate(PlayerManager.Instance.GetPlayerGameObject());
        }
    }

    // DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- 
    // DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- 
    // DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- 
    public void GenerateRandomEquippedItem() {
        float randomSpeed = Random.Range(-2, 10);
        float randomPower = Random.Range(-50,50);
        float randomAccuracy = Random.Range(-100,100);
        //Item randomItem = new Item(randomSpeed, randomPower, randomAccuracy);
        Item randomItem = (Item)ScriptableObject.CreateInstance("Item");
        randomItem.itemSpeed = randomSpeed;
        randomItem.itemPower = randomPower;
        randomItem.itemAccuracy = randomAccuracy;
        
        m_equippedPassiveItems.Add(randomItem);
        PlayerStatManager.Instance.UpdateAllStats();
    }

    public void EquipItemWithDashAbility() {
        Debug.LogWarning("HAVE NOT IMPLEMENTED YET SORRY");
    }
}
