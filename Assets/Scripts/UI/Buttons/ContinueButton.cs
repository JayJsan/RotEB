using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ContinueButton : AbstractButtonController
{
    // Start is called before the first frame update
    void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(OnButtonClicked);
    }

    protected override void OnButtonClicked() {
        CanvasManager.Instance.SwitchCanvas(m_desiredCanvasType);
        // NEED TO CHANGE TO CONTINUE GAME
        GameManager.Instance.ContinueGame();
    }
}
