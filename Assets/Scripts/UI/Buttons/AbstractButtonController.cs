using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class AbstractButtonController : MonoBehaviour
{
    public ButtonType buttonType;
    public CanvasType m_desiredCanvasType;
    protected Button m_button;

    // Start is called before the first frame update
    void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(OnButtonClicked);
    }

    protected virtual void OnButtonClicked() {
        CanvasManager.Instance.SwitchCanvas(m_desiredCanvasType);
    }
}
