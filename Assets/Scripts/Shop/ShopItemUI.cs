using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItemUI : MonoBehaviour
{
    public enum itemPosition {
        itemOne,
        itemTwo,
        itemThree
    }
    public itemPosition itemNum;
    private TextMeshProUGUI m_itemName;
    private TextMeshProUGUI m_itemDescription;
    private TextMeshProUGUI m_itemStats;
    private Button m_button;
    private Item m_item;
    public bool isItemChosen = false;
    // Start is called before the first frame update
    void Start()
    {
        List<TextMeshProUGUI> texts = GetComponentsInChildren<TextMeshProUGUI>(true).ToList();
        texts.ForEach(x => {
            switch(x.gameObject.name) {
            case "ItemName":
                x = m_itemName;
            break;
            case "ItemDescription":
                x = m_itemDescription;
            break;
            case "ItemStats":
                x = m_itemStats;
            break;
            default:
                Debug.LogWarning("No TextMeshProGUI found! : Is " + x.name + " | " + x.gameObject.name);
            break;
            }
        });

        m_button = GetComponentInChildren<Button>(true);
        m_button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked() {
        isItemChosen = true;
        ShopManager.Instance.ChooseItem(this, m_item);
        m_button.gameObject.SetActive(false);
    }

    public void SetShopItem(Item item) {
        m_item = item;
        UpdateText();
    }

    public void UpdateText() {
        UpdateTextDetails(m_item.itemName, m_item.itemDescription);
        UpdateTextStats(m_item.itemSpeed, m_item.itemPower, m_item.itemAccuracy);
    }

    private void UpdateTextDetails(string itemName, string itemDescription) {
        m_itemName.text = itemName;
        m_itemDescription.text = itemDescription;
    }

    private void UpdateTextStats(float speed, float maxPower, float accuracy) {
        string newTextStats = $"Attack Speed: {speed}\nMax Shoot Power: {maxPower}\nAccuracy: {accuracy}";
    }
}
