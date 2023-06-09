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
    private TextMeshProUGUI speedTMP;
    private TextMeshProUGUI powerTMP;
    private TextMeshProUGUI accuracyTMP;

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
}
