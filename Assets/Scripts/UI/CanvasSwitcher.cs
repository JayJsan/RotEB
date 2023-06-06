using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CanvasSwitcher : MonoBehaviour
{
    //public ButtonType buttonType;
    public CanvasType desiredcanvasType;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked() {
        CanvasManager.Instance.SwitchCanvas(desiredcanvasType);
    }
}
