using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI livesTMP;

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

    #endregion
}
