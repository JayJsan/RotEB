using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    // REFACTOR/DO BETTER - 10/06/23
    // - I REALLY DISLIKE USING GAMEOBJECT.FIND TO FIND TEXT ELEMENTS
    // - SURELY THERE'S A BETTER WAY OF DOING THIS
    public static UIManager Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI livesTMP;
    [SerializeField]
    private TextMeshProUGUI speedTMP;
    [SerializeField]    
    private TextMeshProUGUI powerTMP;
    [SerializeField]    
    private TextMeshProUGUI accuracyTMP;
    [SerializeField]
    private GameObject m_equipmentUI;
    private UIEquipmentManager m_UIEquipmentManager;
    private bool m_isInventoryOpen = false;
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
    
    // Start is called before the first frame update
    void Start()
    {
        // NEED A BETTER WAY TO GET OBJECTS BECAUSE IM JUST DRAGGING AND DROPPED REFERENCES IN THE EDITOR CURRENTLY
        if (!System.Object.ReferenceEquals(GameObject.Find("LivesText"), null)) {
            livesTMP = GameObject.Find("LivesText").GetComponent<TextMeshProUGUI>();
        } else {
            Debug.LogWarning("TextManager : CAN'T FIND LIVES TEXT");
        }

        if (!System.Object.ReferenceEquals(GameObject.Find("SpeedText"), null)) {
            speedTMP = GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>();
        } else {
            Debug.LogWarning("TextManager : CAN'T FIND SpeedText");
        }

        if (!System.Object.ReferenceEquals(GameObject.Find("PowerText"), null)) {
            powerTMP = GameObject.Find("PowerText").GetComponent<TextMeshProUGUI>();
        } else {
            Debug.LogWarning("TextManager : CAN'T FIND PowerText");
        }

        if (!System.Object.ReferenceEquals(GameObject.Find("AccuracyText"), null)) {
            accuracyTMP = GameObject.Find("AccuracyText").GetComponent<TextMeshProUGUI>();
        } else {
            Debug.LogWarning("TextManager : CAN'T FIND AccuracyText");
        }

        m_UIEquipmentManager = m_equipmentUI.GetComponentInChildren<UIEquipmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region LIVES
    public void ChangeLivesTextAmount(int amount) {
        livesTMP.text = "Lives : " + amount;
    }

    public void UpdateLivesTextAmount() {
        livesTMP.text = "Lives : " + PlayerStatManager.Instance.GetPlayerLives();
    }

    public void UpdateLivesTextStatus(string status) {
        livesTMP.text = "Lives : " + status;
    }
    #endregion

    #region ITEMS
    public void UpdateStatsTextAmount() {
        speedTMP.text = "Speed: " + PlayerStatManager.Instance.GetCurrentAttackSpeed();
        powerTMP.text = "Power: " + PlayerStatManager.Instance.GetCurrentMaxShootPower();
        accuracyTMP.text = "Accuracy: " + PlayerStatManager.Instance.GetCurrentAccuracy();
    }
    #endregion

    #region INVENTORY
    private void OpenInventory() {
        if (!System.Object.ReferenceEquals(m_equipmentUI, null)) {
            m_equipmentUI.SetActive(true);
        } else {
            Debug.LogWarning("Equipment UI GameObject not found!");
        }
    }

    private void CloseInventory() {
        if (!System.Object.ReferenceEquals(m_equipmentUI, null)) {
            m_equipmentUI.SetActive(false);
        } else {
            Debug.LogWarning("Equipment UI GameObject not found!");
        }
    }

    public void ToggleInventory() {
        if (m_isInventoryOpen) {
            m_isInventoryOpen = false;
            CloseInventory();
        }  else if (!m_isInventoryOpen) {
            m_isInventoryOpen = true;
            OpenInventory();
            UpdateInventory();
        }
    }

    private void UpdateInventory() {
        m_UIEquipmentManager.UpdatePassiveInvetoryList(PlayerItemManager.Instance.GetPassiveItems());
        m_UIEquipmentManager.UpdateActiveItem(PlayerItemManager.Instance.GetActiveItem());
    }
    #endregion
}
