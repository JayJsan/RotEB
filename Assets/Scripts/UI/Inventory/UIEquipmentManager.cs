using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEquipmentManager : MonoBehaviour
{
    public GameObject itemUIPrefab;
    private GameObject m_equipmentHolder;
    private GameObject[,] m_equippedPassiveItems = new GameObject[2,10] {{null,null,null,null,null,null,null,null,null,null},{null,null,null,null,null,null,null,null,null,null}}; 
    private GameObject m_equippedActiveItem;

    public void UpdatePassiveInvetoryList(List<Item> passiveItem) {
        ClearAllPassiveItems();
        for (int j = 0; j < passiveItem.Count; j++) {
            for (int i = 0; i < 1; i++) {
                if (i == 0) {
                    m_equippedPassiveItems[i,j] = CreateNewItemSlot(passiveItem[j + i], transform.position + new Vector3(-50, 200 - 85 * j, 0));
                } else {
                    m_equippedPassiveItems[i,j] = CreateNewItemSlot(passiveItem[j + i], transform.position + new Vector3(50, 200 - 85 * j, 0));
                }
            }
        }
    }

    public void UpdateActiveItem(Item activeItem) {
        if (System.Object.ReferenceEquals(activeItem, null)) {
            return;
        }
    
        // If slot not empty
        if (!System.Object.ReferenceEquals(m_equippedActiveItem, null)) {
            Destroy(m_equippedActiveItem);
        }
        m_equippedActiveItem = CreateNewItemSlot(activeItem, transform.position - new Vector3(180, -180, 0));
    }

    private GameObject CreateNewItemSlot(Item item, Vector3 position) {
        GameObject newItemSlot = Instantiate(itemUIPrefab, position, Quaternion.identity, this.transform);
        TextMeshProUGUI[] itemTexts = newItemSlot.GetComponentsInChildren<TextMeshProUGUI>();

        itemTexts[0].text = item.itemName; // itemTexts[0] should always be NAME
        itemTexts[1].text = $"Attack Speed: {item.itemSpeed}\nMax Shoot Power: {item.itemPower}\nAccuracy: {item.itemAccuracy}"; // itemTexts[0] should always be Stats

        return newItemSlot;
    }

    private void ClearAllPassiveItems() {
        foreach (GameObject uiElement in m_equippedPassiveItems) {
            Destroy(uiElement);
        }
    }
}
