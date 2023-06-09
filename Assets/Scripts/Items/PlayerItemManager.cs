using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    // 10/06/23 - NEED TO IMPROVE 
    public static PlayerItemManager Instance { get; private set; }
    [SerializeField]
    private List<Item> m_equippedItems;

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
        m_equippedItems.Add(itemToEquip);
        CalculateTotalStatsFromItems();
    }

    public void UnequipItem(Item itemToUnequip) {
        string nameToUnequip = itemToUnequip.itemName;
        foreach (Item item in m_equippedItems) {
            if (item.itemName == nameToUnequip) {
                m_equippedItems.RemoveAt(m_equippedItems.IndexOf(item));
                break;
            }
        }
        //m_equippedItems.ForEach(x => {if (x.name == nameToUnequip) m_equippedItems.Remove(x);});
        // Does equippedItems.Remove(itemToUnequip) work here?
        CalculateTotalStatsFromItems();
    }

    private void CalculateTotalStatsFromItems() {
        // Reset stats first
        m_totalAttackSpeed = 0;
        m_totalMaxShootPower = 0;
        m_totalAccuracy = 0;

        // Calculate all
        foreach (Item equippedItem in m_equippedItems) {
            m_totalAttackSpeed += equippedItem.itemSpeed;
            m_totalMaxShootPower += equippedItem.itemPower;
            m_totalAccuracy += equippedItem.itemAccuracy;
        }
    }

    public float GetTotalAttackSpeedFromItems() {
        CalculateTotalStatsFromItems();
        return m_totalAttackSpeed;
    }

    public float GetTotalMaxShootPowerFromItems() {
        CalculateTotalStatsFromItems();
        return m_totalMaxShootPower;
    }

    public float GetTotalAccuracyFromItems() {
        CalculateTotalStatsFromItems();
        return m_totalAccuracy;
    }

    // DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- 
    // DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- 
    // DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- DEBUG PURPOSES -- 
    public void GenerateRandomEquippedItem() {
        float randomSpeed = Random.Range(-2, 2);
        float randomPower = Random.Range(-50,50);
        float randomAccuracy = Random.Range(-100,100);
        Item randomItem = new Item(randomSpeed, randomPower, randomAccuracy);
        m_equippedItems.Add(randomItem);
        PlayerStatManager.Instance.UpdateAllStats();
    }

    public void UnequipAllItems() {
        m_equippedItems.Clear();
        PlayerStatManager.Instance.UpdateAllStats();
    }
}
