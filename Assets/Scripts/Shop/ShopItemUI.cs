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
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemStats;
    public TextMeshProUGUI buttonText;
    public Button button;
    private Item m_item;
    public bool isItemChosen = false;
    public List<TextMeshProUGUI> texts;
    // Start is called before the first frame update
    void Start()
    {
        // NEEDA FIX THIS -- NOT FINDING COMPONENTS WHEN OBJECT INACTIVE FOR SOME REASON
        // -- TEMP FIX BY GIVING REFERENCE IN EDITOR
        texts = GetComponentsInChildren<TextMeshProUGUI>(true).ToList();
        texts.ForEach(x => {
            switch(x.gameObject.name) {
            case "ItemName":
                itemName = x;
            break;
            case "ItemDescription":
                itemDescription = x;
            break;
            case "ItemStats":
                itemStats = x;
            break;
            case "Text (TMP)":
                buttonText = x;
            break;
            default:
                Debug.LogWarning("No TextMeshProGUI found! : Is " + x.name + " | " + x.gameObject.name);
            break;
            }
        });

        button = GetComponentInChildren<Button>(true);
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnEnable() {
        button.image.color = Color.white;
        buttonText.text = "Choose";    
    }

    private void OnButtonClicked() {
        if (!isItemChosen) {
            isItemChosen = true;
            ShopManager.Instance.ChooseItem(this, m_item);

            //m_button.gameObject.SetActive(false);

            buttonText.text = "Chosen!";
            button.image.color = Color.gray;
        }
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
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;
    }

    private void UpdateTextStats(float speed, float maxPower, float accuracy) {
        string newTextStats = $"Attack Speed: {speed}\nMax Shoot Power: {maxPower}\nAccuracy: {accuracy}";
        this.itemStats.text = newTextStats;
    }
}
